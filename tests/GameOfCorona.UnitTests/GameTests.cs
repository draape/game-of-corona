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
        
        [Fact]
        public void A_quarantined_person_cannot_infect_others()
        {
            var person = new Person();
            var infectedPerson = new Person {IsInfected = true, IsInQuarantine = true};
            
            person.Meet(infectedPerson, 1.0);
            
            Assert.False(person.IsInfected);
        }
        
        [Fact]
        public void An_immune_person_cannot_be_infected()
        {
            var person = new Person{IsImmune = true};
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, 1.0);
            
            Assert.False(person.IsInfected);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void When_a_healthy_person_is_in_contact_with_an_infected_person_Should_go_into_isolation(double probability, bool expected)
        {
            var person = new Person();
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, 0);
            person.Sleep(probability, 0, 0, 0, 0, 0);
            
            Assert.Equal(expected, person.IsInIsolation);
        }
        
        [Fact]
        public void When_a_healthy_person_gets_infected_Should_not_go_into_isolation()
        {
            var person = new Person();
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, 100);
            person.Sleep(100, 0, 0, 0, 0, 0);
            
            Assert.False(person.IsInIsolation);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_Should_go_into_quarantine(double probability, bool expected)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, probability, 0, 0, 0, 0);
            
            Assert.Equal(expected, infectedPerson.IsInQuarantine);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_stays_infected(double probability, bool expected)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, 0, probability, 0, 0, 0);
            
            Assert.Equal(expected, infectedPerson.IsInfected);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(0, true)]
        public void An_infected_person_is_healthy_again_but_not_immune(double probability, bool expected)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, 0, 1.0, probability, 0, 0); // Needs to pass 1.0 in stay infected. Could be optimized.
            
            Assert.Equal(expected, infectedPerson.IsInfected);
            Assert.False(infectedPerson.IsImmune);
        }

        [Theory]
        [InlineData(1, false, true)]
        [InlineData(0, true, false)]
        public void An_infected_person_is_healthy_again_and_immune(double probability, bool expectedInfected, bool expectedImmune)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, 0, 1.0, 0, probability, 0); // Needs to pass 1.0 in stay infected. Could be optimized.
            
            Assert.Equal(expectedInfected, infectedPerson.IsInfected);
            Assert.Equal(expectedImmune, infectedPerson.IsImmune);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_dies(double probability, bool expected)
        {
            var infectedPerson = new Person {IsInfected = true};
            infectedPerson.Sleep(0, 0, 0, 0, 0, probability);
            
            Assert.Equal(expected, infectedPerson.IsDead);
        }

        [Fact]
        public void A_healthy_person_cannot_become_immune()
        {
            var person = new Person();
            
            person.Sleep(0, 0, 0, 1.0, 1.0, 0);
            
            Assert.False(person.IsImmune);
        }
        
        [Fact]
        public void A_dead_person_stays_dead()
        {
            var person = new Person{IsDead = true};
            var infectedPerson = new Person {IsInfected = true};
            
            person.Meet(infectedPerson, 1.0);
            person.Sleep(1.0, 1.0, 1.0, 1.0, 1.0, 0);
            
            Assert.True(person.IsDead);
            Assert.False(person.IsInfected);
            Assert.False(person.IsImmune);
            Assert.False(person.IsInQuarantine);
            Assert.False(person.IsInIsolation);
        }
    }
}