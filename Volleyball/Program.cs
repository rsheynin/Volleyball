using System;
using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using VB.Console.IOC;
using VB.Infrastructure.Models;
using VB.Infrastructure.Services;

namespace VB.Console
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        static void Main(string[] args)
        {

            var container = CreateWindsorContainer();
            //var serializer = container.Resolve<ISerializer>();
            //var linqService = container.Resolve<ILinqService>();
            //var remoteFile = container.Resolve<IRemoteFile>();
            //var persistentService = container.Resolve<IPersistentService>();
            var ivfmanager = container.Resolve<IIVFManager>();



            
            var clientmanager = new ClientManager(ivfmanager);

            //clientmanager.CreateMatchByClientData();
            clientmanager.CreateCoachByClientData(new Guid());

            //clientmanager.GetMatchById();
            //clientmanager.GetMatchByDate();
            //clientmanager.GetMatchByTeamName();

            //var player = clientmanager.CreatePlayerByClientData();
            //var team = clientmanager.CreateTeamByClientData();

//            ivf.Persistent.UpdateobjinData<P layer>(player, "player.json");
//            ivf.Persistent.DeleteodjfromData<Player>(player.Id, "player.json");

            System.Console.ReadKey(); 
        }

        private static IWindsorContainer CreateWindsorContainer()
        {
            var ioc = new MainIOC();
            IWindsorContainer container = new WindsorContainer();
            IConfigurationStore configurationStore = new DefaultConfigurationStore();
            ioc.Install(container, configurationStore);
            return container;
        }
    }
}
