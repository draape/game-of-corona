using System;

namespace GameOfCorona
{
    public class Person
    {
        public bool IsInfected { get; set; }

        public void Meet(Person person, double probability)
        {
            if(CheckRandom(probability))
                IsInfected = person.IsInfected;
        }

        bool CheckRandom(in double probability)
        {
            var random = new Random();
            var randomFactor = random.Next(0, 100) / 100.0;
            return randomFactor <= probability;
        }
    }
}