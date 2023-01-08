using NAudio.Wave;

namespace ByteBeat
{
    internal class AudioOut : ISampleProvider
    {
        public WaveFormat WaveFormat { get; private set; }

        private IDigitAudioTract audio;

        private int nOffset = 0;
        public AudioOut(int sampleRate, int channels, IDigitAudioTract audio)
        {
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
            this.audio = audio;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int num = offset;

            for (int i = 0; i < count / WaveFormat.Channels; i++)
            {
                double output = (byte)audio.GetSample(nOffset / (WaveFormat.SampleRate / 8000));

                output /= 255;
                output = output * 2 - 1;
                output *= 0.8;
                nOffset++;

                for (int j = 0; j < WaveFormat.Channels; j++)
                    buffer[num++] = (float)output;
            }

            return count;

        }
    }
}
