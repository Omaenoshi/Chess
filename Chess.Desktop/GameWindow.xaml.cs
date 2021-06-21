using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private string figures = "RKBQLBKRPPPPPPPP        PPPPPPPPRKBLQBKR";

        public GameWindow()
        {
            InitializeComponent();
            Show();
            Draw();
            Example();
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
                    //Border bd = new Border();
                    //bd.Width = widthSquare;
                    //bd.Height = heightSquare;

                    //var text = new TextBlock();
                    //text.Text = "Kolya";
                    //text.VerticalAlignment = VerticalAlignment.Center;
                    //text.TextAlignment = TextAlignment.Center;
                    //bd.Child = text;
                    BitmapImage the = new BitmapImage(new Uri(@"../../../black_bishop.jpg", UriKind.Relative));
                    ImageBrush myimage = new ImageBrush(the);
                    square.Fill = myimage;
                    ChangeColor();
                    square.Stroke = Brushes.Black;
                    square.MouseUp += Clicked;
                    Canvas.SetLeft(square, heightSquare * j);
                    Canvas.SetRight(square, widthSquare * i);
                    Canvas.SetTop(square, widthSquare * i);
                    //Canvas.SetLeft(bd, heightSquare * j);
                    //Canvas.SetRight(bd, widthSquare * i);
                    //Canvas.SetTop(bd, widthSquare * i);
                    chessField.Children.Add(square);
                    Figure figure = new Figure();
                    //chessField.Children.Add(bd);



                }
                ChangeColor();
            }
        }

        private void ChangeColor()
        {
            currentSquareColor = currentSquareColor == Brushes.Gray ? Brushes.White : Brushes.Gray;
        }

        private void Clicked(object sender, MouseButtonEventArgs e)
        {
            var sq = (Rectangle)sender;
            sq.Fill = Brushes.Red;
            var index = chessField.Children.IndexOf((Rectangle)sender);
        }

        private void DrawFigures()
        {

        }

        private void Example()
        {
            Border bd = new Border();
            bd.Width = widthSquare;
            bd.Height = heightSquare;
            BitmapImage the = new BitmapImage(new Uri(@"../../../imgw.jpg", UriKind.Relative));
            ImageBrush myimage = new ImageBrush(the);
            chessField.Background = myimage;

            //var text = new TextBlock();
            //text.Text = "Kolya";
            //text.VerticalAlignment = VerticalAlignment.Center;
            //text.TextAlignment = TextAlignment.Center;

            //Figure figure = new Figure();

            //bd.Child = text;
            ////bd.Child = figure;
            //bd.MouseUp += Clicked;
            //chessField.Children.Add(bd);
        }
    }
}
