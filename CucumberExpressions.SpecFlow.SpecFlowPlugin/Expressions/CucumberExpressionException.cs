using System;
using System.Runtime.Serialization;
using TechTalk.SpecFlow;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public class CucumberExpressionException : SpecFlowException
    {
        public CucumberExpressionException(string message) : base(message)
        {
        }

        public CucumberExpressionException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CucumberExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}