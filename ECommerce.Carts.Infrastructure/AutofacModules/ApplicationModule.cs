﻿using Autofac;
using ECommerce.Carts.Domain.IRepositories;
using ECommerce.Carts.Domain.IServices;
using ECommerce.Carts.Infrastructure.Repositories;
using ECommerce.Carts.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = Autofac.Module;

namespace Ecommerce.Carts.Infrastructure.AutofacModules
{
    public class ApplicationModule:Module
    {
        public string _catalogConnectionString { get; }
        public string _orderConnectionString { get; }
        public ApplicationModule(string catalogConnectionString,string orderConnectionString)
        {      
            _catalogConnectionString = catalogConnectionString;
            _orderConnectionString = orderConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //builder.RegisterAssemblyTypes(assembly)
            //   .Where(t => t.Name.EndsWith("Repository"))
            //  .AsImplementedInterfaces()
            //  .WithParameter(new TypedParameter(typeof(string), _connectionString));

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterType<CatalogRepository>()
                   .As<ICatalogRepository>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(typeof(string), _catalogConnectionString));

            builder.RegisterType<OrderRepository>()
                   .As<IOrderRepository>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(typeof(string), _orderConnectionString));

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var redis = c.Resolve<IConnectionMultiplexer>();
                var expireMinutes = config.GetValue<int>("Cart:ExpireMinutes");

                return new RedisCartService(redis, TimeSpan.FromMinutes(expireMinutes));
            }).As<IRedisCartService>().InstancePerLifetimeScope();
        }
    }
}
