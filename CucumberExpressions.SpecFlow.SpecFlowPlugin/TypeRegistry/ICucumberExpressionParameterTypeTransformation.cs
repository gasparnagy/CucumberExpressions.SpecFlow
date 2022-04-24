using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.TypeRegistry
{
    public interface ICucumberExpressionParameterTypeTransformation
    {
        string Name { get; }
        string Regex { get; }
        IBindingType TargetType { get; }
        bool UseForSnippets { get; }
        int Weight { get; }
    }
}