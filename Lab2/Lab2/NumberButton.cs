using System;
using System.Drawing;


namespace Lab2 {
    class NumberButton : Button {
        private int Data;

        public NumberButton(int x, int y, int width, int height, int data, ButtonCallback callback) {
            this.Callback = callback;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            Data = data;
            this.IsExpanded = false;
        }

        public override void Click(int x, int y) {
            Data = NumberInputDialog.ShowDialog("Введите новое значение", Data);
            Callback(Data);
        }

        public override void Draw(Graphics graphics) {
            graphics.FillRectangle(Brushes.LightGray, X, Y, Width, Height);

            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            graphics.DrawString(Data.ToString(), new Font(FontFamily.GenericSansSerif, Width/6f), Brushes.Black, new RectangleF(X, Y, Width, Height), format);
        }
    }
}
