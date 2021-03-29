using System;
using System.Drawing;

namespace Lab3_WinForms {
    public class Line {
        public Rectangle Rectangle { private set; get; }
        private readonly Color LineColor;

        public Line(int x, int y, int width, int height, Color color) {
            Rectangle = new Rectangle(x, y, width, height);
            LineColor = color;
        }

        public bool IsInside(int x, int y) =>
            Rectangle.Contains(x, y);

        public bool IsInrersectWith(Rectangle r) => Rectangle.IntersectsWith(r);

        public void Draw(Graphics g) {
            g.FillRectangle(new SolidBrush(LineColor), Rectangle);
            g.DrawRectangle(Pens.Black, Rectangle);
        }

        public static Color[] Colors = new Color[] {
            Color.Red,
            Color.Blue,
            Color.Yellow,
            Color.Green,
            Color.Black,
            Color.Coral,
            Color.Indigo,
            Color.Lime,
            Color.Tomato
        };
    }
}
