using System;

namespace GameOfCorona
{
    public class Person
    {
        bool _hasMetInfectedPerson;
        public bool IsInfected { get; set; }
        public bool IsInIsolation { get; set; }
        public bool IsInQuarantine { get; set; }
        public bool IsImmune { get; set; }
        public bool IsDead { get; set; }

        public void Meet(Person person, double probability)
        {
            if (person.IsInQuarantine || IsImmune) return;
            
            if(CheckRandom(probability))
                IsInfected = person.IsInfected;
                
            _hasMetInfectedPerson = person.IsInfected;
        }

        public void Sleep(double pIsolation, double pQuarantine, double pStayInfected, double pHealthyNotImmune,
            double pHealthyAndImmune, double pDies)
        {
            if(CheckRandom(pIsolation) && !IsInfected)
                IsInIsolation = _hasMetInfectedPerson;

            if(CheckRandom(pQuarantine))
                IsInQuarantine = IsInfected;

            if (!CheckRandom(pStayInfected))
                IsInfected = false;

            if (CheckRandom(pHealthyNotImmune) && IsInfected)
            {
                IsImmune = false;
                IsInfected = false;
            }
            
            if (CheckRandom(pHealthyAndImmune) && IsInfected)
            {
                IsImmune = true;
                IsInfected = false;
            }

            if (CheckRandom(pDies))
                IsDead = true;
        }

        static bool CheckRandom(in double probability)
        {
            var random = new Random();
            var randomFactor = random.Next(0, 100) / 100.0;
            return randomFactor <= probability;
        }
    }
}