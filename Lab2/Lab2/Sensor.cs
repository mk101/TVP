// https://educube.ru/news/1774/
using System;
using System.Drawing;
using System.Collections.Generic;

namespace Lab2 {

    class Sensor {
        public enum SensorType { Color, Light}
        public enum SensorSign { Equal, NotEqual, Less, LessEqual, More, MoreEqual}

        public event SensorTypeEventHandler TypeChanged;
        public event SensorSignEventHandler SignChanged;
        public event SensorInputValueEventHandler InputValueChanged;

        public static Color[] Colors = {
            Color.White,
            Color.Black,
            Color.Red,
            Color.Green,
            Color.Blue
        };

        private readonly int X, Y;
        private readonly int Width, Height;

        private SensorType Type;
        private int TypeIndex;

        private SensorSign Sign;
        private int SignIndex;

        private int Value;

        private ExpandedButton TypeButton;
        private ExpandedButton SignButton;
        private NumberButton ValueButton;

        private readonly Random Rand;

        public Sensor(int x, int y, int width, int height, SensorType type = SensorType.Light, SensorSign sign = SensorSign.Equal, int value = 1) {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            this.Type = type;
            this.Sign = sign;
            SetValue(value);

            this.TypeButton = null;
            this.SignButton = null;
            this.ValueButton = null;

            this.TypeIndex = 0;
            this.SignIndex = 0;

            this.Rand = new Random();
        }

        public void SetSensorType(SensorType type) {
            Type = type;
            TypeChanged?.Invoke(this, new SensorTypeEventArgs() { Type = Type });
        }
        public SensorType GetSensorType() => Type;

        public void SetSign(SensorSign sign) {
            Sign = sign;
            SignChanged?.Invoke(this, new SensorSignEventArgs() { Sign = Sign });
        }
        public SensorSign GetSign() => Sign;

        public void SetValue(int value) {
            if (value <= (Type == SensorType.Light ? 100 : Colors.Length - 1) && value >= 0) {
                Value = value;
            } else if (value > (Type == SensorType.Light ? 100 : Colors.Length-1)) {
                Value = (Type == SensorType.Light ? 100 : Colors.Length - 1);
            } else {
                Value = 0;
            }

            InputValueChanged?.Invoke(this, new SensorInputValueEventArgs() { InputValue = Value });
        }

        public int GetValue() => Value;

        public void Draw(Graphics graphics) {
            graphics.DrawRectangle(Pens.Black, X, Y, Width, Height);

            int elementCount = 3;
            int offset = Width/50;

            int elementWidth = Width / elementCount - 2 * offset;
            int elementHeight = Height/2 - 2 * offset;

            int localX = X + (Width - (elementCount*(elementWidth + offset) - offset)) / 2;
            int localY = Y + Height/2 + offset;

            graphics.DrawImage(Image.FromFile("datchik.png"), localX + elementWidth/4, localY - Height / 2, elementWidth/2, elementHeight);

            List<string> typeTitles = new List<string> {
                "Свет",
                "Цвет"
            };            
            TypeButton = new ExpandedButton(localX, localY, elementWidth, elementHeight, typeTitles, TypeIndex, TypeButton != null ? TypeButton.IsExpanded : false, (v) => { TypeIndex = v; ChangeType(v); });
            TypeButton.Draw(graphics);
            localX += elementWidth + offset;

            List<string> signTitles;
            if (Type == SensorType.Light) {
                signTitles = new List<string> {
                    "=",
                    "!=",
                    "<",
                    "<=",
                    ">",
                    ">="
                };
            } else {
                signTitles = new List<string> {
                    "=",
                    "!="
                };
            }
            SignButton = new ExpandedButton(localX, localY, elementWidth, elementHeight, signTitles, SignIndex, SignButton != null ? SignButton.IsExpanded : false, (v) => { SignIndex = v; ChangeSign(v); });
            SignButton.Draw(graphics);
            localX += elementWidth + offset;

            if (Type == SensorType.Color) {
                Brush colorBrush = null;
                switch (Value) {
                    case 0: colorBrush = Brushes.White; break;
                    case 1: colorBrush = Brushes.Black; break;
                    case 2: colorBrush = Brushes.Red;   break;
                    case 3: colorBrush = Brushes.Green; break;
                    default: colorBrush = Brushes.Blue; break;
                }
                graphics.FillRectangle(colorBrush, new Rectangle(localX + elementWidth/3 + offset - 2, localY - Height/3, elementWidth/4, elementHeight/2));
            } else {
                graphics.FillRectangle(Brushes.Black, new Rectangle(localX + elementWidth / 4, localY - Height/15 - (int)(elementHeight*Value/100f), elementWidth / 2, (int)(elementHeight * (Value / 100f))));
                graphics.DrawRectangle(Pens.Black, new Rectangle(localX + elementWidth / 4, localY - Height / 15 - elementHeight, elementWidth / 2, elementHeight));
            }

            ValueButton = new NumberButton(localX, localY, elementWidth, elementHeight, Value, (v) => SetValue(v));
            ValueButton.Draw(graphics);

            graphics.FillRectangle(Brushes.Yellow, X, Y, Width, Height / 10);
        }

