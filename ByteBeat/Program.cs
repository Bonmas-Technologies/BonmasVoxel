using NAudio.Wave;
using System;
using System.Threading.Tasks;

namespace ByteBeat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var waveOut = new WaveOutEvent())
            {
                AudioOut output = new AudioOut(8000, 1, new Bytebeat());
                waveOut.Init(output);
                waveOut.Play();

                Console.WriteLine("Lets go!");

                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Task.Delay(200).Wait();
                }
            }
        }

    }
}
