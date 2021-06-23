using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Logic
{
    public class Field
    {
        public Figure[,] Figures { get; set; } 
        
        public Field()
        {
            Figures = new Figure[8, 8];
            CreateFigures("RKBQLBKR/PPPPPPPP/......../......../......../......../pppppppp/rkblqbkr");          
        }

        public Field(string savedPosition)
        {
            Figures = new Figure[8, 8];
            CreateFigures(savedPosition);
        }

        public void Move(Figure figure, int x, int y)
        {
            if (Figures[y, x] == null)
            {
                Figures[y, x] = figure;
                Figures[figure.CurrentCoordinate.Y, figure.CurrentCoordinate.X] = null;
            }
        }

        private void CreateFigures(string position)
        {
            for (var i = 0; i < 8; i++)
            {
                var row = position.Split('/');
                for (var j = 0; j < 8; j++)
                {
                    var str = row[i].ToCharArray();
                    State(str[j], i, j);
                }
            }
        }

        public void Kill(Figure figure)
        {
            Figures[figure.CurrentCoordinate.Y, figure.CurrentCoordinate.X] = null;
        }

        public void State(char symbol, int x, int y)
        {
            switch(symbol)
            {
                case 'b':
                    Figures[x, y] = new Bishop(Side.Black);
                    break;
                case 'r':
                    Figures[x, y] = new Rook(Side.Black);
                    break;
                case 'k':
                    Figures[x, y] = new Knight(Side.Black);
                    break;
                case 'l':
                    Figures[x, y] = new King(Side.Black);
                    break;
                case 'q':
                    Figures[x, y] = new Queen(Side.Black);
                    break;
                case 'p':
                    Figures[x, y] = new Pawn(Side.Black);
                    break;
                case 'B':
                    Figures[x, y] = new Bishop(Side.White);
                    break;
                case 'R':
                    Figures[x, y] = new Rook(Side.White);
                    break;
                case 'K':
                    Figures[x, y] = new Knight(Side.White);
                    break;
                case 'L':
                    Figures[x, y] = new King(Side.White);
                    break;
                case 'Q':
                    Figures[x, y] = new Queen(Side.White);
                    break;
                case 'P':
                    Figures[x, y] = new Pawn(Side.White);
                    break;

            }
            if (Figures[x, y] != null)
                Figures[x, y].CurrentCoordinate = new Coordinate(y, x);
        }

        public string SavedFiguresDisposition()
        {
            StringBuilder result = new StringBuilder();

            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    if (Figures[row, col] != null)
                        result.Append(Figures[row, col].Name);
                    else
                        result.Append('.');
                }
                result.Append('/');
            }

            return result.ToString();
        }
    }
}
