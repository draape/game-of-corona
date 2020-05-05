namespace GameOfCorona
{
    public class ProbabilitySettings
    {
        public ProbabilitySettings()
        {
            Infection = 0.5;
            Isolation = 0.1;
            Quarantine = 0.8;
            Healthy = 0.45;
            Immune = 0.05;
            Dies = 0.05;
        }
        
        public ProbabilitySettings(double infection, double isolation, double quarantine, double healthy, double immune,
            double dies)
        {
            Infection = infection;
            Isolation = isolation;
            Quarantine = quarantine;
            Immune = immune;
            Healthy = healthy;
            Dies = dies;
        }
        
        public double Infection { get; }
        public double Isolation { get; }
        public double Quarantine { get; }
        public double Healthy { get; }
        public double Immune { get; }
        public double Dies { get; }
    }
}