using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class LateBoundStepDefinitionBindingWithSource : MethodBinding, IStepDefinitionBindingWithSource
    {
        private readonly Lazy<Tuple<Regex, string>> _regexProvider;

        public string ExpressionSource { get; }
        public bool IsValid => _regexProvider.Value.Item2 == null;
        public string ErrorMessage => _regexProvider.Value.Item2;

        public bool IsScoped => BindingScope != null;
        public BindingScope BindingScope { get; }
        public StepDefinitionType StepDefinitionType { get; }
        public Regex Regex => _regexProvider.Value.Item1 ?? GetErrorRegex();

        private Regex GetErrorRegex()
        {
            return new Regex($"error: '{Regex.Escape(ExpressionSource)}': {Regex.Escape(ErrorMessage)}|.*".Replace(@"\ ", " ").Replace(@"\{", "{"));
        }

        public LateBoundStepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod, BindingScope bindingScope, string expressionSource, Func<Tuple<Regex, string>> regexFactory)
            : this(stepDefinitionType, bindingMethod, bindingScope, expressionSource, new Lazy<Tuple<Regex, string>>(regexFactory, true))
        {
        }

        protected LateBoundStepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod, BindingScope bindingScope, string expressionSource, Lazy<Tuple<Regex, string>> regexProvider) 
            : base(bindingMethod)
        {
            _regexProvider = regexProvider;
            StepDefinitionType = stepDefinitionType;
            BindingScope = bindingScope;
            ExpressionSource = expressionSource;
        }
    }
}