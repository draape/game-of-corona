namespace GameOfCorona
{
    public class ProbabilitySettings
    {
        public ProbabilitySettings()
        {
            Infection = 0.5;
            Isolation = 0.1;
            Quarantine = 0.8;
            StayInfected = 0.5;
            HealthyNotImmune = 0.05;
            HealthyAndImmune = 0.4;
            Dies = 0.05;
        }
        
        public ProbabilitySettings(double infection, double isolation, double quarantine, double stayInfected, double healthyNotImmune, double healthyAndImmune, double dies)
        {
            Infection = infection;
            Isolation = isolation;
            Quarantine = quarantine;
            StayInfected = stayInfected;
            HealthyNotImmune = healthyNotImmune;
            HealthyAndImmune = healthyAndImmune;
            Dies = dies;
        }
        
        public double Infection { get; }
        public double Isolation { get; }
        public double Quarantine { get; }
        public double StayInfected { get; }
        public double HealthyNotImmune { get; }
        public double HealthyAndImmune { get; }
        public double Dies { get; }
    }
}