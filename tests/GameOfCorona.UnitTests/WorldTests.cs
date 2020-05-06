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
            var personA = A.Fake<IPerson>();
            var personB = A.Fake<IPerson>();
            var world = new World();
            
            world.Populate(new List<IPerson>{personA, personB, A.Fake<IPerson>(), A.Fake<IPerson>()});
            
            world.Sunrise();
            
            A.CallTo(() => personA.Meet(A<IPerson>._))
                .MustHaveHappenedANumberOfTimesMatching(x => x == 3);
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
    }
}