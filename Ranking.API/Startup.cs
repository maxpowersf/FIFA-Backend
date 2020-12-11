using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "FIFA Management API",
                    Description = "This documentation provides information about FIFA Management API endpoints",
                    Contact = new OpenApiContact()
                    {
                        Name = "Paolo Marcolini",
                        Email = "ingpmarcolini@gmail.com"
                    }
                });
            });
            
            services.Configure<IISOptions>(options =>
            {

            });

            services.AddAuthorization();
            services.AddDbContext<RankingContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .EnableSensitiveDataLogging(true);
            });

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
                app.Use(async (context, next) =>
                {
                    await next();
                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FIFA Management API");
            });
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
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IRankingRepository, RankingRepository>();
            services.AddTransient<ITournamentTypeRepository, TournamentTypeRepository>();
            services.AddTransient<ITournamentRepository, TournamentRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IGoalscorerRepository, GoalscorerRepository>();
            services.AddTransient<ITeamStatRepository, TeamStatRepository>();
            services.AddTransient<ITeamStatWorldCupRepository, TeamStatWorldCupRepository>();
            services.AddTransient<IH2HRepository, H2HRepository>();
            services.AddTransient<IH2HWorldCupRepository, H2HWorldCupRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
        }

        private void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IMatchTypeService, MatchTypeService>();
            services.AddTransient<IConfederationService, ConfederationService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IRankingService, RankingService>();
            services.AddTransient<ITournamentTypeService, TournamentTypeService>();
            services.AddTransient<ITournamentService, TournamentService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IGoalscorerService, GoalscorerService>();
            services.AddTransient<ITeamStatService, TeamStatService>();
            services.AddTransient<ITeamStatWorldCupService, TeamStatWorldCupService>();
            services.AddTransient<IH2HService, H2HService>();
            services.AddTransient<IH2HWorldCupService, H2HWorldCupService>();
            services.AddTransient<IMatchService, MatchService>();
        }
    }
}
