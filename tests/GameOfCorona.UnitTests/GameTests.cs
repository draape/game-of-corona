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
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void When_a_healthy_person_is_in_contact_with_an_infected_person_Should_go_into_isolation(double probability, bool expected)
        {
            var person = new Person();
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, 0);
            person.Sleep(probability, 0, 0);
            
            Assert.Equal(expected, person.IsInIsolation);
        }
        
        [Fact]
        public void When_a_healthy_person_gets_infected_Should_not_go_into_isolation()
        {
            var person = new Person();
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, 100);
            person.Sleep(100, 0, 0);
            
            Assert.False(person.IsInIsolation);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_Should_go_into_quarantine(double probability, bool expected)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, probability, 0);
            
            Assert.Equal(expected, infectedPerson.IsInQuarantine);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_stays_infected(double probability, bool expected)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, 0, probability);
            
            Assert.Equal(expected, infectedPerson.IsInfected);
        }
    }
}