using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Oscs
{
    internal interface IModulator : IOscillator
    {
        double ModulatorFrequency { get; set; }

        double Factor { get; set; }
    }
}
