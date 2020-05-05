namespace GameOfCorona.UnitTests
{
    public class ProbabilitySettingsBuilder
    {
        double _infection;
        double _isolation;
        double _quarantine;
        double _stayInfected;
        double _healthyNotImmune;
        double _healthyAndImmune;
        double _dies;

        public ProbabilitySettingsBuilder WithInfection(double probability)
        {
            _infection = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithIsolation(double probability)
        {
            _isolation = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithQuarantine(double probability)
        {
            _quarantine = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithStayInfected(double probability)
        {
            _stayInfected = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithHealthyNotImmune(double probability)
        {
            _healthyNotImmune = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithHealthyAndImmune(double probability)
        {
            _healthyAndImmune = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithDies(double probability)
        {
            _dies = probability;
            return this;
        }
        
        public ProbabilitySettings Build()
        {
            return new ProbabilitySettings(_infection, _isolation, _quarantine, _stayInfected, _healthyNotImmune, _healthyAndImmune, _dies);
        }
    }
}