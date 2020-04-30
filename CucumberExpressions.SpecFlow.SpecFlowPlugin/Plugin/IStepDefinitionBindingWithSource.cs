using System;
using TechTalk.SpecFlow.Bindings;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public interface IStepDefinitionBindingWithSource : IStepDefinitionBinding
    {
        string SourceExpression { get; }

        bool IsValid { get; }
        string Error { get; }
    }
}
