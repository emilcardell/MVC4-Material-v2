using Raven.Client;
using Raven.Client.Document;
using StructureMap;
namespace WorkingExample {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<IDocumentStore>().Singleton().Use(CurrentDocumentStore());
                            x.For<IDocumentSession>().HybridHttpOrThreadLocalScoped().Use(y => y.GetInstance<IDocumentStore>().OpenSession());
                        });
            return ObjectFactory.Container;
        }

        public static IDocumentStore CurrentDocumentStore()
        {
            var documentStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
            }.Initialize();

            return documentStore;
        }
    }
}