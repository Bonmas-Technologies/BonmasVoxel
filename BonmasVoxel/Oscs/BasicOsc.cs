using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Oscs
{
    internal class BasicOsc : IOscillator
    {
        public double Frequency { get; set; }

        public OscTypes Type { get; set; }

        public double Phase { get; set; }

        private Random noiseGen;

        public BasicOsc(OscTypes type)
        {
            Type = type;
            noiseGen = new Random();
        }

        public double GetSample(double time)
        {
            double sample;

            switch (Type)
            {
                case OscTypes.Sine:
                    sample = Sin(time);
                    break;
                case OscTypes.Square:
                    sample = Sqr(time);
                    break;
                case OscTypes.Triangle:
                    sample = Tri(time);
                    break;
                case OscTypes.Saw:
                    sample = Saw(time);
                    break;
                case OscTypes.Noise:
                    sample = Noise(time);
                    break;
                default:
                    sample = 0;
                    break;
            }
            return sample;
        }

        private double Sin(double time)
        {
            return Math.Sin(time * Frequency * Math.PI * 2.0 + Phase);
        }

        private double Sqr(double time)
        {
            double saw = (time * 2.0 * Frequency + Phase) % 2.0 - 1.0;

            return saw >= 0.0 ? 1 : -1;
        }

        private double Tri(double time)
        {
            double result = (2.0 * time * 2.0 * Frequency + Phase) % 2.0;

            if (result > 1.0)
                result = 2.0 - result;

            if (result < -1.0)
                result = -2.0 - result;

            return result;
        }

        private double Saw(double time)
        {
            return (time * 2.0 * Frequency + Phase) % 2.0 - 1.0;
        }

        private double Noise(double time)
        {
            return noiseGen.NextDouble() * 2 - 1;
        }
    }

    enum OscTypes
    {
        Sine,
        Square,
        Triangle,
        Saw,
        Noise
    }
}
