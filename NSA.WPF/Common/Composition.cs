using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace NSA.WPF.Common
{
    public static class Composition
    {
        private static readonly AggregateCatalog _aggregateCatalog = new AggregateCatalog();
        private static readonly CompositionContainer _container = new CompositionContainer(_aggregateCatalog);

        public static void RegisterAssembly(Assembly assembly)
        {
            _aggregateCatalog.Catalogs.Add(new AssemblyCatalog(assembly));
        }

        public static T Resolve<T>()
        {
            return _container.GetExportedValue<T>();
        }

        public static T Resolve<T>(string contract)
        {
            return _container.GetExportedValue<T>(contract);
        }

        public static void ResolveDependecies(object obj)
        {
            _container.SatisfyImportsOnce(obj);
        }
    }
}
