using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    //converted from https://github.com/cucumber/cucumber/blob/master/cucumber-expressions/java/src/main/java/io/cucumber/cucumberexpressions/CucumberExpression.java
    public class CucumberExpression : IStepDefinitionMatchExpression
    {
        // Does not include (){} characters because they have special meaning
        private static readonly Regex ESCAPE_PATTERN = new Regex("([\\\\^\\[$.|?*+\\]])");
        static readonly Regex PARAMETER_PATTERN = new Regex("(\\\\\\\\)?\\{([^}]*)\\}");
        private static readonly Regex OPTIONAL_PATTERN = new Regex("(\\\\\\\\)?\\(([^)]+)\\)");
        private static readonly Regex ALTERNATIVE_NON_WHITESPACE_TEXT_REGEXP = new Regex("([^\\s^/]+)((/[^\\s^/]+)+)");
        private static readonly string DOUBLE_ESCAPE = "\\\\";

        private static readonly Regex ILLEGAL_PARAMETER_NAME_PATTERN = new Regex("([\\[\\]()$.|?*+])");
        private static readonly Regex UNESCAPE_PATTERN = new Regex("(\\\\([\\[$.|?*+\\]]))");


        private static readonly string PARAMETER_TYPES_CANNOT_BE_ALTERNATIVE =
            "Parameter types cannot be alternative: ";

        private static readonly string PARAMETER_TYPES_CANNOT_BE_OPTIONAL = "Parameter types cannot be optional: ";

        private readonly string _source;
        private readonly Regex _regex;
        private readonly CucumberExpressionParameterTypeRegistry _typeRegistry;

        public CucumberExpression(string expression, CucumberExpressionParameterTypeRegistry typeRegistry)
        {
            this._source = expression;
            _typeRegistry = typeRegistry;

            expression = ProcessEscapes(expression);
            expression = ProcessOptional(expression);
            expression = ProcessAlternation(expression);
            expression = ProcessParameters(expression);
            expression = "^" + expression + "$";
            _regex = new Regex(expression);
        }

        private string ProcessEscapes(string expression)
        {
            return ESCAPE_PATTERN.Replace(expression, "\\$1");
        }

        private string ProcessAlternation(string expression)
        {
            var matcher = ALTERNATIVE_NON_WHITESPACE_TEXT_REGEXP.Matches(expression);
            var sb = new StringBuilder();
            int lastNonMatchIndex = 0;
            foreach (Match match in matcher)
            {
                sb.Append(expression.Substring(lastNonMatchIndex, match.Index - lastNonMatchIndex));
                lastNonMatchIndex = match.Index + match.Length;

                // replace \/ with /
                // replace / with |
                string replacement = match.Groups[0].Value.Replace('/', '|').Replace("\\\\\\|", "/");

                if (replacement.Contains("|"))
                {
                    // Make sure the alternative parts don't contain parameter types
                    foreach (var part in replacement.Split(new[] { "\\|" }, StringSplitOptions.None))
                    {
                        CheckNotParameterType(part, PARAMETER_TYPES_CANNOT_BE_ALTERNATIVE);
                    }

                    sb.Append("(?:" + replacement + ")");
                }
                else
                {
                    // All / were escaped
                    sb.Append(replacement);
                }
            }

            sb.Append(expression.Substring(lastNonMatchIndex));
            return sb.ToString();
        }

        private void CheckNotParameterType(string s, string message)
        {
            if (PARAMETER_PATTERN.IsMatch(s))
            {
                throw new CucumberExpressionException(message + _source);
            }
        }

        private string ProcessOptional(string expression)
        {
            var matcher = OPTIONAL_PATTERN.Matches(expression);
            var sb = new StringBuilder();
            int lastNonMatchIndex = 0;
            foreach (Match match in matcher)
            {
                sb.Append(expression.Substring(lastNonMatchIndex, match.Index - lastNonMatchIndex));
                lastNonMatchIndex = match.Index + match.Length;

                // look for double-escaped parentheses
                string parameterPart = match.Groups[2].Value;
                if (DOUBLE_ESCAPE.Equals(match.Groups[1].Value))
                {
                    sb.Append("\\\\(" + parameterPart + "\\\\)");
                }
                else
                {
                    CheckNotParameterType(parameterPart, PARAMETER_TYPES_CANNOT_BE_OPTIONAL);
                    sb.Append("(?:" + parameterPart + ")?");
                }
            }

            sb.Append(expression.Substring(lastNonMatchIndex));
            return sb.ToString();
        }

        public static void CheckParameterTypeName(string typeName)
        {
            var unescapedTypeName = UNESCAPE_PATTERN.Replace(typeName, "$2");
            var match = ILLEGAL_PARAMETER_NAME_PATTERN.Match(unescapedTypeName);
            if (match.Success)
            {
                throw new CucumberExpressionException($"Illegal character '{match.Groups[1].Value}' in parameter name {unescapedTypeName}.");
            }
        }

        private string ProcessParameters(string expression)
        {
            var matcher = PARAMETER_PATTERN.Matches(expression);
            var sb = new StringBuilder();
            int lastNonMatchIndex = 0;
            foreach (Match match in matcher)
            {
                sb.Append(expression.Substring(lastNonMatchIndex, match.Index - lastNonMatchIndex));
                lastNonMatchIndex = match.Index + match.Length;

                if (DOUBLE_ESCAPE.Equals(match.Groups[1].Value))
                {
                    sb.Append("\\\\{" + match.Groups[2].Value + "\\\\}");
                }
                else
                {
                    string typeName = match.Groups[2].Value;
                    CheckParameterTypeName(typeName);
                    var parameterType = _typeRegistry.GetByName(typeName);
                    if (parameterType == null)
                    {
                        throw new CucumberExpressionException($"Undefined parameter type: '{typeName}'");
                    }

                    sb.Append(BuildCaptureRegexp(parameterType.RegexStrings.ToArray()));
                }
            }

            sb.Append(expression.Substring(lastNonMatchIndex));
            return sb.ToString();
        }

        private string BuildCaptureRegexp(string[] regexps)
        {
            if (regexps.Length == 1)
            {
                return regexps[0];
            }
            else
            {
                StringBuilder sb = new StringBuilder("(");
                bool bar = false;
                foreach (var captureGroupRegexp in regexps)
                {
                    if (bar) sb.Append("|");
                    var grouplessRegex = Regex.Replace(captureGroupRegexp, @"(?<!\\)\((?!\?\:)", "(?:");
                    sb.Append("(?:").Append(grouplessRegex).Append(")");
                    bar = true;
                }

                sb.Append(")");
                return sb.ToString();
            }
        }

        //public List<Argument<?>> match(string text, Type...typeHints)
        //{
        //    readonly Group group =
        //    treeRegexp.match(text);
        //    if (group == null)
        //    {
        //        return null;
        //    }

        //    List < ParameterType < ?>> parameterTypes = new ArrayList<>(this.parameterTypes);
        //    for (int i = 0; i < parameterTypes.size(); i++)
        //    {
        //        ParameterType < ?> parameterType = parameterTypes.get(i);
        //        Type type = i < typeHints.length ? typeHints[i] : string.class;
        //        if (parameterType.isAnonymous())
        //        {
        //            ParameterByTypeTransformer defaultTransformer =
        //                parameterTypeRegistry.getDefaultParameterTransformer();
        //            parameterTypes.set(i,
        //                parameterType.deAnonymize(type, arg->defaultTransformer.transform(arg, type)));
        //        }
        //    }

        //    return Argument.build(group, treeRegexp, parameterTypes);
        //}

        public Regex GetRegex()
        {
            return _regex;
        }

        public string GetSource()
        {
            return _source;
        }
    }
}