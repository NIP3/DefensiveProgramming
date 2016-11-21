using System.Diagnostics;
using Microsoft.Practices.Unity;
using PKW.Contracts;
using PKW.ControlCenter.Voting;

namespace PKW.ControlCenter
{
    public class UnityBootstrapper
    {
        public static void Register(IUnityContainer container)
        {
            container
                .RegisterType<IVotingService, VotingService>()
                .RegisterType<IDataRepository, DataRepository>(new ContainerControlledLifetimeManager());
        }
    }
}