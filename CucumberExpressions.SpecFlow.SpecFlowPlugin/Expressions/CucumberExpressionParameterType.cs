using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Bindings.Reflection;

namespace CucumberExpressions.SpecFlow.SpecFlowPlugin.Expressions
{
    public class CucumberExpressionParameterType
    {
        internal const string MatchAllRegex = @"(.*)";

        public string Name { get; }
        public IBindingType TargetType { get; }
        public ICucumberExpressionParameterTypeTransformation[] Transformations { get; }

        public string[] RegexStrings { get; }

        public CucumberExpressionParameterType(string name, IBindingType targetType, IEnumerable<ICucumberExpressionParameterTypeTransformation> transformations)
        {
            Name = name;
            TargetType = targetType;
            Transformations = transformations.ToArray();

            var regexStrings = Transformations.Select(tr => tr.Regex).Distinct().ToArray();
            if (regexStrings.Length > 1 && regexStrings.Contains(MatchAllRegex))
                regexStrings = new[] {MatchAllRegex};
            RegexStrings = regexStrings;
        }
    }
}
