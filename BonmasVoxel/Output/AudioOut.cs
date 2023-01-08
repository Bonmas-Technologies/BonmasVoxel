using BonmasVoxel.Logic;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Output
{
    internal class AudioOut : ISampleProvider
    {
        public WaveFormat WaveFormat { get; private set; }

        private Sequencer _sequencer;

        private IAudioTract audio;

        private int nOffset = 0;
        public AudioOut(int sampleRate, int channels, IAudioTract audio, Sequencer sequencer)
        {
            _sequencer = sequencer;

            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
            this.audio = audio;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int num = offset;

            for (int i = 0; i < count / WaveFormat.Channels; i++)
            {
                double time = (double)nOffset / WaveFormat.SampleRate;

                double output = audio.GetSample(time);

                _sequencer.Update(time);

                nOffset++;

                for (int j = 0; j < WaveFormat.Channels; j++)
                    buffer[num++] = (float)output;
            }

            return count;

        }
    }
}
