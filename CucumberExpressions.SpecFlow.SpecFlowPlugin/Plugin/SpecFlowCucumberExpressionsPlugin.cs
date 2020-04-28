using System;
using CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(SpecFlowCucumberExpressionsPlugin))]

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class SpecFlowCucumberExpressionsPlugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.RegisterGlobalDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterTypeAs<CustomBindingFactory, IBindingFactory>();
            };
        }
    }
}
