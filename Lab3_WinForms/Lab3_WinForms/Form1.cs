using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3_WinForms {
    public partial class Form1 : Form {
        private readonly GameManager Manager;
        public Form1() {
            InitializeComponent();            
            Manager = new GameManager(5, Size, CreateGraphics());
            GameTimer.Interval = 1000; // .5s
            GameTimer.Start();
            GameTimer.Tick += GameTimer_Tick;

            Manager.Play();
            Manager.Win += Manager_Win;
            Manager.Fail += Manager_Fail;
        }

        private void Manager_Fail() {
            GameTimer.Stop();
            if (MessageBox.Show("Попробовать еще раз?", "Вы проиграли!", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                GameTimer.Start();
                Manager.Restart();
            }
        }

        private void Manager_Win() {
            GameTimer.Stop();
            if (MessageBox.Show("Попробовать еще раз?", "Победа!", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                GameTimer.Start();
                Manager.Restart();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            Manager.Tick();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == ' ' && Manager.Status == GameStatus.Play) {
                GameTimer.Stop();
                Manager.Pause();
            } else if (e.KeyChar == ' ' && Manager.Status == GameStatus.Pause) {
                GameTimer.Start();
                Manager.Play();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {
            Manager.Click(e.X, e.Y);
        }
    }
}
