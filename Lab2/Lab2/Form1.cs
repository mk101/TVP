using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lab2 {
    public partial class Form1 : Form {
        private Sensor Sensor1;
        private Sensor Sensor2;

        public Form1() {
            InitializeComponent();

            Sensor1 = new Sensor(20, 20, 300, 100);
            Thread.Sleep(10);
            Sensor2 = new Sensor(350, 200, 200, 50);
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = this.CreateGraphics();

            Sensor1.Draw(graphics);
            Sensor2.Draw(graphics);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {
            if (Sensor1.IsInside(e.X,e.Y)) {
                Sensor1.Click(e.X, e.Y);
            } else if (Sensor2.IsInside(e.X,e.Y)) {
                Sensor2.Click(e.X, e.Y);
            }
            Invalidate();
        }

        private void button_Click(object sender, EventArgs e) {
            int sensor1Value = 0;
            int sensor2Value = 0;

            bool res1 = Sensor1.Calculate(out sensor1Value);
            bool res2 = Sensor2.Calculate(out sensor2Value);

            label.Text = $"Результат:\nSensor1({(Sensor1.GetSensorType() == Sensor.SensorType.Light ? sensor1Value.ToString() : Sensor.Colors[sensor1Value].ToString())}) - {res1}\nSensor2({(Sensor2.GetSensorType() == Sensor.SensorType.Light ? sensor2Value.ToString() : Sensor.Colors[sensor2Value].ToString())}) - {res2}";
        }
    }
}
