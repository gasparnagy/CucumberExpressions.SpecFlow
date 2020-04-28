# CucumberExpressions.SpecFlow

[Cucumber expressions](https://cucumber.io/docs/cucumber/cucumber-expressions/) support for SpecFlow v3.1 (SpecFlow plugin). 

_Note: Cucumber expression support will be hopefully added to the core SpecFlow package in a future version. But for the current SpecFlow versions, it can be already used using this plugin._

With Cucumber expressions you can specify your step definitions in a simpler way.

Instead of using regular expressions:

```
[When(@"I add (\d+) copies of the book ""([^""])"" into my basket")]
```
  
Use Cucumber expressions:

```
[When("I add {int} copies of the book {string} into my basket")]
```
  
## Installation 

Just simply add the `CucumberExpressions.SpecFlow.3-1` NuGet package to your SpecFlow v3.1 project!

```
PM> Install-Package CucumberExpressions.SpecFlow.3-1
```

## Features

The plugin supports most of the features described in the [Cucumber expressions documentation](https://cucumber.io/docs/cucumber/cucumber-expressions/). You can use `{int}`, `{float}`, `{word}`, `{string}` and also the generic `{}` anonymous matching pattern. SpecFlow detects the parameter type of your method and performs the necessary conversation anyway.

You can also use the most common built-in types by (case sensitive) type name (without namespace): `{Boolean}`, `{Byte}`, `{Int32}`, `{Decimal}`, `{Guid}`, `{DateTime}`, etc.

The custom conversions can be used either with the anonymous parameter `{}` or with the target type name, like `{Person}`. To specify the conversion, you need to use the usual `[StepArgumentTransformation]` attribute:

```
[StepArgumentTransformation]
public Person ConvertPerson(string name)
{
  //...
}
```

## Compatibility

It is mostly backwards compatible as it recognizes common regex patterns and uses regex mathching for them. If necessary you can force using the regex matcher by wrapping your pattern with `^` and `$`.

```
[When(@"^forced regex$")]
```

## IDE Support

To be able to recognize Cucumber expression in your Visual Studio, the Visual Studio integration also has to be updated. Currently the [Devreoom for SpecFlow](https://github.com/specsolutions/deveroom-visualstudio) (free, open-source) Visual Studio extension supports this new patterns.


