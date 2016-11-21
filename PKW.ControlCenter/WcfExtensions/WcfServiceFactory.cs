using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using PKW.ControlCenter.Voting;
using Unity.Wcf;

namespace PKW.ControlCenter.WcfExtensions
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            UnityBootstrapper.Register(container);
            DataInit.Load(container.Resolve<IDataRepository>());
        }        
    }
}