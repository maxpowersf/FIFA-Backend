using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ranking.Application.Implementations;
using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Data;
using Ranking.Data.Repositories;

namespace Ranking.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthorization();
            services.AddDbContext<RankingContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //services.AddGraphQLAuth();
            services.AddAutoMapper(typeof(Startup));
            this.ConfigureIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureIoC(IServiceCollection services)
        {
            ConfigureRepositories(services);
            ConfigureApplicationServices(services);
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IMatchTypeRepository, MatchTypeRepository>();
            services.AddTransient<IConfederationRepository, ConfederationRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IRankingRepository, RankingRepository>();
            services.AddTransient<ITournamentTypeRepository, TournamentTypeRepository>();
            services.AddTransient<ITournamentRepository, TournamentRepository>();
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IMatchTypeService, MatchTypeService>();
            services.AddTransient<IConfederationService, ConfederationService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IRankingService, RankingService>();
            services.AddTransient<ITournamentTypeService, TournamentTypeService>();
            services.AddTransient<ITournamentService, TournamentService>();
        }
    }
}
