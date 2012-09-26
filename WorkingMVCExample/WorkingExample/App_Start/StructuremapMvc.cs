using System.Web.Http;
using System.Web.Mvc;
using StructureMap;
using StructureMap.ServiceLocatorAdapter;

[assembly: WebActivator.PreApplicationStartMethod(typeof(WorkingExample.App_Start.StructuremapMvc), "Start")]

namespace WorkingExample.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}