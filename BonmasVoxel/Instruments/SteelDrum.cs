using BonmasVoxel.Logic;
using BonmasVoxel.Oscs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Instruments
{
    internal class SteelDrum : IInstrument
    {
        public Voices Type => Voices.Mono;

        public int VoiceCount => 1;

        private const int modFreq = 500;

        ADSR _volumeControl;
        ADSR _noiseControl;
        BasicOsc _percs;
        RMOsc _base;

        public SteelDrum()
        {
            _volumeControl = new ADSR()
            {
                AttackTime = 0.01,
                DecayTime = 0.1,
                SustainVolume = Utils.DBToVolume(-24),
                ReleaseTime = 0.2
            };

            _noiseControl = new ADSR()
            {
                AttackTime = 0,
                DecayTime = 0.10,
                SustainVolume = Utils.DBToVolume(-24),
                ReleaseTime = 0.2
            };

            var carrier = new BasicOsc(OscTypes.Sine);
            var modulator = new FMOsc(new BasicOsc(OscTypes.Sine), new BasicOsc(OscTypes.Sine));

            modulator.ModulatorFrequency = modFreq * 2;
            modulator.Factor = 28;

            _base = new RMOsc(modulator, carrier);

            _base.ModulatorFrequency = modFreq;
            _base.Frequency = 6000;
            _base.Factor = 0.1;

            _percs = new BasicOsc(OscTypes.Noise);
        }

        public double GetSample(double time)
        {
            double sample = _base.GetSample(time);

            sample *= _volumeControl.GetControlSignal(time);

            sample += _percs.GetSample(time) * Utils.DBToVolume(-30) * _noiseControl.GetControlSignal(time);

            sample *= Utils.DBToVolume(-6);

            return sample;
        }

        public void NoteOff(double time, Note note, int octave)
        {
            _volumeControl.TurnOff(time);
            _noiseControl.TurnOff(time);
        }

        public void NoteOn(double time, Note note, int octave)
        {
            _volumeControl.TurnOn(time);
            _noiseControl.TurnOn(time);
        }
    }
}
