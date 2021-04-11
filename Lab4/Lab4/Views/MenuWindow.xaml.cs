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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4.Views {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window {
        public MenuWindow() {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void Play2PButton_Click(object sender, RoutedEventArgs e) {
            FillGridWindow w = new FillGridWindow(2);
            w.Owner = this;
            if(w.ShowDialog() == true) {
                GameWindow gm = new GameWindow(w.Grids[0], w.Grids[1], false);
                gm.Owner = this;
                gm.ShowDialog();
            }
        }

        private void PlayBotButton_Click(object sender, RoutedEventArgs e) {
            FillGridWindow w = new FillGridWindow(1);
            w.Owner = this;
            if(w.ShowDialog() == true) {
                GameWindow gm = new GameWindow(w.Grids[0], w.Grids[1], true);
                gm.Owner = this;
                gm.ShowDialog();
            }
        }
    }
}
