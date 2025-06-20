using Autofac;
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
        public string _connectionString { get; }
        public string _catalogConnectionString { get; set; }
        public ApplicationModule(string connectionString, string catalogConnectionString)
        {
            _connectionString = connectionString;        
            _catalogConnectionString = catalogConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
               .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces()
              .WithParameter(new TypedParameter(typeof(string), _connectionString));

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterType<CatalogRepository>()
                   .As<ICatalogRepository>()
                   .InstancePerLifetimeScope()
                   .WithParameter(new TypedParameter(typeof(string), _catalogConnectionString));
        }
    }
}
