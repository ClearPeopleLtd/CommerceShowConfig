using Castle.Core.Resource;
using Castle.Windsor;
using ClearPeople.uCommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Xml;
using UCommerce.Infrastructure;
using UCommerce.Infrastructure.Configuration;

namespace ClearPeople.UCommerce.Infrastructure
{
    public class CustomObjectFactory: ObjectFactory
    {
        public XmlNode GetConfig()
        {
            return this.Container.GetConfig();          
        }
        private string GetConfiguredConfig()
        {
            RuntimeConfigurationSection section = WebConfigurationManager.GetSection("commerce/runtimeConfiguration") as RuntimeConfigurationSection;
            if (section == null)
            {
                return null;
            }
            string componentsConfig = section.ComponentsConfig;
            if (!string.IsNullOrWhiteSpace(componentsConfig) && !(componentsConfig == "(auto)"))
            {
                return componentsConfig;
            }
            return null;
        }

        private CpExtendedWindsorContainer Container
        {
             get { return new CpExtendedWindsorContainer(this.GetConfiguredConfig() ?? this.AutoDiscoverConfig()); }
        }

    }
    
}
