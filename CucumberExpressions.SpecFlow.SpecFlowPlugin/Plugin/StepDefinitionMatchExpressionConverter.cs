using System;
using System.Text.RegularExpressions;
using CucumberExpressions.SpecFlow.SpecFlowPlugin.TypeRegistry;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class StepDefinitionMatchExpressionConverter
    {
        private static readonly Regex ParameterPlaceholder = new Regex(@"{\w*}");
        private static readonly Regex CommonRegexStepDefPatterns = new Regex(@"(\([^\)]+[\*\+]\)|\.\*)");

        private readonly IStepDefinitionRegexCalculator _stepDefinitionRegexCalculator;
        private readonly IParameterTypeRegistry _cucumberExpressionParameterTypeRegistry;

        public StepDefinitionMatchExpressionConverter(IStepDefinitionRegexCalculator stepDefinitionRegexCalculator, CucumberExpressionParameterTypeRegistry cucumberExpressionParameterTypeRegistry)
        {
            _stepDefinitionRegexCalculator = stepDefinitionRegexCalculator;
            _cucumberExpressionParameterTypeRegistry = cucumberExpressionParameterTypeRegistry;
        }

        public bool IsCucumberExpression(string cucumberExpressionCandidate)
        {
            if (cucumberExpressionCandidate.StartsWith("^") || cucumberExpressionCandidate.EndsWith("$"))
                return false;

            if (ParameterPlaceholder.IsMatch(cucumberExpressionCandidate))
                return true;

            if (CommonRegexStepDefPatterns.IsMatch(cucumberExpressionCandidate))
                return false;

            return true;
        }

        public IExpression CreateExpression(string expressionString, StepDefinitionType type, IBindingMethod bindingMethod)
        {
            if (expressionString == null || !IsCucumberExpression(expressionString))
            {
                if (expressionString == null)
                    expressionString = _stepDefinitionRegexCalculator.CalculateRegexFromMethod(type, bindingMethod);

                return new RegularExpression(new Regex(expressionString));
            }

            return new CucumberExpression(expressionString, _cucumberExpressionParameterTypeRegistry);
        }
    }
}
