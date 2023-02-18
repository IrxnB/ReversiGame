using Reversi.ViewModels;
using ReversiLib;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Reversi.Views
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(int width, int height)
        {
            InitializeComponent();
            DataContext = new GameVM(width, height);
            init();
        }

        public GameWindow(ReversiGame game)
        {
            InitializeComponent();
            DataContext = new GameVM(game);
            init();
        }

        private void init()
        {
            var dc = ((GameVM)DataContext);
            var field = dc.Game.Field;
            Binding curImgBinding = new Binding("IsFirstPlayerTurn");
            curImgBinding.Source = dc.Game;
            curImgBinding.Converter = new TurnImageSourceConverter();
            CurrentTurn.Image.SetBinding(Image.SourceProperty, curImgBinding);
            for(int row = 0; row < dc.Game.Height; row++)
            {
                var dock = new DockPanel();
                for (int column = 0; column < dc.Game.Width; column++)
                {
                    int y = row;
                    int x = column;
                    GameCell cell = new GameCell();
                    Binding binding = new Binding("State");
                    binding.Source = field[y][x];
                    binding.Converter = new StateImageConverter();
                    cell.Image.SetBinding(Image.SourceProperty, binding);
                    cell.Button.Click += (s, e) => dc.Game.MakeTurn(y, x);
                    dock.Children.Add(cell);
                }
                mainPanel.Children.Add(dock);

                mainPanel.Width = dc.Game.Width * 70;
                mainPanel.Height = dc.Game.Height * 70;
            }
            dc.CloseWindowHandler += (s, e) => { MessageBox.Show("Game succesfully saved"); Close(); };
            dc.GameOverHandler += (s, e) => 
            {
                var args = e as GameOverEventArgs;
                var text = args.FirstScore == args.SecondScore ? "Draw!"
                : $"{(args.FirstScore > args.SecondScore ? "First" : "Second")} player won!";
                MessageBox.Show($"Game over. {text}");
                Close();
            };
        }
    }
}
