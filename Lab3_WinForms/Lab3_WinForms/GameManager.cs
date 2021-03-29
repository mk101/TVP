using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab3_WinForms {
    public enum GameStatus { Play, Pause, Fail, Win}
    
    public class GameManager {
        private readonly uint N;
        private readonly Size GameSize;
        private readonly Graphics FormGraphics;
        private readonly Size LineSize;
        private List<Line> Lines;
        private readonly Random Rand;
        public GameStatus Status { private set; get; }
        public uint Score { private set; get; }
        public event Action Win;
        public event Action Fail;

        public GameManager(uint n, Size size, Graphics graphics) {
            Rand = new Random();
            N = n;
            Status = GameStatus.Pause;
            GameSize = size;
            FormGraphics = graphics;
            Score = 0;
            Lines = new List<Line>();

            LineSize = new Size(size.Width / 5, size.Height/10);

            AddLines(n);
        }

        public void AddLines(uint n) {
            for (uint i = 0; i < n; i++) {
                bool rev = Rand.Next(0, 2) == 1;
                if (!rev) {
                    Lines.Add(new Line(Rand.Next(0, GameSize.Width - LineSize.Width), Rand.Next(0, GameSize.Height - LineSize.Height), LineSize.Width, LineSize.Height, Line.Colors[Rand.Next(0, Line.Colors.Length)]));
                } else {
                    Lines.Add(new Line(Rand.Next(0, GameSize.Width - LineSize.Height), Rand.Next(0, GameSize.Height - LineSize.Width), LineSize.Height, LineSize.Width, Line.Colors[Rand.Next(0, Line.Colors.Length)]));
                }
            }
        }

        public void Pause() {
            Status = GameStatus.Pause;
            ClearCanvas();
            foreach (var l in Lines) {
                l.Draw(FormGraphics);
            }
            DrawGUI();
        }
        
        public void Play() {
            Status = GameStatus.Play;
            ClearCanvas();
            foreach (var l in Lines) {
                l.Draw(FormGraphics);
            }
            DrawGUI();
        }

        public void Check() {
            if (Lines.Count >= 2 * N) {
                Status = GameStatus.Fail;
                Fail?.Invoke();
            }
            if (Lines.Count == 0) {
                Status = GameStatus.Win;
                Win?.Invoke();
            }
        }

        private void ClearCanvas() =>
            FormGraphics.Clear(Color.White);

        private void DrawGUI() {
            FormGraphics.DrawString($"{Lines.Count}/{2 * N}", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), Brushes.Black, new PointF(10,10));
            FormGraphics.DrawString($"{Score}", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), Brushes.Black, new PointF(10, 40));
            if (Status == GameStatus.Pause) {
                FormGraphics.DrawString("Pause", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), Brushes.Black, new PointF(GameSize.Width - 110, 10));
            }
        }

        private bool CanDelete(int start) {
            for (int i = start+1; i < Lines.Count; i++) {
                if (Lines[start].IsInrersectWith(Lines[i].Rectangle)) {
                    return false;
                }
            }
            return true;
        }

        public void Click(int x, int y) {
            if (Status != GameStatus.Play) return;
            for (int i =0; i < Lines.Count; i++) {
                if (Lines[i].IsInside(x,y)) {
                    if (CanDelete(i)) {
                        Lines.RemoveAt(i);
                        ClearCanvas();
                        foreach (var l in Lines) {
                            l.Draw(FormGraphics);
                        }
                        Score++;
                        DrawGUI();
                        Check();
                        break;
                    }
                }
            }
        }

        public void Tick() {
            if (Status == GameStatus.Play) {
                ClearCanvas();

                Check();

                foreach (var l in Lines) {
                    l.Draw(FormGraphics);
                }

                DrawGUI();
                AddLines(1);
            }
        }

        public void Restart() {
            Score = 0;
            Lines = new List<Line>();
            AddLines(N);

            Status = GameStatus.Play;
            ClearCanvas();
        }
    }
}
