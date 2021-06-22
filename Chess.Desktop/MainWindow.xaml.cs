using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess.Logic;

namespace Chess.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            BinaryReader binaryReader = new BinaryReader(File.Open("GameInfo.dat", FileMode.Open));
            GameWindow gameWindow = new GameWindow(new Game(binaryReader.ReadString()));
            binaryReader.Close();
            this.Close();
            gameWindow.Show();
        }

        private void RunGame_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(new Game());
            this.Close();
            gameWindow.Show();
            
        }
    }
}
