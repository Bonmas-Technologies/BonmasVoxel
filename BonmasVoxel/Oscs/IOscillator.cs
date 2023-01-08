namespace BonmasVoxel.Oscs
{
    public interface IOscillator : IAudioTract
    {
        public double Frequency { get; set; }
        public double Phase { get; set; }
    }
}