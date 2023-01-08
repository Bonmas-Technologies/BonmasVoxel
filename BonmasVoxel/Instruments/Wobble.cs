using BonmasVoxel.Logic;
using BonmasVoxel.Oscs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Instruments
{
    internal class Wobble : IInstrument
    {
        public Voices Type => Voices.Mono;

        public int VoiceCount => 1;

        private const double WobbleFactor = 0.4;
        private const int WobbleFrequency = 5;

        private ADSR adsr;
        private BasicOsc Lfo;

        private FMOsc MainHarm;
        private FMOsc FifthHarm;
        private FMOsc SeventhHarm;

        public Wobble()
        {
            adsr = new ADSR()
            {
                AttackTime = 0.01,
                DecayTime = 0.2,
                SustainVolume = Utils.DBToVolume(-12),
                ReleaseTime = 0.1
            };

            Lfo = new BasicOsc(OscTypes.Sine)
            {
                Frequency = WobbleFrequency
            };

            MainHarm = new FMOsc(Lfo, new BasicOsc(OscTypes.Sine));
            FifthHarm = new FMOsc(Lfo, new BasicOsc(OscTypes.Sine));
            SeventhHarm = new FMOsc(Lfo, new BasicOsc(OscTypes.Sine));

            MainHarm.Factor = WobbleFactor;
            FifthHarm.Factor = WobbleFactor;
            SeventhHarm.Factor = WobbleFactor;

            SetFrequency(55);
        }

        public double GetSample(double time)
        {
            double sample = 0;

            sample += MainHarm.GetSample(time) * Utils.DBToVolume(-6);
            sample += FifthHarm.GetSample(time) * Utils.DBToVolume(-9);
            sample += SeventhHarm.GetSample(time) * Utils.DBToVolume(-12);

            sample *= adsr.GetControlSignal(time);

            return sample;
        }

        private void SetFrequency(double freq)
        {
            MainHarm.Frequency = freq;
            FifthHarm.Frequency = freq * 5;
            SeventhHarm.Frequency = freq * 7;
        }

        public void NoteOn(double time, Note note, int octave)
        {
            SetFrequency(Utils.NoteToFrequency(note, octave));
            adsr.TurnOn(time);
        }

        public void NoteOff(double time, Note note, int octave)
        {
            SetFrequency(Utils.NoteToFrequency(note, octave));
            adsr.TurnOff(time);
        }
    }
}
