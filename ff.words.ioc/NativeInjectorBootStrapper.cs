namespace ff.words.ioc
{
    using AutoMapper;
    using ff.words.application.Interfaces;
    using ff.words.application.Services;
    using ff.words.data.Context;
    using ff.words.data.Interfaces;
    using ff.words.data.Repository;
    using ff.words.data.UoW;
    using Microsoft.Extensions.DependencyInjection;

    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            // services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            // services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>(); ;

            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));

            services.AddScoped<IEntryService, EntrySerivce>();

            // Infra - Data
            services.AddScoped<IEntryRepository, EntryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<FFWordsContext>();
        }
    }
}
