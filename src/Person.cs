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

            if(CheckRandom(_probabilities.Quarantine)) // TODO can get out of quarantine without being healthy first
                IsInQuarantine = IsInfected;

            if (!CheckRandom(_probabilities.StayInfected))
                IsInfected = false;

            if (CheckRandom(_probabilities.Healthy) && IsInfected) // TODO if in quarantine, will not get out 
            {
                IsInfected = false;
                if (CheckRandom(_probabilities.Immune))
                    IsImmune = true;
            }

            if (CheckRandom(_probabilities.Dies)) // TODO dies when healthy
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