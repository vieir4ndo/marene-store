using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MareneStore.Api
{
    public class DefaultDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly ServiceProvider _serviceProvider;

        public DefaultDependencyResolver(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => _serviceProvider.GetServices(serviceType);

        public System.Web.Http.Dependencies.IDependencyScope BeginScope() => this;

        public void Dispose() { }
    }
}