using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.TypeRegistry
{
    public class BuiltInCucumberExpressionParameterTypeTransformation : ICucumberExpressionParameterTypeTransformation
    {
        public string Name { get; }
        public string Regex { get; }
        public IBindingType TargetType { get; }

        public BuiltInCucumberExpressionParameterTypeTransformation(string regex, IBindingType targetType, string name = null)
        {
            Regex = regex;
            TargetType = targetType;
            Name = name;
        }
    }
}