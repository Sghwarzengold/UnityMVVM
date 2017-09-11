using MVVMExample.Models;

using System;
using System.Collections.Generic;

using UnityMvvm;

namespace MVVMExample.ViewModels
{
    public enum GameState
    {
        Game,
        BlackWins,
        WhiteWins
    }

    public class GameViewModel : ViewModel
    {
        private const int DRAUGHTS_PER_PLAYER = 12;

        private GameModel m_gameModel;

        public GameViewModel(GameModel gameModel)
        {
            m_gameModel = gameModel;
            Blacks = new List<DraughtViewModel>();
            Whites = new List<DraughtViewModel>();
            
            Rebuild();
        }

        private void Rebuild()
        {
            foreach (var dr in m_gameModel.Blacks)
                Blacks.Add(new DraughtViewModel(dr));

            foreach (var dr in m_gameModel.Whites)
                Whites.Add(new DraughtViewModel(dr));
        }

        public List<DraughtViewModel> Blacks { get; internal set; }
        public List<DraughtViewModel> Whites { get; internal set; }

        private GameState m_state;

        public GameState State
        {
            get { return m_state; }
            set
            {
                m_state = value;
                NotifySubscribers();
            }
        }

        public void InitiStartState()
        {
            Blacks.Clear();
            Whites.Clear();
            m_gameModel.Blacks.Clear();
            m_gameModel.Whites.Clear();

            for (int i = 0; i < DRAUGHTS_PER_PLAYER; i++)
            {
                var blackRow = (int)(i / 4);
                var blackColumn = (int)((i * 2) % 8 + (1 - blackRow % 2));

                m_gameModel.Blacks.Add(new Man { Y = blackRow, X = blackColumn });

                var whiteRow = blackRow + 5;
                var whiteColumn = (int)((i * 2) % 8 + blackRow % 2);

                m_gameModel.Whites.Add(new Man { Y = whiteRow, X = whiteColumn });
            }

            State = GameState.Game;

            Rebuild();
        }

        public void Turn(DraughtViewModel draught, int x, int y)
        {
            if (x < 0 || y < 0 || x > 7 || y > 7)
            {
                BadTurn();
                return;
            }

            if (Math.Abs(x - draught.X) > 1 &&
               Math.Abs(y - draught.Y) > 1)
            {
                var enemies = Whites.Contains(draught) ? Blacks : Whites;

                var xDir = Math.Sign(x - draught.X);
                var yDir = Math.Sign(y - draught.Y);

                var victim = enemies.Find(d => d.X == draught.X + xDir && d.Y == draught.Y + yDir);
                if (victim == null)
                {
                    BadTurn();
                    return;
                }

                enemies.Remove(victim);
                NotifySubscribers();

                draught.X = x;
                draught.Y = y;
            }

            if (Blacks.Contains(draught))
                BlackTurn(draught, x, y);
            else if (Whites.Contains(draught))
                WhiteTurn(draught, x, y);
            else
                BadTurn();

            if (Whites.Count == 0)
                State = GameState.BlackWins;
            else if (Blacks.Count == 0)
                State = GameState.WhiteWins;
        }

        private void WhiteTurn(DraughtViewModel draught, int x, int y)
        {
            if ((x != draught.X - 1 && x != draught.X + 1) ||
                y != draught.Y - 1)
            {
                BadTurn();
                return;
            }

            draught.X = x;
            draught.Y = y;
        }

        private void BlackTurn(DraughtViewModel draught, int x, int y)
        {

            if ((x != draught.X - 1 && x != draught.X + 1) ||
                y != draught.Y + 1)
            {
                BadTurn();
                return;
            }

            draught.X = x;
            draught.Y = y;
        }

        private void BadTurn()
        {

        }
    }
}
