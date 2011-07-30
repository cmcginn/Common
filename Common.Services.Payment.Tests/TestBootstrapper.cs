using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.ServiceLocation;
namespace Common.Services.Payment.Tests
{
    public class TestBootstrapper
    {
        IUnityContainer _Container;

        public IUnityContainer Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        public void Run()
        {
            _Container = new UnityContainer();
            UnityConfigurationSection section
                 = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");
            section.Configure(_Container);
        }
    }
}
