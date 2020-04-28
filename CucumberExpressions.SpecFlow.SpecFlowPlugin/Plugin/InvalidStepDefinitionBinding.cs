using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Plugin
{
    public class InvalidStepDefinitionBinding : MethodBinding, IStepDefinitionBinding
    {
        public string ErrorMessage { get; }

        public bool IsScoped => BindingScope != null;
        public BindingScope BindingScope { get; }
        public StepDefinitionType StepDefinitionType { get; }
        public Regex Regex { get; }

        public InvalidStepDefinitionBinding(StepDefinitionType stepDefinitionType, IBindingMethod bindingMethod,
            BindingScope bindingScope, string errorMessage, string expressionSource)
            : base(bindingMethod)
        {
            StepDefinitionType = stepDefinitionType;
            Regex = new Regex($"error: '{Regex.Escape(expressionSource)}': {Regex.Escape(errorMessage)}|.*".Replace(@"\ ", " ").Replace(@"\{", "{"));
            BindingScope = bindingScope;
            ErrorMessage = errorMessage;
        }
    }
}
