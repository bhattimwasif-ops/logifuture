using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using WalletApi.Models;
using WalletApi.Services;

namespace WalletApi.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<WalletDbContext>()
                .InstancePerRequest();

            builder.RegisterType<WalletService>()
                .As<IWalletService>()
                .InstancePerRequest();

            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}