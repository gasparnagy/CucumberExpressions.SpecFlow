using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class LateBoundStepDefinitionBindingWithSource : MethodBinding, IStepDefinitionBindingWithSource
    {
        private readonly Lazy<Regex> _regexProvider;

        public string ExpressionSource { get; }
        public bool IsValid => !Regex.ToString().StartsWith("error:");
        public string ErrorMessage => IsValid ? null : Regex.ToString();

        public bool IsScoped => BindingScope != null;
        public BindingScope BindingScope { get; }
        public StepDefinitionType StepDefinitionType { get; }
        public Regex Regex => _regexProvider.Value;

        public LateBoundStepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod, BindingScope bindingScope, string expressionSource, Func<Regex> regexFactory)
            : this(stepDefinitionType, bindingMethod, bindingScope, expressionSource, new Lazy<Regex>(regexFactory, true))
        {

        }

        protected LateBoundStepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod, BindingScope bindingScope, string expressionSource, Lazy<Regex> regexProvider) 
            : base(bindingMethod)
        {
            _regexProvider = regexProvider;
            StepDefinitionType = stepDefinitionType;
            BindingScope = bindingScope;
            ExpressionSource = expressionSource;
        }
    }
}