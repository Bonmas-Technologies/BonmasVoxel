using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Logic
{
    internal class ADSR
    {
        public double AttackTime { get; set; }
        
        public double DecayTime { get; set; }
        
        public double SustainVolume { get => sustainVolume; set => sustainVolume = value; }
        
        public double ReleaseTime { get; set; }

        public double AttackVolume { get; set; } = 1;

        public double ReleaseVolume { get; set; } = 0;


        private double sustainVolume;

        private double currentVolume;

        private double VolumeAtTrigger;

        private double GateOnTime;
        private double GateOffTime;

        private bool gated;

        public void TurnOn(double time)
        {
            GateOnTime = time;
            gated = true;
            VolumeAtTrigger = currentVolume;
        }

        public void TurnOff(double time)
        {
            GateOffTime = time;
            gated = false;
            VolumeAtTrigger = currentVolume;
        }

        public double GetControlSignal(double time)
        {
            double currentTime;

            if (gated)
            {
                if ((GateOnTime + AttackTime) > time)
                {
                    currentTime = (time - GateOnTime) / AttackTime;

                    currentVolume = Utils.ClampedLerp(VolumeAtTrigger, AttackVolume, currentTime);
                }
                else
                {
                    currentTime = (time - GateOnTime - AttackTime) / DecayTime;

                    currentVolume = Utils.ClampedLerp(AttackVolume, sustainVolume, currentTime);
                }
            }
            else
            {
                currentTime = (time - GateOffTime) / ReleaseTime;

                currentVolume = Utils.ClampedLerp(VolumeAtTrigger, ReleaseVolume, currentTime);
            }

            return currentVolume;
        }

        public void Reset()
        {
            currentVolume = ReleaseVolume;
        }
    }
}
