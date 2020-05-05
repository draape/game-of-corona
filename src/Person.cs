using System;

namespace GameOfCorona
{
    public class Person
    {
        readonly ProbabilitySettings _probabilities;
        bool _hasMetInfectedPerson;

        public bool IsInfected { get; set; }
        public bool IsInIsolation { get; set; }
        public bool IsInQuarantine { get; set; }
        public bool IsImmune { get; set; }
        public bool IsDead { get; set; }
        
        public Person(ProbabilitySettings probabilities) => _probabilities = probabilities;

        public void Meet(Person person)
        {
            if (person.IsInQuarantine || IsImmune || IsDead) return;
            
            if(CheckRandom(_probabilities.Infection))
                IsInfected = person.IsInfected;
                
            _hasMetInfectedPerson = person.IsInfected;
        }

        public void Sleep()
        {
            if (IsDead)
                return;
            
            if(CheckRandom(_probabilities.Isolation) && !IsInfected)
                IsInIsolation = _hasMetInfectedPerson;

            if(CheckRandom(_probabilities.Quarantine))
                IsInQuarantine = IsInfected;

            if (!CheckRandom(_probabilities.StayInfected))
                IsInfected = false;

            if (CheckRandom(_probabilities.HealthyNotImmune) && IsInfected)
            {
                IsImmune = false;
                IsInfected = false;
            }
            
            if (CheckRandom(_probabilities.HealthyAndImmune) && IsInfected)
            {
                IsImmune = true;
                IsInfected = false;
            }

            if (CheckRandom(_probabilities.Dies))
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