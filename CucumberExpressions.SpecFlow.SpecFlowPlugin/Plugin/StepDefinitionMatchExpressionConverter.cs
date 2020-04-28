using System;
using System.Text.RegularExpressions;
using CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class StepDefinitionMatchExpressionConverter
    {
        private static readonly Regex ParameterPlaceholder = new Regex(@"{\w*}");
        private static readonly Regex CommonRegexStepDefPatterns = new Regex(@"(\([^\)]+[\*\+]\)|\.\*)");

        private readonly IStepDefinitionRegexCalculator _stepDefinitionRegexCalculator;
        private readonly CucumberExpressionParameterTypeRegistry _cucumberExpressionParameterTypeRegistry;

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

        public IStepDefinitionMatchExpression CreateExpression(string expressionString, StepDefinitionType type, IBindingMethod bindingMethod)
        {
            if (expressionString == null || !IsCucumberExpression(expressionString))
            {
                if (expressionString == null)
                    expressionString = _stepDefinitionRegexCalculator.CalculateRegexFromMethod(type, bindingMethod);

                return new RegexExpression(expressionString);
            }

            return new CucumberExpression(expressionString, _cucumberExpressionParameterTypeRegistry);
        }
    }
}
