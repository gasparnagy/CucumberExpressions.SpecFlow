using System;
using TechTalk.SpecFlow;
using Xunit;

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

        [Then("they cost ${int} in total")]
        public void ThenTheyCostInTotal(int expectedPrice)
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

        [Then(@"the user should be {User}")]
        public void ThenTheUserShouldBeUser(User user)
        {
            Assert.Equal("Marvin", user.Name);
        }

        [Then("the coordinates should be {Coordinate}")]
        public void ThenTheCoordinatesShouldBe(Coordinate coordinate)
        {
            Assert.NotNull(coordinate);
        }

        [StepArgumentTransformation(@"(.*), (.*)")]
        public Coordinate ConvertCoordinate(int xCoord, int yCoord)
        {
            return new Coordinate 
            {
                X = xCoord,
                Y = yCoord
            };
        }

        [StepArgumentTransformation]
        public Person PersonConverter(string name)
        {
            return new Person(name);
        }

        [StepArgumentTransformation(@"user (.*)")]
        public User UserConverter(string name)
        {
            return new User(name);
        }
    }
}
