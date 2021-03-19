using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab2 {
    class ExpandedButton : Button {
        
        private List<string> Data;
        private string Title;

        private int CurentIndex;

        public ExpandedButton(int x, int y, int width, int height, List<string> data, int curentIndex, bool isExpanded, ButtonCallback callback) {
            this.Callback = callback;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Data = data;
            this.CurentIndex = curentIndex;

            this.IsExpanded = isExpanded;
            this.Title = data[curentIndex];
        }

        public bool IsExpandedInside(int x, int y) =>
                IsExpanded && (x >= X - Width / 2 && x <= X - Width / 2 + Width * 2) && 
                (y >= Y + Height && y <= Y + Height + Height * Data.Count);
        

        public override void Click(int x, int y) {
            if (IsExpandedInside(x, y)) {
                int start = Y + Height;
                int index = 0;
                for (int i = 0; i < Data.Count; i++) {
                    if (y >= start && y <= start + Height) {
                        break;
                    }
                    index++;
                    start += Height;
                }
                Callback(index);
            }
        }

        public override void Draw(Graphics graphics) {
            graphics.FillRectangle(Brushes.LightGray, X, Y, Width, Height);

            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Font font = new Font(FontFamily.GenericSansSerif, Width / 6f);
            graphics.DrawString(Title, font, Brushes.Black, new RectangleF(X,Y, Width, Height), format);
            if (IsExpanded) {
                graphics.FillRectangle(Brushes.Gray, new Rectangle((X - Width / 2), Y + Height + 3, Width * 2 + 3, Height * Data.Count));
                graphics.FillRectangle(Brushes.LightGray, new Rectangle(X - Width / 2, Y + Height, Width * 2, Height * Data.Count));
                int y = Y + Height;
                for (int i = 0; i < Data.Count; i++) {
                    graphics.DrawString(Data[i], font, Brushes.Black, new RectangleF(X - Width / 2, y, Width * 2, Height), format);
                    y += Height ;
                }
            }
        }
    }
}
