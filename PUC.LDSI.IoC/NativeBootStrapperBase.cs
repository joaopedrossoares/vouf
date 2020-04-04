﻿
using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.Application.AppServices;
using PUC.LDSI.Application.Interfaces;
using PUC.LDSI.DataBase.Repository;
using PUC.LDSI.Domain.Interfaces.Repository;
using PUC.LDSI.Domain.Interfaces.Services;
using PUC.LDSI.Domain.Services;

namespace PUC.LDSI.IoC
{
    public abstract class NativeBootStrapperBase
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Example
            //services.AddScoped<IClasseAppService, ClasseAppService>();

            //Application
            services.AddScoped<ITurmaAppService, TurmaAppService>();

            //Domain - Repository
            services.AddScoped<ITurmaRepository, TurmaRepository>();

            //Domain - Services
            services.AddScoped<ITurmaService, TurmaService>();
        }
    }
}