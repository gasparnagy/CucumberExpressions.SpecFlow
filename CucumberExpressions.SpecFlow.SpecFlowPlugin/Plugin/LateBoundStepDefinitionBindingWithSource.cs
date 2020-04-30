using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class LateBoundStepDefinitionBindingWithSource : MethodBinding, IStepDefinitionBindingWithSource
    {
        private readonly Lazy<Tuple<Regex, string>> _regexProvider;

        public string SourceExpression { get; }
        public bool IsValid => Error == null;
        public string Error => _regexProvider.Value.Item2;

        public bool IsScoped => BindingScope != null;
        public BindingScope BindingScope { get; }
        public StepDefinitionType StepDefinitionType { get; }
        public Regex Regex => _regexProvider.Value.Item1 ?? GetErrorRegex();

        private Regex GetErrorRegex()
        {
            return new Regex($"error: '{Regex.Escape(SourceExpression)}': {Regex.Escape(Error)}|.*".Replace(@"\ ", " ").Replace(@"\{", "{"));
        }

        public LateBoundStepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod, BindingScope bindingScope, string sourceExpression, Func<Tuple<Regex, string>> regexFactory)
            : this(stepDefinitionType, bindingMethod, bindingScope, sourceExpression, new Lazy<Tuple<Regex, string>>(regexFactory, true))
        {
        }

        protected LateBoundStepDefinitionBindingWithSource(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod, BindingScope bindingScope, string sourceExpression, Lazy<Tuple<Regex, string>> regexProvider) 
            : base(bindingMethod)
        {
            _regexProvider = regexProvider;
            StepDefinitionType = stepDefinitionType;
            BindingScope = bindingScope;
            SourceExpression = sourceExpression;
        }
    }
}