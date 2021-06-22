using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess.Logic
{
    public class Game
    {
        public Field ChessField { get; private set; }

        public Player currentPlayer { get; private set; }

        private Player[] players;

        public Game()
        {
            ChessField = new Field();
            players = new Player[] { new Player(Side.White), new Player(Side.Black) };
            currentPlayer = players[(int)Side.White];
        }

        public Game(string savedProcess)
        {
            var savedFiguresDisposition = savedProcess.Split('_')[0];
            var savedSide = savedProcess.Split('_')[1].ToCharArray()[0];
            ChessField = new Field(savedFiguresDisposition);
            players = new Player[] { new Player(Side.White), new Player(Side.Black) };
            currentPlayer = players.Where(x => x.PlayerSide == (Side)savedSide).FirstOrDefault();

        }

        public bool IsWin()
        {
            return currentPlayer.CheckLoss();
        }

        public string SaveGame()
        {
            string savedPositions = ChessField.SavedFiguresDisposition();
            string savedPlayerSide = "_" + (char)currentPlayer.PlayerSide;

            return savedPositions + savedPlayerSide;
        }

        public void ChangePlayer()
        {
            currentPlayer = currentPlayer.PlayerSide == Side.White ? players[(int)Side.Black] : players[(int)Side.White];
        }

    }
}
