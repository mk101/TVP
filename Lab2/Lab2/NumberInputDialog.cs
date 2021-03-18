using System;
using System.Windows.Forms;

namespace Lab2 {
    static class NumberInputDialog {
        public static int ShowDialog(string caption, int value) {
            Form f = new Form() {
                Width = 200,
                Height = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            NumericUpDown numeric = new NumericUpDown() { Top = 20, Left = 30, Value = value, Minimum = 0, Maximum = 100, Width = 125 };
            System.Windows.Forms.Button button = new System.Windows.Forms.Button() { Text = "Ok", Left = 45, Top = 48, Width = 100, DialogResult = DialogResult.OK };
            button.Click += (sender, e) => { f.Close(); };
            f.Controls.Add(numeric);
            f.Controls.Add(button);

            return f.ShowDialog() == DialogResult.OK ? ((int)numeric.Value) : value;
        }
    }
}
