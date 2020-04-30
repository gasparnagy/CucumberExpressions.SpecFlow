using System;
using TechTalk.SpecFlow;

namespace CucumberExpressions.SpecFlow.TestProject
{
    [Binding]
    public class StepDefinitions
    {
        [Given("I registered as {}")]
        public void GivenIRegisteredAsPerson(Person person)
        {
            //...
        }

        [Given("I logged in as {Person}")]
        public void GivenILoggedInAsPerson(Person person)
        {
            //...
        }

        [When("I add {int} copy/copies of the book {string} into my basket")]
        public void WhenIAddCopyOfTheBookIntoMyBasket(int copies, string title)
        {
            //...
        }

        [Then("my basket should contain {Int32} book(s)")]
        public void ThenMyBasketShouldContainBooks(int expectedCopies)
        {
            //...
        }

        // fallback to regex
        [Given(@"^something$")]
        public void Something()
        {
            //...
        }

        // fallback to method-based
        [Given]
        public void Given_something_FOO_else(int foo)
        {

        }

        [StepArgumentTransformation]
        public Person PersonConverter(string name)
        {
            return new Person(name);
        }
    }
}
