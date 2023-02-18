﻿using Reversi.ViewModels;
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
    /// Interaction logic for GameLoadWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        public LoadGameWindow()
        {
            InitializeComponent();
            DataContext = new LoadGameMenu();
            (DataContext as LoadGameMenu).CloseHandler += (s, e) => Close();
        }
    }
}
