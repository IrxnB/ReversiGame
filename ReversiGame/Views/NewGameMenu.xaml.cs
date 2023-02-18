using Reversi.ViewModels;
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
    /// Interaction logic for NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        public NewGameWindow()
        {
            InitializeComponent();
            DataContext = new NewGameMenu();
            (DataContext as NewGameMenu).CloseWindowHandler += CloseWindowListener;
        }
        public void CloseWindowListener(object e, EventArgs args)
        {
            if (DataContext.Equals(e)) this.Close();
        }
    }
}
