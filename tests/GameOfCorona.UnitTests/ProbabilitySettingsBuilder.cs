namespace GameOfCorona.UnitTests
{
    public class ProbabilitySettingsBuilder
    {
        double _infection;
        double _isolation;
        double _quarantine;
        double _healthy;
        double _immune;
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
        
        public ProbabilitySettingsBuilder WithHealthy(double probability)
        {
            _healthy = probability;
            return this;
        }
        
        public ProbabilitySettingsBuilder WithImmune(double probability)
        {
            _immune = probability;
            return this;
        }

        public ProbabilitySettingsBuilder WithDies(double probability)
        {
            _dies = probability;
            return this;
        }
        
        public ProbabilitySettings Build() => 
            new ProbabilitySettings(_infection, _isolation, _quarantine, _healthy, _immune, _dies);
    }
}