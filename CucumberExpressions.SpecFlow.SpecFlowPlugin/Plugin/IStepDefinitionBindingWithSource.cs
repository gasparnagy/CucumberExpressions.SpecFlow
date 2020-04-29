using System;
using TechTalk.SpecFlow.Bindings;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public interface IStepDefinitionBindingWithSource : IStepDefinitionBinding
    {
        string ExpressionSource { get; }

        bool IsValid { get; }
        string ErrorMessage { get; }
    }
}
