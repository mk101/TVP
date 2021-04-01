using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3_WPF {
    public enum GameStatus { Play, Pause, Fail, Win }
    class GameManager {
        private readonly uint N;
        private readonly Size GameSize;
        private readonly Size LineSize;
        private List<Rectangle> Lines;
        private readonly Grid MainGrid;
        private readonly Random Rand;
        private readonly TextBlock ScoreLabel;
        private readonly TextBlock CountLabel;
        public GameStatus Status { private set; get; }

        private uint score;
        public uint Score { 
            private set {
                score = value;
                ScoreLabel.Text = value.ToString();
            }
            get => score;
        }
        public event Action Win;
        public event Action Fail;

        public GameManager(uint n, Size size, Grid grid, TextBlock scoreLabel, TextBlock countLabel) {
            Rand = new Random();
            N = n;
            Status = GameStatus.Pause;
            GameSize = size;
            score = 0;
            Lines = new List<Rectangle>();
            MainGrid = grid;
            ScoreLabel = scoreLabel;
            CountLabel = countLabel;

            LineSize = new Size(size.Width / 5, size.Height / 10);

            AddLines(n);
            CountLabel.Text = $"{n}/{2 * n}";
        }

        private void UpdateGrid() {
            MainGrid.Children.Clear();
            for (int i =0; i < Lines.Count; i++) {
                MainGrid.Children.Add(Lines[i]);
            }
        }

        public void AddLines(uint n) {
            for (uint i = 0; i < n; i++) {
                bool rev = Rand.Next(0, 2) == 1;
                var r = new Rectangle();
                if (!rev) {
                    //Lines.Add(new Line(Rand.Next(0, GameSize.Width - LineSize.Width), Rand.Next(0, GameSize.Height - LineSize.Height), LineSize.Width, LineSize.Height, Line.Colors[Rand.Next(0, Line.Colors.Length)]));
                    r = new Rectangle {
                        Width = LineSize.Width,
                        Height = LineSize.Height,
                        Margin = new Thickness(Rand.MyNextDouble(0, GameSize.Width - LineSize.Width), Rand.MyNextDouble(0, GameSize.Height - LineSize.Height), 0, 0),
                        Fill = Colors[Rand.Next(0, Colors.Length)]
                    };
                } else {
                    r = new Rectangle {
                        Width = LineSize.Height,
                        Height = LineSize.Width,
                        Margin = new Thickness(Rand.MyNextDouble(0, GameSize.Width - LineSize.Height), Rand.MyNextDouble(0, GameSize.Height - LineSize.Width), 0, 0),
                        Fill = Colors[Rand.Next(0, Colors.Length)]
                    };
                }
                r.MouseDown += Click;
                r.HorizontalAlignment = HorizontalAlignment.Left;
                r.VerticalAlignment = VerticalAlignment.Top;
                Lines.Add(r);
                UpdateGrid();
                CountLabel.Text = $"{Lines.Count}/{2 * N}";
            }
        }

        private void Click(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (Status != GameStatus.Play) return;

            int index = Lines.IndexOf(sender as Rectangle);
            if (CanDelete(index)) {
                Lines.RemoveAt(index);
                UpdateGrid();
                Score++;
                CountLabel.Text = $"{Lines.Count}/{2 * N}";
            }
        }

        public void Pause() {
            Status = GameStatus.Pause;
        }

        public void Play() {
            Status = GameStatus.Play;
        }

        public bool Check() {
            if (Lines.Count >= 2 * N) {
                Status = GameStatus.Fail;
                Fail?.Invoke();
                return false;
            }
            if (Lines.Count == 0) {
                Status = GameStatus.Win;
                MainGrid.Children.Clear();
                Win?.Invoke();
                return false;
            }
            return true;
        }

        private bool CanDelete(int start) {
            var r = new Rect((int)Lines[start].Margin.Left, (int)Lines[start].Margin.Top, (int)Lines[start].Width, (int)Lines[start].Height);
            for (int i = start+1; i < Lines.Count; i++) {
                var c = new Rect((int)Lines[i].Margin.Left, (int)Lines[i].Margin.Top, (int)Lines[i].Width, (int)Lines[i].Height);
                if (r.IntersectsWith(c)) {
                    return false;
                }
            }
            return true;
        }

        public void Tick() {
            if (Status == GameStatus.Play) {
                if (Check()) {
                    AddLines(1);
                }
            }
        }

        public void Restart() {
            Score = 0;
            Lines = new List<Rectangle>();
            AddLines(N);

            Status = GameStatus.Play;
        }

        public static Brush[] Colors = new Brush[] {
            Brushes.Red,
            Brushes.Blue,
            Brushes.Yellow,
            Brushes.Green,
            Brushes.Black,
            Brushes.Coral,
            Brushes.Indigo,
            Brushes.Lime,
            Brushes.Tomato
        };
    }

    public static class RandomExtension {
        public static double MyNextDouble(this Random r, double start, double end) {
            return (end - start) * r.NextDouble() + start;
        }
    }
}
