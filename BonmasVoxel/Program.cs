using System;
using NAudio;
using NAudio.Wave;
using System.Threading.Tasks;
using System.IO;
using NAudio.Midi;
using BonmasVoxel.Output;
using BonmasVoxel.Instruments;
using BonmasVoxel.Logic;

namespace BonmasVoxel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Wobble wobble = new Wobble();

            var sequencer = new Sequencer(80);
            sequencer.NoteOnTurn += wobble.NoteOn;
            sequencer.NoteOffTurn += wobble.NoteOff;

            AudioOut output = new AudioOut(44100, 2, wobble, sequencer);

            Console.WriteLine(MidiIn.NumberOfDevices);

            using (var waveOut = new WaveOutEvent())
            {
                waveOut.Init(output);
                waveOut.Play();

                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Task.Delay(200).Wait();
                }
            }

        }
    }

    

}
