using System;
using System.Reflection;
using System.Text.RegularExpressions;
using CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class CustomBindingFactory : IBindingFactory
    {
        private readonly BindingFactory _defaultBindingFactory;
        private readonly StepDefinitionMatchExpressionConverter _definitionMatchExpressionConverter;

        public CustomBindingFactory(BindingFactory defaultBindingFactory, StepDefinitionMatchExpressionConverter definitionMatchExpressionConverter)
        {
            _defaultBindingFactory = defaultBindingFactory;
            _definitionMatchExpressionConverter = definitionMatchExpressionConverter;
        }

        public IHookBinding CreateHookBinding(IBindingMethod bindingMethod, HookType hookType, BindingScope bindingScope, int hookOrder)
        {
            return _defaultBindingFactory.CreateHookBinding(bindingMethod, hookType, bindingScope, hookOrder);
        }

        public IStepDefinitionBinding CreateStepBinding(StepDefinitionType type, string sourceExpression, IBindingMethod bindingMethod, BindingScope bindingScope)
        {
            return new LateBoundStepDefinitionBindingWithSource(type, bindingMethod, bindingScope, sourceExpression ?? bindingMethod.Name, () => CreateStepBindingRegex(type, sourceExpression, bindingMethod));
        }

        private Tuple<Regex, string> CreateStepBindingRegex(StepDefinitionType type, string expressionSource, IBindingMethod bindingMethod)
        {
            try
            {
                var expression = _definitionMatchExpressionConverter.CreateExpression(expressionSource, type, bindingMethod);
                return new Tuple<Regex, string>(expression.GetRegex(), null);
            }
            catch (Exception ex)
            {
                if (Assembly.GetEntryAssembly()?.FullName.StartsWith("deveroom") ?? false)
                    return new Tuple<Regex, string>(null, ex.Message);
                throw new CucumberExpressionException($"error: '{expressionSource}': {ex.Message}");
            }
        }

        public IStepArgumentTransformationBinding CreateStepArgumentTransformation(string regexString, IBindingMethod bindingMethod)
        {
            return _defaultBindingFactory.CreateStepArgumentTransformation(regexString, bindingMethod);
        }
    }
}
