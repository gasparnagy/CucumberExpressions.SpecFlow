using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public class UserDefinesCucumberExpressionParameterTypeTransformation : ICucumberExpressionParameterTypeTransformation
    {
        private readonly IStepArgumentTransformationBinding _stepArgumentTransformationBinding;

        public string Name => _stepArgumentTransformationBinding is CucumberExpressionParameterTypeBinding parameterTypeBinding ? parameterTypeBinding.Name : null; // later IStepArgumentTransformationBinding could capture name
        public string Regex => _stepArgumentTransformationBinding.Regex?.ToString().TrimStart('^').TrimEnd('$') ?? CucumberExpressionParameterType.MatchAllRegex;
        public IBindingType TargetType => _stepArgumentTransformationBinding.Method.ReturnType;

        public UserDefinesCucumberExpressionParameterTypeTransformation(IStepArgumentTransformationBinding stepArgumentTransformationBinding)
        {
            _stepArgumentTransformationBinding = stepArgumentTransformationBinding;
        }
    }
}