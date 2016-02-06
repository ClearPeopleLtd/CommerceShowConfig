using Castle.Core.Resource;
using Castle.MicroKernel;
using Castle.MicroKernel.ModelBuilder;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Conversion;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ClearPeople.UCommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ClearPeople.uCommerce.Infrastructure
{
    internal class CpExtendedWindsorContainer : WindsorContainer
    {
        private string XmlPath;
        public CpExtendedWindsorContainer(string xmlFilePath) : base(CpExtendedWindsorContainer.CreateKernel(), new DefaultComponentInstaller())
        {
            if (xmlFilePath == null)
            {
                throw new ArgumentException("xmlFilePath");
            }
            XmlPath = xmlFilePath;
            this.Kernel.Resolver.AddSubResolver(new ListResolver(this.Kernel));
            this.Kernel.AddSubSystem(SubSystemConstants.ConversionManagerKey, new DefaultConversionManager());
            
        }
        public XmlNode GetConfig()
        {
            FileResource fileResource = new FileResource(XmlPath);
            return (new CpCustomXmlInterpreter(fileResource)).GetConfig(fileResource, this.Kernel.ConfigurationStore, this.Kernel);
            
        }

        private static IKernel CreateKernel()
        {
            DefaultKernel defaultKernel = new DefaultKernel();
            defaultKernel.ComponentModelBuilder = new DefaultComponentModelBuilder(defaultKernel);
            return defaultKernel;
        }
    }
}
