using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class StepDefinitionBindingWithSource : StepDefinitionBinding
    {
        public string ExpressionSource { get; }

        public StepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, Regex regex, IBindingMethod bindingMethod, BindingScope bindingScope, string expressionSource) : base(stepDefinitionType, regex, bindingMethod, bindingScope)
        {
            ExpressionSource = expressionSource;
        }
    }
}
