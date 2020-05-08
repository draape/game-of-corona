using System.Collections.Generic;
using FakeItEasy;
using Xunit;

namespace GameOfCorona.UnitTests
{
    public class WorldTests
    {
        [Fact]
        public void A_world_should_be_populated_with_persons()
        {
            var world = new World();
            var persons = new List<IPerson>
            {
                A.Fake<IPerson>(), A.Fake<IPerson>(), A.Fake<IPerson>(), A.Fake<IPerson>()
            };

            world.Populate(persons);

            Assert.Equal(persons.Count, world.Population.Length);
        }

        [Fact]
        public void All_persons_should_meet_neighbours_during_a_day()
        {
            var personA = GetFakePerson();
            var personB = GetFakePerson();
            var world = new World();
            
            world.Populate(new List<IPerson>{personA, personB, GetFakePerson(), GetFakePerson()});
            
            world.Sunrise();
            
            A.CallTo(() => personA.Meet(A<IPerson>._))
                .MustHaveHappenedANumberOfTimesMatching(x => x == 3);
        }

        [Fact]
        public void A_person_that_gets_infected_should_not_infect_anyone_else_until_the_next_day()
        {
            var allInfectedSettings = new ProbabilitySettings(1, 0, 0, 0, 0, 0);
            var personA = new Person(allInfectedSettings) {IsInfected = true};
            var personB = new Person(allInfectedSettings);
            var personC = new Person(allInfectedSettings);
            var world = new World();
            
            world.Populate(new List<IPerson>
            {
                personA, personB, personC, 
                GetFakePerson(), GetFakePerson(), GetFakePerson(),
                GetFakePerson(), GetFakePerson(), GetFakePerson()
            });
            
            world.Sunrise();
            
            Assert.False(personC.IsInfected);
        }
        
        [Fact]
        public void All_persons_should_sleep_at_night()
        {
            var personA = A.Fake<IPerson>();
            var personB = A.Fake<IPerson>();
            var world = new World();
            
            world.Populate(new List<IPerson>{personA, personB, A.Fake<IPerson>(), A.Fake<IPerson>()});
            
            world.Sunset();
            
            A.CallTo(() => personA.Sleep()).MustHaveHappened();
            A.CallTo(() => personB.Sleep()).MustHaveHappened();
        }
        
        static IPerson GetFakePerson()
        {
            var fakePerson = A.Fake<IPerson>();
            A.CallTo(() => fakePerson.Clone()).Returns(A.Fake<Person>());
            return fakePerson;
        }
    }
}