using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Oscs
{
    internal class RMOsc : IModulator
    {
        public double Frequency { get => _carrier.Frequency; set => _carrier.Frequency = value; }
        
        public double ModulatorFrequency { get => _modulator.Frequency; set => _modulator.Frequency = value; }
        
        public double Factor { get; set; }

        public double Phase { get; set; }

        private IOscillator _modulator;
        private IOscillator _carrier;

        public RMOsc(IOscillator modulator, IOscillator carrier)
        {
            Factor = 0;

            _modulator = modulator;
            _carrier = carrier;
        }

        public double GetSample(double time)
        {
            double signal = _carrier.GetSample(time);

            return signal * Utils.Lerp(_modulator.GetSample(time), signal, Factor);
        }
    }
}
