using System;
using System.Text.RegularExpressions;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public interface IStepDefinitionMatchExpression
    {
        //List<Argument<?>> match(string text, Type... typeHints);
        Regex GetRegex();
        string GetSource();
    }
}
