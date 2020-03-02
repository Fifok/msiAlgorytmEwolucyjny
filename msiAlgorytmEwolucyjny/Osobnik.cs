namespace msiAlgorytmEwolucyjny
{
    public class Osobnik
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Dopasowanie { get; set; }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, F: {Dopasowanie}";
        }
    }
}