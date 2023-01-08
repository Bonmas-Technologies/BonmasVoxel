using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Oscs
{
    internal class FMOsc : IModulator
    {
        public double Frequency { get => _carrier.Frequency; set => _carrier.Frequency = value; }
        
        public double ModulatorFrequency { get => _modulator.Frequency; set => _modulator.Frequency = value; }
        
        public double Factor { get; set; }

        public double Phase { get => _modulator.Phase; set => _modulator.Phase = value; }

        private IOscillator _modulator;
        private IOscillator _carrier;

        public FMOsc(IOscillator modulator, IOscillator carrier)
        {
            Factor = 1;

            _modulator = modulator;
            _carrier = carrier;
        }

        public double GetSample(double time)
        {
            _carrier.Phase = _modulator.GetSample(time) * Factor;

            return _carrier.GetSample(time);
        }
    }
}
