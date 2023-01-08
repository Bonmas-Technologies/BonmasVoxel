using System;
using System.Collections.Generic;
using System.Text;

namespace ByteBeat
{
    internal class Bytebeat : IDigitAudioTract
    {
        public int GetSample(int time)
        {
            //return (time*(time>>5|time>>8))>>(time>>16);
            return (time >> 7 | time | time >> 6) * 10 + 4 * (time & time >> 13 | time >> 6);
        }
    }
}
