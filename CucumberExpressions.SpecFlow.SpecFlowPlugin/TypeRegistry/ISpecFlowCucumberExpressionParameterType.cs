using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.TypeRegistry
{
    public interface ISpecFlowCucumberExpressionParameterType : IParameterType
    {
        IBindingType TargetType { get; }
        ICucumberExpressionParameterTypeTransformation[] Transformations { get; }
    }
}