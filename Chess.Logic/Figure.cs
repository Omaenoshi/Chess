using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Chess.Logic
{
    public abstract class Figure 
    {
        public char Name { get; protected set; }
        
        public Side Side { get; protected set; }

        public Coordinate CurrentCoordinate { get; set; }

        public abstract bool CanMove(int x, int y);      
    }


    public enum Side
    {
        White = 0,
        Black = 1
    }

    public class Bishop : Figure
    {
        public Bishop(Side side)
        {
            Side = side;
            Name = Side == Side.White ? 'B' : 'b';
        }

        public override bool CanMove(int x, int y)
        {
            int deltaX = Math.Abs(CurrentCoordinate.X - x);
            int deltaY = Math.Abs(CurrentCoordinate.Y - y);
            return deltaX == deltaY;
        }
    }
    
    public class Rook : Figure
    {
        public Rook(Side side)
        {
            Side = side;
            Name = Side == Side.White ? 'R' : 'r';
        }

        public override bool CanMove(int x, int y)
        {
            return CurrentCoordinate.X == x || CurrentCoordinate.Y == y;
        }
    }

    public class Knight : Figure
    {
        public Knight(Side side)
        {
            Side = side;
            Name = Side == Side.White ? 'K' : 'k';
        }

        public override bool CanMove(int x, int y)
        {
            int deltaX = Math.Abs(CurrentCoordinate.X - x);
            int deltaY = Math.Abs(CurrentCoordinate.Y - y);
            return (deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2);
        }
    }

    public class Queen : Figure
    {
        public Queen(Side side)
        {
            Side = side;
            Name = Side == Side.White ? 'Q' : 'q';
        }

        public override bool CanMove(int x, int y)
        {
            int deltaX = Math.Abs(CurrentCoordinate.X - x);
            int deltaY = Math.Abs(CurrentCoordinate.Y - y);
            return CurrentCoordinate.X == x || CurrentCoordinate.Y == y || deltaX == deltaY;
        }
    }

    public class King : Figure
    {
        public King(Side side)
        {
            Side = side;
            Name = Side == Side.White ? 'L' : 'l';
        }

        public override bool CanMove(int x, int y)
        {
            int deltaX = Math.Abs(CurrentCoordinate.X - x);
            int deltaY = Math.Abs(CurrentCoordinate.Y - y);
            return deltaX == 1 && deltaY < 2;
        }
    }

    public class Pawn : Figure
    {
        public Pawn(Side side)
        {
            Side = side;
            Name = Side == Side.White ? 'P' : 'p';
        }

        public override bool CanMove(int x, int y)
        {
            int deltaY = Math.Abs(CurrentCoordinate.Y - y);
            return CurrentCoordinate.X == x && deltaY < 2;
        }
    }
}
