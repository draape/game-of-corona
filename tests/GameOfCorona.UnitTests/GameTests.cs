using Xunit;

namespace GameOfCorona.UnitTests
{
    public class GameTests
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void When_a_healthy_person_is_in_contact_with_an_infected_person_Should_get_infected(double probability, bool expected)
        {
            var person = new Person();
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, probability);
            
            Assert.Equal(expected, person.IsInfected);
        }
    }
}