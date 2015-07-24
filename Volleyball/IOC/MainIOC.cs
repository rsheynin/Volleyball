using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VB.Infrastructure.Services;

namespace VB.Console.IOC
{
    [ExcludeFromCodeCoverage]
    public class MainIOC : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISerializer>()
                                        .ImplementedBy<Serializer>());

            container.Register(Component.For<ILinqService>()
                                        .ImplementedBy<LinqService>());
            
            container.Register(Component.For<IRemoteFile>()
                                        .ImplementedBy<RemoteFile>());

            container.Register(Component.For<IPersistentService>()
                                        .ImplementedBy<PersistentService>());

            container.Register(Component.For<IIVFManager>()
                                        .ImplementedBy<IVFManager>());
        }
    }
}
