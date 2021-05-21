using System;
using InfoTrack.Tools.Domain.Interfaces;
using InfoTrack.Tools.Http.Repositories;
using InfoTrack.Tools.Services;
using Microsoft.Extensions.DependencyInjection;


namespace InfoTrack.Tools.Bootstrapper
{
    public static class ServiceExtensions
    {
        public static void RegisterServicesAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISearchEngineService, SearchEngineService>();
            services.AddScoped<ISearchEngineRepository, SearchEngineRepository>();
        }
    }
}