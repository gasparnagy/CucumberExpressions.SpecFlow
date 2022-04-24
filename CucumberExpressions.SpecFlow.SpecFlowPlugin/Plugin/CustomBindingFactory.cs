﻿using System;
using System.Reflection;
using System.Text.RegularExpressions;
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
                return new Tuple<Regex, string>(expression.Regex, null);
            }
            catch (Exception ex)
            {
                if (IsInvokedFromIde())
                    return new Tuple<Regex, string>(null, ex.Message);
                throw new CucumberExpressionException($"error: '{expressionSource}': {ex.Message}");
            }
        }

        private static bool IsInvokedFromIde()
        {
            var assemblyFullName = Assembly.GetEntryAssembly()?.FullName;
            return assemblyFullName != null && 
                   (assemblyFullName.StartsWith("deveroom") || assemblyFullName.StartsWith("specflow-vs"));
        }

        public IStepArgumentTransformationBinding CreateStepArgumentTransformation(string regexString, IBindingMethod bindingMethod)
        {
            return _defaultBindingFactory.CreateStepArgumentTransformation(regexString, bindingMethod);
        }
    }
}
