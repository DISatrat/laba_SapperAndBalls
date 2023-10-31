using Laba4_sapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace laba
{
    public partial class Form1 : Form
    {
        private Sapper sap;
        public Form1()
        {
            InitializeComponent();


            Stopwatch stopWatchSap = new Stopwatch();


            sap = new Sapper(7);
            InitializeDataGridView();

            label2.Text = sap.MaximalRectangle().ToString();
            stopWatchSap.Stop();

            long timeSap = stopWatchSap.ElapsedMilliseconds;
            label1.Text = timeSap + "мс".ToString();



            Stopwatch stopWatchBalls = new Stopwatch();

            stopWatchBalls.Start();
            
            Balls[] balls=new Balls[20];
            
            int l = Balls.CountBalls7Days(balls);


            if (l == 0)
            {
                label4.Text = "0";

            }
            else
            {
                label4.Text = (604800 / l).ToString();
            }


            listView1.Items.Clear();

            for (int i = 0; i < balls.Length; i++)
            {
                ListViewItem item = new ListViewItem();

                item.Text = balls[i].height.ToString();
                listView1.Items.Add(item);
            }

            stopWatchBalls.Stop();

            long ballsTime = stopWatchBalls.ElapsedMilliseconds;

            label3.Text = ballsTime + "мс".ToString();


            //делаем график 

            ChartArea chartArea = new ChartArea();
            chart1.ChartAreas.Add(chartArea);

            chartArea.AxisX.Name = "n";
            chartArea.AxisY.Name = "T(n)";

            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            
            Series seriesBalls = new Series("Balls Algorithm");
            seriesBalls.ChartType = SeriesChartType.Spline; 

            Series seriesSapper = new Series("Sapper Algorithm");
            seriesSapper.ChartType = SeriesChartType.Spline;

            for (int n = 1; n <= 20; n++)
            {
                long ballsNTime = TimeTestForAnyNBalls(n);
                long sapperNTime = TimeTestForAnyNSapper(n);

                seriesBalls.Points.AddXY(ballsNTime, n);
                seriesSapper.Points.AddXY(sapperNTime, n);
            }
          
            chart1.Series.Add(seriesBalls);
            chart1.Series.Add(seriesSapper);


            chart1.Invalidate();


        }

        public long TimeTestForAnyNBalls(int n)
        {

            Stopwatch watch = new Stopwatch();

            Balls[] balls = new Balls[n];
            
            Balls.CountBalls7Days(balls);
            watch.Stop();

            return watch.ElapsedMilliseconds;
        }

        public long TimeTestForAnyNSapper(int n)
        {
            Sapper sap = new Sapper(n);
            Stopwatch stopWatchSap = new Stopwatch();
            stopWatchSap.Start();
            string s = sap.MaximalRectangle().ToString();
            stopWatchSap.Stop();

            return stopWatchSap.ElapsedMilliseconds;
        }

        private void InitializeDataGridView()
        {
            dataGridView1.ColumnCount = sap.Field.GetLength(1);
            dataGridView1.RowCount = sap.Field.GetLength(0);

            for (int i = 0; i < sap.Field.GetLength(0); i++)
            {
                for (int j = 0; j < sap.Field.GetLength(1); j++)
                {
                    dataGridView1[j, i].Value = sap.Field[i, j].ToString();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }


    }
}
