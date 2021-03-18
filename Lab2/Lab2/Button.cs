using System;
using System.Drawing;

namespace Lab2 {
    public delegate void ButtonCallback(int value);

    abstract class Button {
        protected ButtonCallback Callback;
        protected int X, Y;
        protected int Width, Height;
        public bool IsExpanded;

        public bool IsInside(int x, int y) =>
            (x >= X && X + Width >= x) && (y >= Y && Y + Height >= y);

        public abstract void Draw(Graphics graphics);
        public abstract void Click(int x, int y);
    }
}
