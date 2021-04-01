using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;

namespace Lab3_WPF {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly GameManager Manager;
        private readonly DispatcherTimer Timer;
        public MainWindow() {
            InitializeComponent();
            
            Manager = new GameManager(5, new Size(Width, Height), Content, ScoreLabel, CountLabel);
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0,0,1); // 1s
            Timer.Tick += Timer_Tick;
            Timer.Start();
            Manager.Play();

            Manager.Fail += Manager_Fail;
            Manager.Win += Manager_Win;
        }

        private void Manager_Win() {
            Timer.Stop();
            if (MessageBox.Show("Сыграть еще раз?", "Победа", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                Manager.Restart();
                Timer.Start();
            }
        }

        private void Manager_Fail() {
            Timer.Stop();
            if (MessageBox.Show("Сыграть еще раз?", "Проигрыш", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                Manager.Restart();
                Timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e) {
            Manager.Tick();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Space) {
                if (Manager.Status == GameStatus.Play) {
                    Timer.Stop();
                    Manager.Pause();
                    PauseLabel.Visibility = Visibility.Visible;
                } else if (Manager.Status == GameStatus.Pause){
                    Timer.Start();
                    Manager.Play();
                    PauseLabel.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
