using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Volleyball.Services;

namespace Volleyball.IOC
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
        }
    }
}
