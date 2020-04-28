using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public interface ICucumberExpressionParameterTypeTransformation
    {
        string Name { get; }
        string Regex { get; }
        IBindingType TargetType { get; }
    }
}