        private void ChangeSign(int index) {
            switch (index) {
                case 0:  Sign = SensorSign.Equal; break;
                case 1:  Sign = SensorSign.NotEqual; break;
                case 2:  Sign = SensorSign.Less; break;
                case 3:  Sign = SensorSign.LessEqual; break;
                case 4:  Sign = SensorSign.More; break;
                default: Sign = SensorSign.MoreEqual; break;
            }

            SignChanged?.Invoke(this, new SensorSignEventArgs() { Sign = Sign });
        }

        private void ChangeType(int index) {
            switch (index) {
                case 0:     Type = SensorType.Light; break;
                default:    Type = SensorType.Color; break;
            }
            SetValue(0);

            TypeChanged?.Invoke(this, new SensorTypeEventArgs() { Type = Type });
            SignIndex = 0;
            SetSign(SensorSign.Equal);
        }

        public bool IsInside(int x, int y) {
            if (TypeButton != null && TypeButton.IsExpanded) {
                return ((x >= X && X + Width >= x) && (y >= Y && Y + Height >= y)) || TypeButton.IsExpandedInside(x, y);
            }

            if (SignButton != null && SignButton.IsExpanded) {
                return ((x >= X && X + Width >= x) && (y >= Y && Y + Height >= y)) || SignButton.IsExpandedInside(x, y);
            }

            return (x >= X && X + Width >= x) && (y >= Y && Y + Height >= y);
        }

        public void Click(int x, int y) {
            if (TypeButton.IsInside(x, y) || TypeButton.IsExpandedInside(x, y)) {
                TypeButton.Click(x, y);
                TypeButton.IsExpanded = !TypeButton.IsExpanded;
                if (TypeButton.IsExpanded) SignButton.IsExpanded = false;
            } else if (SignButton.IsInside(x, y) || SignButton.IsExpandedInside(x, y)) {
                SignButton.Click(x, y);
                SignButton.IsExpanded = !SignButton.IsExpanded;
                if (SignButton.IsExpanded) TypeButton.IsExpanded = false;
            } else if (ValueButton.IsInside(x, y)) {
                ValueButton.Click(x, y);
            }
        }

        public bool Calculate(out int sensorValue) {
            sensorValue = Rand.Next(0, (Type == SensorType.Light ? 100 : Colors.Length - 1) + 1);

            switch (Sign) {
                case SensorSign.Equal:      return sensorValue == Value;
                case SensorSign.NotEqual:   return sensorValue != Value;
                case SensorSign.Less:       return sensorValue < Value;
                case SensorSign.LessEqual:  return sensorValue <= Value;
                case SensorSign.More:       return sensorValue > Value;
                case SensorSign.MoreEqual:  return sensorValue >= Value;
                default:                    return sensorValue == Value;
            }
        }

        public class SensorTypeEventArgs : EventArgs {
            public SensorType Type { get; set; }
        }
        public delegate void SensorTypeEventHandler(object sender, SensorTypeEventArgs e);

        public class SensorSignEventArgs : EventArgs {
            public SensorSign Sign { get; set; }
        }
        public delegate void SensorSignEventHandler(object sender, SensorSignEventArgs e);

        public class SensorInputValueEventArgs : EventArgs {
            public int InputValue { get; set; }
        }
        public delegate void SensorInputValueEventHandler(object sender, SensorInputValueEventArgs e);
    }
}
