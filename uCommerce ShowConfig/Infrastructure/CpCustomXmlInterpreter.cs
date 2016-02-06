using Castle.Core.Resource;
using Castle.MicroKernel;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.MicroKernel.SubSystems.Resource;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Configuration.Interpreters.XmlProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UCommerce.Infrastructure.Components.Windsor;

namespace ClearPeople.UCommerce.Infrastructure
{
    public class CpCustomXmlInterpreter: CustomXmlInterpreter
    {
        public CpCustomXmlInterpreter(string filename) : base(filename)
        {
        }

        public CpCustomXmlInterpreter(IResource source) : base(source)
        {
        }
        public XmlNode GetConfig(IResource source, IConfigurationStore store, IKernel kernel)
        {
            CustomXmlProcessor customXmlProcessor = new CustomXmlProcessor(base.EnvironmentName, kernel.GetSubSystem(SubSystemConstants.ResourceKey) as IResourceSubSystem);
            try
            {
                XmlNode xmlNodes = customXmlProcessor.Process(source);
                (new PartialComponentElementMerger()).MergePartialComponentsIntoComponents(xmlNodes);
                return xmlNodes;


            }
            catch (XmlProcessorException xmlProcessorException)
            {
                throw new ConfigurationProcessingException("Unable to process xml resource.", xmlProcessorException);
            }
        }
    }
}
