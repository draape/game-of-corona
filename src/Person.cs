using System;

namespace GameOfCorona
{
    public class Person
    {
        bool _hasMetInfectedPerson;
        public bool IsInfected { get; set; }
        public bool IsInIsolation { get; set; }

        public void Meet(Person person, double probability)
        {
            if(CheckRandom(probability))
                IsInfected = person.IsInfected;

            _hasMetInfectedPerson = person.IsInfected;
        }

        public void Sleep(double pIsolation)
        {
            if(CheckRandom(pIsolation))
                IsInIsolation = _hasMetInfectedPerson;
        }

        static bool CheckRandom(in double probability)
        {
            var random = new Random();
            var randomFactor = random.Next(0, 100) / 100.0;
            return randomFactor <= probability;
        }
    }
}