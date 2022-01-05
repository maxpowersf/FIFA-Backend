using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ranking.Application.Implementations;
using Ranking.Application.Interfaces;
using Ranking.Application.Repositories;
using Ranking.Data;
using Ranking.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ranking.Sync
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        static void Main(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                                .SetBasePath(Path.Combine(AppContext.BaseDirectory));

            if(environment == "Production")
            {
                builder.AddJsonFile($"appsettings.{environment}.json", optional: false);
            }
            else
            {
                builder.AddJsonFile($"appsettings.json", optional: false);
            }

            Configuration = builder.Build();

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<Sync>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<RankingContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(Program));

            ConfigureRepositories(services);
            ConfigureApplicationServices(services);

            services.AddTransient<Sync>();

            return services;
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<ITournamentRepository, TournamentRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<ITournamentTypeRepository, TournamentTypeRepository>();
            services.AddTransient<IGoalscorerRepository, GoalscorerRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
        }

        private static void ConfigureApplicationServices(IServiceCollection services)
        {
            services.AddTransient<ITournamentService, TournamentService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IGoalscorerService, GoalscorerService>();
        }
    }
}
