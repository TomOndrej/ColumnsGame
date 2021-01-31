using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ColumnsGame.Ioc.Attributes;
using Prism.Ioc;
using Unity;
using Unity.RegistrationByConvention;

namespace ColumnsGame.Ioc
{
    public static class ContainerExtensions
    {
        public static void RegisterNavigations(this IContainerRegistry containerRegistry, Assembly assembly)
        {
            var assemblyTypes = GetAssemblyTypes(assembly);

            foreach (Type type in assemblyTypes)
            {
                if (type.GetCustomAttribute<IocRegisterNavigationAttribute>() is null)
                {
                    continue;
                }

                containerRegistry.RegisterForNavigation(type, type.Name);
            }
        }

        public static void RegisterDependencies(this IUnityContainer container, Assembly assembly)
        {
            var assemblyTypes = GetAssemblyTypes(assembly);

            List<Type> typesToRegister = new List<Type>();

            foreach (Type type in assemblyTypes)
            {
                if (type.GetCustomAttribute<IocRegisterImplementationAttribute>() is null)
                {
                    continue;
                }

                typesToRegister.Add(type);
            }

            container.RegisterTypes(typesToRegister, WithMappings.FromAllInterfaces, WithName.Default,
                WithLifetime.Transient, overwriteExistingMappings: true);
        }

        private static IEnumerable<Type> GetAssemblyTypes(Assembly assembly)
        {
            return 
                assembly
                    .GetTypes()
                    .Select(t => t.GetTypeInfo())
                    .Where(t => !t.IsAbstract && t.DeclaredConstructors.Any(c => !c.IsStatic && c.IsPublic))
                    .Select(t => t.AsType());
        }
    }
}
