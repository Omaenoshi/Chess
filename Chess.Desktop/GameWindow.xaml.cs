using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Chess.Logic;
using Figure = Chess.Logic.Figure;

namespace Chess.Desktop
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private SolidColorBrush currentSquareColor = Brushes.White;
        private const double widthSquare = 70;
        private const double heightSquare = 70;
        private Game currentGame;
        private Field field;
        private Figure currentFigure;


        public GameWindow(Game currentGame)
        {
            InitializeComponent();
            this.currentGame = currentGame; 
            field = currentGame.ChessField;
            Draw();
        }


        private void DrawFiguresOnField()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (field.Figures[i, j] != null)
                        DrawOneFigure(field.Figures[i, j], i, j); 
                }
            }
        }

        private void DrawOneFigure(Figure figure, int i, int j)
        {
            Border myBorder = new Border();           
            myBorder.Height = heightSquare;
            myBorder.Width = widthSquare;
            BitmapImage currentImage = new BitmapImage(DrawImage(figure.Name));
            ImageBrush myCurrentImage = new ImageBrush(currentImage);
            myBorder.Background = myCurrentImage;
            myBorder.MouseDown += ClickedOnFigure;

            Canvas.SetLeft(myBorder, heightSquare * j);
            Canvas.SetRight(myBorder, widthSquare * i);
            Canvas.SetTop(myBorder, widthSquare * i);
            chessField.Children.Add(myBorder);
        }

        private Uri DrawImage(char name)
        {
            return name switch
            {
                'b' => new Uri(@"..\..\..\Pictures\black_bishop.png.", UriKind.Relative),
                'r' => new Uri(@"..\..\..\Pictures\black_rook.png.", UriKind.Relative),
                'k' => new Uri(@"..\..\..\Pictures\black_knight.png.", UriKind.Relative),
                'q' => new Uri(@"..\..\..\Pictures\black_queen.png.", UriKind.Relative),
                'l' => new Uri(@"..\..\..\Pictures\black_king.png.", UriKind.Relative),
                'p' => new Uri(@"..\..\..\Pictures\black_pawn.png.", UriKind.Relative),
                'B' => new Uri(@"..\..\..\Pictures\white_bishop.png.", UriKind.Relative),
                'R' => new Uri(@"..\..\..\Pictures\white_rook.png.", UriKind.Relative),
                'K' => new Uri(@"..\..\..\Pictures\white_knight.png.", UriKind.Relative),
                'Q' => new Uri(@"..\..\..\Pictures\white_queen.png.", UriKind.Relative),
                'L' => new Uri(@"..\..\..\Pictures\white_king.png.", UriKind.Relative),
                'P' => new Uri(@"..\..\..\Pictures\white_pawn.png.", UriKind.Relative),
                _ => throw new Exception("Error"),
            };
        }

        private void Draw()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Rectangle square = new Rectangle();
                    square.Width = widthSquare;
                    square.Height = heightSquare;
                    square.Fill = currentSquareColor;
                    ChangeColor();
                    square.Stroke = Brushes.Black;
                    square.MouseDown += ClickedOnSpace;
                    Canvas.SetLeft(square, heightSquare * j);
                    Canvas.SetRight(square, widthSquare * i);
                    Canvas.SetTop(square, widthSquare * i);
                    chessField.Children.Add(square);
                }
                ChangeColor();
            }

            DrawFiguresOnField();
            WriteCurrentPlayerSide();
        }

        private void ChangeColor()
        {
            currentSquareColor = currentSquareColor == Brushes.Gray ? Brushes.White : Brushes.Gray;
        }

        private void ClickedOnSpace(object sender, MouseButtonEventArgs e)
        {
            if (currentFigure != null)
            {
                var point = e.GetPosition(this);
                var x = (int)point.X / 70;
                var y = (int)point.Y / 70;
                if (currentFigure.CanMove(x, y))
                {
                    field.Move(currentFigure, x, y);
                    if (currentGame.IsWin())
                        this.Close();
                    currentGame.ChangePlayer();
                    chessField.Children.Clear();
                    currentFigure = null;
                    Draw();
                }                
            }           
        }

        private void WriteCurrentPlayerSide()
        {
            textLabel.Content = currentGame.currentPlayer.PlayerSide == Side.White ? "Ходят белые" : "Ходят чёрные";
        }

        private void ClickedOnFigure(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(this);
            var x = (int)point.X / 70;
            var y = (int)point.Y / 70;
            if (currentFigure != null && currentFigure.Side == currentGame.currentPlayer.PlayerSide)
                TryKill(field.Figures[y, x]);
            else if (currentFigure != null || field.Figures[y, x].Side == currentGame.currentPlayer.PlayerSide)
            {
                currentFigure = field.Figures[y, x];
                if (currentFigure != null)
                    currentFigure.CurrentCoordinate = new Coordinate(x, y);
            }               
        }

        private void TryKill(Figure figure)
        {
            if (figure.Side != currentGame.currentPlayer.PlayerSide)
            {
                field.Kill(figure);
                field.Move(currentFigure, figure.CurrentCoordinate.X, figure.CurrentCoordinate.Y);
                if (currentGame.IsWin())
                    this.Close();
                Draw();
            }
                

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string info = currentGame.SaveGame();

            File.Delete("GameInfo.dat");
            BinaryWriter binaryWriter = new BinaryWriter(File.Open("GameInfo.dat", FileMode.OpenOrCreate));
            binaryWriter.Write(info);
            binaryWriter.Close();

            MainWindow menu = new MainWindow();
            menu.Show();
            Close();
        }
    }
}
