using System.Collections;
using System.Configuration;
using System.Reflection;
using System.Xml;

namespace Quixo.Engine
{
	/*
<configuration>
  <configSections>
	  <section name="QuixoEngines"
				  type="Quixo.Engine.EngineConfigurationSectionHandler, Quixo.Engine" />
  </configSections>
  <QuixoEngines>
		<Assembly value="Quixo.Test.Engine.dll" />
  </QuixoEngines>
	*/
	public sealed class EngineConfigurationSectionHandler
		: IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			//  TODO (4/12/2005): Need some defensive coding + logging to ensure types can be created, etc.
			var engines = new ArrayList();

			var engineAssemblyNodes = section.SelectNodes("./Assembly");

			foreach (XmlNode engineAssemblyNode in engineAssemblyNodes)
			{
				var engineAssembly = Assembly.Load(engineAssemblyNode.Attributes["value"].Value);
				var typesInEngineAssembly = engineAssembly.GetTypes();
				var baseEngineType = typeof(BaseEngine);

				foreach (var typeInEngineAssembly in typesInEngineAssembly)
				{
					if (baseEngineType.IsAssignableFrom(typeInEngineAssembly) &&
						 baseEngineType != typeInEngineAssembly)
					{
						engines.Add(typeInEngineAssembly);
					}
				}
			}

			return engines;
		}
	}
}
