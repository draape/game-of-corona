using Xunit;

namespace GameOfCorona.UnitTests
{
    public class GameTests
    {
        readonly ProbabilitySettingsBuilder _probabilitySettingsBuilder;
        
        public GameTests() => _probabilitySettingsBuilder = new ProbabilitySettingsBuilder();

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void When_a_healthy_person_is_in_contact_with_an_infected_person_Should_get_infected(double probability, bool expected)
        {
            var settings = _probabilitySettingsBuilder.WithInfection(probability).Build();

            var person = new Person(settings);
            var infectedPerson = new Person(settings) {IsInfected = true};
            
            person.Meet(infectedPerson);
            
            Assert.Equal(expected, person.IsInfected);
        }
        
        [Fact]
        public void A_quarantined_person_cannot_infect_others()
        {
            var settings = _probabilitySettingsBuilder.WithInfection(1).Build();
            
            var person = new Person(settings);
            var infectedPerson = new Person(settings) {IsInfected = true, IsInQuarantine = true};
            
            person.Meet(infectedPerson);
            
            Assert.False(person.IsInfected);
        }
        
        [Fact]
        public void An_immune_person_cannot_be_infected()
        {
            var settings = _probabilitySettingsBuilder.WithInfection(1).Build();
            
            var person = new Person(settings) {IsImmune = true};
            var infectedPerson = new Person(settings) {IsInfected = true};
            
            person.Meet(infectedPerson);
            
            Assert.False(person.IsInfected);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void When_a_healthy_person_is_in_contact_with_an_infected_person_Should_go_into_isolation(double probability, bool expected)
        {
            var settings = _probabilitySettingsBuilder.WithIsolation(probability).Build();
            
            var person = new Person(settings);
            var infectedPerson = new Person(settings) {IsInfected = true};
            
            person.Meet(infectedPerson);
            person.Sleep();
            
            Assert.Equal(expected, person.IsInIsolation);
        }
        
        [Fact]
        public void When_a_healthy_person_gets_infected_Should_not_go_into_isolation()
        {
            var settings = _probabilitySettingsBuilder.WithInfection(1).WithIsolation(1).Build();
            
            var person = new Person(settings);
            var infectedPerson = new Person(settings) {IsInfected = true};
            
            person.Meet(infectedPerson);
            person.Sleep();
            
            Assert.False(person.IsInIsolation);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_Should_go_into_quarantine(double probability, bool expected)
        {
            var settings = _probabilitySettingsBuilder.WithQuarantine(probability).Build();
            
            var infectedPerson = new Person(settings) {IsInfected = true};
            infectedPerson.Sleep();
            
            Assert.Equal(expected, infectedPerson.IsInQuarantine);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_stays_infected(double probability, bool expected)
        {
            var settings = _probabilitySettingsBuilder.WithStayInfected(probability).Build();
            
            var infectedPerson = new Person(settings) {IsInfected = true};
            infectedPerson.Sleep();
            
            Assert.Equal(expected, infectedPerson.IsInfected);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(0, true)]
        public void An_infected_person_is_healthy_again_but_not_immune(double probability, bool expected)
        {
            // Needs to pass 1.0 in stay infected. Could be optimized.
            var settings = _probabilitySettingsBuilder.WithStayInfected(1).WithHealthyNotImmune(probability).Build();
            
            var infectedPerson = new Person(settings) {IsInfected = true};
            infectedPerson.Sleep();
            
            Assert.Equal(expected, infectedPerson.IsInfected);
            Assert.False(infectedPerson.IsImmune);
        }

        [Theory]
        [InlineData(1, false, true)]
        [InlineData(0, true, false)]
        public void An_infected_person_is_healthy_again_and_immune(double probability, bool expectedInfected, bool expectedImmune)
        {
            // Needs to pass 1.0 in stay infected. Could be optimized.
            var settings = _probabilitySettingsBuilder.WithStayInfected(1).WithHealthyAndImmune(probability).Build();
            
            var infectedPerson = new Person(settings) {IsInfected = true};
            infectedPerson.Sleep();
            
            Assert.Equal(expectedInfected, infectedPerson.IsInfected);
            Assert.Equal(expectedImmune, infectedPerson.IsImmune);
        }
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void An_infected_person_dies(double probability, bool expected)
        {
            var settings = _probabilitySettingsBuilder.WithDies(probability).Build();
            
            var infectedPerson = new Person(settings) {IsInfected = true};
            infectedPerson.Sleep();
            
            Assert.Equal(expected, infectedPerson.IsDead);
        }

        [Fact]
        public void A_healthy_person_cannot_become_immune()
        {
            var settings = _probabilitySettingsBuilder.WithHealthyAndImmune(1).Build();
            
            var person = new Person(settings);
            
            person.Sleep();
            
            Assert.False(person.IsImmune);
        }
        
        [Fact]
        public void A_dead_person_stays_dead()
        {
            var settings = _probabilitySettingsBuilder
                .WithInfection(1)
                .WithIsolation(1)
                .WithStayInfected(1)
                .WithHealthyNotImmune(1)
                .WithHealthyAndImmune(1)
                .WithQuarantine(1)
                .WithDies(1)
                .Build();
            
            var person = new Person(settings) {IsDead = true};
            var infectedPerson = new Person(settings) {IsInfected = true};
            
            person.Meet(infectedPerson);
            person.Sleep();
            
            Assert.True(person.IsDead);
            Assert.False(person.IsInfected);
            Assert.False(person.IsImmune);
            Assert.False(person.IsInQuarantine);
            Assert.False(person.IsInIsolation);
        }
    }
}