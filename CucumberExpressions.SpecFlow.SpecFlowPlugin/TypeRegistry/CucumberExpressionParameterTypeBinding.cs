using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.TypeRegistry
{
    public class CucumberExpressionParameterTypeBinding : StepArgumentTransformationBinding
    {
        public string Name { get; }

        public CucumberExpressionParameterTypeBinding(string regexString, IBindingMethod bindingMethod, string name) : base(regexString, bindingMethod)
        {
            Name = name;
        }
    }
}