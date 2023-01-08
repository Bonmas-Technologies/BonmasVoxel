using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Logic
{
    internal interface ILogicTract
    {
        double GetControlSignal(double time);
    }
}
