using System;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;
using System.Text.RegularExpressions;
using RegexClass = System.Text.RegularExpressions.Regex;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public class UserDefinedCucumberExpressionParameterTypeTransformation : ICucumberExpressionParameterTypeTransformation
    {
        private readonly IStepArgumentTransformationBinding _stepArgumentTransformationBinding;

        public string Name => _stepArgumentTransformationBinding is CucumberExpressionParameterTypeBinding parameterTypeBinding ? parameterTypeBinding.Name : null; // later IStepArgumentTransformationBinding could capture name
        public string Regex { get; }
        public IBindingType TargetType => _stepArgumentTransformationBinding.Method.ReturnType;

        public UserDefinedCucumberExpressionParameterTypeTransformation(IStepArgumentTransformationBinding stepArgumentTransformationBinding)
        {
            _stepArgumentTransformationBinding = stepArgumentTransformationBinding;
            Regex = GetCucumberExpressionRegex(stepArgumentTransformationBinding);
        }

        private string GetCucumberExpressionRegex(IStepArgumentTransformationBinding stepArgumentTransformationBinding)
        {
            var regexString = stepArgumentTransformationBinding.Regex?.ToString().TrimStart('^').TrimEnd('$');
            if (regexString == null)
                return CucumberExpressionParameterType.MatchAllRegex;
            
            if (RegexClass.IsMatch(regexString, @"\(\?|\\\("))
                return CucumberExpressionParameterType.MatchAllRegex; // too complex regex

            var regexRemovedCaptureGroups = regexString.Replace("(", "(?:");
            var cucumberExpressionTypeRegex = $"({regexRemovedCaptureGroups})";

            if (!IsValidRegex(cucumberExpressionTypeRegex))
                return CucumberExpressionParameterType.MatchAllRegex; // too complex regex

            return cucumberExpressionTypeRegex;
        }

        private bool IsValidRegex(string regexString)
        {
            try
            {
                var _ = new RegexClass(regexString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}