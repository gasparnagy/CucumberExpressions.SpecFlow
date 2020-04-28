using System.Text.RegularExpressions;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public class RegexExpression : IStepDefinitionMatchExpression
    {
        private readonly string _source;

        public RegexExpression(string source)
        {
            _source = source;
        }

        public Regex GetRegex()
        {
            var regexSource = _source;
            if (!regexSource.StartsWith("^"))
                regexSource = "^" + regexSource;
            if (!regexSource.EndsWith("$"))
                regexSource = regexSource + "$";
            return new Regex(regexSource, RegexOptions.CultureInvariant);
        }

        public string GetSource()
        {
            return _source;
        }
    }
}