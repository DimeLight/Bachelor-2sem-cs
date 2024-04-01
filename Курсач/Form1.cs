using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;

namespace Курсач
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double[] Converttodouble(string[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                y[i] = Convert.ToDouble(x[i]);
            return y;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataTable1.AcceptChanges();
            if (tabControl1.SelectedIndex == 1)
            {
                var model = new PlotModel { Title = "График" };
                double[] X0 = new double[dataTable1.Rows.Count], Y0 = new double[dataTable1.Rows.Count], Z0 = new double[dataTable1.Rows.Count];
                double[,] values = new double[dataTable1.Rows.Count, dataTable1.Columns.Count];
                int i = 0;
                foreach (DataRow row in dataTable1.Rows)
                {
                    if (i < dataTable1.Rows.Count)
                    {
                        for (int j = 0; j < dataTable1.Columns.Count; j++)
                        {
                            values[i, j] = (double)row[j]; //передача данных из dataTable1 в массив values
                            //и отдельно по столбцам
                            if (j == 0)
                                X0[i] = values[i, j];
                            if (j == 1)
                                Y0[i] = values[i, j];
                            if (j == 2)
                                Z0[i] = values[i, j];
                        }
                        i++;
                    }
                }


                //генерация значений
                Func<double, double, double> peaks = (x, y) => 3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1)) - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y) - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
                var peaksData = ArrayBuilder.Evaluate(peaks, X0, Y0);

                var cs = new ContourSeries
                {
                    Color = OxyColors.Black,
                    LabelBackground = OxyColors.White,
                    ColumnCoordinates = Y0,
                    RowCoordinates = X0,
                    Data = peaksData
                };

                model.Series.Add(cs);
                plotView1.Model = model;
            }
        }
    }
}

