using BonmasVoxel.Instruments;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonmasVoxel.Logic
{
    internal class Sequencer : ISequencer
    {
        private const float PPQ = 96f;

        public float Tempo { get { return _tempo; } set { ChangeTempo(value); } }

        private float _tempo;
        private float _ppqTickLength;
        private float _tempoInSecoundTick;

        private int _currentTick;
        private int _previousTick = int.MaxValue;

        public event ISequencer.NoteEvent NoteOnTurn;
        public event ISequencer.NoteEvent NoteOffTurn;

        public Sequencer(float tempo)
        {
            ChangeTempo(tempo);
        }

        private void ChangeTempo(float newTempo)
        {
            _tempoInSecoundTick = newTempo / 60f;
            _ppqTickLength = 1f / _tempoInSecoundTick / PPQ;
            _tempo = newTempo;
        }

        public void Update(double time)
        {
            int currentTick = (int)Math.Max(Math.Ceiling(time % _ppqTickLength / _ppqTickLength * 2) - 1, 0f);

            if (_previousTick != currentTick)
            {
                if (currentTick == 0)
                {
                    Tick(time);
                    _currentTick++;
                }

                _previousTick = currentTick;
            }
        }

        private void Tick(double time)
        {
            if (_currentTick % PPQ == 0)
                NoteOnTurn.Invoke(time, Note.B, 1);
            else if (_currentTick % PPQ == PPQ / 2)
                NoteOffTurn.Invoke(time, Note.B, 1);
        }
    }

    interface ISequencer
    {
        public delegate void NoteEvent(double time, Note note, int octave);

        float Tempo { get; set; }

        event NoteEvent NoteOnTurn;
        event NoteEvent NoteOffTurn;

        void Update(double time);
    }
}
