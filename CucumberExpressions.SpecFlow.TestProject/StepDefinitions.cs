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

        [StepArgumentTransformation]
        public Person PersonConverter(string name)
        {
            return new Person(name);
        }
    }
}
