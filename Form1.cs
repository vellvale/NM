using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NM
{
    public partial class Form1 : Form
    {
        double T = 100, T0 = 0, n = 1000, X0 = 0.2;
        List<double> findPoints = new List<double>();

        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            //chart1.ChartAreas[0].AxisY.Minimum = 0;
        }

        void drawPlot(List<double> p)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(T0, X0);
            for (int i = 0; i < n; i++)
            {
                chart1.Series[0].Points.AddXY((i + 1) * ((T - T0) / n), p[i]);
            }
        }

        List<double> solution()
        {
            List<double> points = new List<double>();
            double x = X0, Xn, k1 = 0, k2 = 0, k3 = 0, k4 = 0;

            for(int i = 0; i < n; i++)
            {
                points.Add(x);
                k1 = K1(x);
                k2 = K2(x, k1);
                k3 = K3(x, k1, k2);
                k4 = K4(x, k1, k2, k3);
                Xn = x + 1.0/8 * K1(x) + 3.0/8 * k2 + 3.0/8 * k3 + 1.0/8 * k4;
                x = Xn;
            }
            
            return points;
        }

        double func(double x)
        {
            double f = (Math.Exp(-x) + Math.Sin(3 * x)) * Math.Atan(x) / (Math.Sin(x) + 3);
            
            return f;
        }

        double K1(double Xm)
        {
            double h = (T - T0) / n;
            double K = h * func(Xm);

            return K;
        }
        double K2(double Xm, double k1)
        {
            double h = (T - T0) / n;
            double K = h * func(Xm + 1/3 * k1);
            return K;
        }
        double K3(double Xm, double k1, double k2)
        {
            double h = (T - T0) / n;
            double K = h * func(Xm - 1/3 * k1 + k2);
            return K;
        }
        double K4(double Xm, double k1, double k2, double k3)
        {
            double h = (T - T0) / n;
            double K = h * func(Xm + k1 - k2 + k3);
            return K;
        }

        #region style
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "T = 100")
                T = Convert.ToDouble(textBox4.Text);
            if (textBox1.Text != "T0 = 0")
                T0 = Convert.ToDouble(textBox1.Text);
            if (textBox2.Text != "X0 = 0.2")
                X0 = Convert.ToDouble(textBox2.Text);
            if (textBox3.Text != "n = 1000")
                n = Convert.ToDouble(textBox3.Text);

            findPoints = solution();

            if (findPoints.Count > 0)
                drawPlot(findPoints);
            else
                MessageBox.Show("точки решения не найдены");
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "T0 = 0")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == " " || textBox1.Text == "")
            {
                textBox1.Text = "T0 = 0";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "X0 = 0.2")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == " " || textBox2.Text == "")
            {
                textBox2.Text = "X0 = 0.2";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "n = 1000")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == " " || textBox3.Text == "")
            {
                textBox3.Text = "n = 1000";
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "T = 100")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {

            if (textBox4.Text == " " || textBox4.Text == "")
            {
                textBox4.Text = "T = 100";
                textBox4.ForeColor = Color.Gray;
            }
        }
        #endregion
    }
}
