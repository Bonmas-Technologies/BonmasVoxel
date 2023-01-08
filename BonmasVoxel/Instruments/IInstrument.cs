using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Instruments
{
    public interface IInstrument : IAudioTract, INoteTrigger
    {
        Voices Type { get; }

        int VoiceCount { get; }
    }

    public interface INoteTrigger
    {
        void NoteOn(double time, Note note, int octave);

        void NoteOff(double time, Note note, int octave);
    }

    public enum Voices
    {
        Mono,
        Poly
    }

    public enum Note
    {
        C,
        Cs,
        D,
        Ds,
        E,
        F,
        Fs,
        G,
        Gs,
        A,
        As,
        B
    }
}
