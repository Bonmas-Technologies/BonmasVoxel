using System;
using System.Text;
using System.Collections.Generic;
using NAudio.Wave;

namespace BonmasVoxel.Oscs
{
    public class WavetableOsc : IOscillator
    {
        public double Frequency { get; set; } = 440;
        public double Phase { get; set; }

        private readonly int _tablesize;
        private List<float[]> _wavetables;

        public WavetableOsc(string path, int tablesize = 2048)
        {
            _tablesize = tablesize;
            _wavetables = new List<float[]>();

            var file = new AudioFileReader(path);

            long countOfTables = file.Length / _tablesize / (file.WaveFormat.BitsPerSample / 8);

            for (int table = 0; table < countOfTables; table++)
            {
                var buffer = new float[_tablesize];
                file.Read(buffer, 0, _tablesize);
                _wavetables.Add(buffer);
            }

            file.Close();
        }

        public double GetSample(double time)
        {
            double genCoord = (time * Frequency + Phase) % 1.0;

            int tableSamplePos = (int)(genCoord * _tablesize);

            if (tableSamplePos < 0)
            {
                int phaseOffsetStrenght = tableSamplePos / _tablesize + 1;

                tableSamplePos = _tablesize * phaseOffsetStrenght + tableSamplePos;
            }

            double sample = _wavetables[0][tableSamplePos]; // TODO: Add wavetable selector

            return sample;
        }
    }
}
