using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Logging;
using Module = Autofac.Module;

namespace WeatherFF.Loader
{
    public class LoggingModule : Module
    {
        private readonly LoggerFactory _loggerFactory;

        public LoggingModule(Action<ILoggerFactory> initialize)
        {
            _loggerFactory = new LoggerFactory();
            initialize?.Invoke(_loggerFactory);
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => _loggerFactory.CreateLogger("Default")).As<ILogger>();
            base.Load(builder);
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry,
            IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;

            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }

        private void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, _loggerFactory.CreateLogger(instanceType), null);
            }
        }

        private void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            var t = e.Component.Activator.LimitType;

            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter((p, i) => p.ParameterType == typeof (ILogger), (p, i) => _loggerFactory.CreateLogger(t))
                });
        }
    }
}