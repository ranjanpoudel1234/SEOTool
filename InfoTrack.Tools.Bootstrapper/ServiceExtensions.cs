using System;
using InfoTrack.Tools.Domain.Interfaces;
using InfoTrack.Tools.Http.Interfaces;
using InfoTrack.Tools.Http.Repositories;
using InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Bing;
using InfoTrack.Tools.Http.Repositories.SearchEngineHandlers.Google;
using InfoTrack.Tools.Services;
using Microsoft.Extensions.DependencyInjection;


namespace InfoTrack.Tools.Bootstrapper
{
    public static class ServiceExtensions
    {
        public static void RegisterAppServicesAndRepositories(this IServiceCollection services)
        {
            //when these grow in number, they can be registered dynamically using reflection
            services.AddScoped<ISearchEngineService, SearchEngineService>();
            services.AddScoped<ISearchEngineRepository, SearchEngineRepository>();

            services.AddScoped<ISearchEngineHandler, GoogleSearchEngineHandler>();
            services.AddScoped<ISearchEngineHandler, BingSearchEngineHandler>();
        }
    }
}