using BonmasVoxel.Instruments;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel
{
    internal static class Utils
    {
        private static double[] Notes =
        {
            16.35, 
            17.32,
            18.35,
            19.45,
            20.60,
            21.83,
            23.12,
            24.50,
            25.96,
            27.50,
            29.14,
            30.87
        };


        public static double VolumeToDB(double volume)
        {
            return 20 * Math.Log10(volume);
        }
        public static double DBToVolume(double db)
        {
            return Math.Pow(10, db / 20);
        }

        public static double NoteToFrequency(Note note, int octave)
        {
            return Notes[(int)note] * Math.Pow(2, octave);
        }

        public static double Lerp(double from, double to, double time)
        {
            return from * (1 - time) + to * time;
        }

        public static double ClampedLerp(double from, double to, double time)
        {
            time = Math.Max(Math.Min(time, 1), 0);

            return Lerp(from, to, time);
        }
    }
}
