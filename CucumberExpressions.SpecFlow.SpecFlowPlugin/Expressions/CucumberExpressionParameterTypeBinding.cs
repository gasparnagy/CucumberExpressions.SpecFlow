using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
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