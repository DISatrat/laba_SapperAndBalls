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
        private Balls balls;
        public Form1()
        {
            InitializeComponent();


            Stopwatch stopWatchSap = new Stopwatch();


            stopWatchSap.Start();
            sap = new Sapper(3);
            InitializeDataGridView();

            label2.Text = sap.FindMaxSafeZone().ToString();
            stopWatchSap.Stop();

            long timeSap = stopWatchSap.ElapsedMilliseconds;
            label1.Text = timeSap + "мс".ToString();


            //не считается время
            Stopwatch stopWatchBalls = new Stopwatch();


            stopWatchBalls.Start();

            balls = new Balls(20);

            int ballCount = balls.BallCount;

            int[] ballHeights = balls.BallHeights;

            listView1.Items.Clear();

            for (int i = 0; i < ballCount; i++)
            {
                ListViewItem item = new ListViewItem();

                item.Text = ballHeights[i].ToString();
                listView1.Items.Add(item);
            }
            int l = balls.LCM(ballHeights);
            int answer = 604800 / l;

            stopWatchBalls.Stop();

            long ballsTime = stopWatchBalls.ElapsedMilliseconds;

            label4.Text = answer.ToString();
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

            for (int n = 5000; n <= 10000; n+=500)
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
            Stopwatch stopWatchBalls = new Stopwatch();

            stopWatchBalls.Restart();

            Balls balls = new Balls(n);
            int ballCount = balls.BallCount;

            int[] ballHeights = balls.BallHeights;

            int l = balls.LCM(ballHeights);
            int answer = 604800 / l;

            stopWatchBalls.Stop();

            long ballsTime = stopWatchBalls.ElapsedMilliseconds;

            return ballsTime;
        }

        public long TimeTestForAnyNSapper(int n)
        {
            Sapper sap = new Sapper(n);
            Stopwatch stopWatchSap = new Stopwatch();


            stopWatchSap.Start();
            sap = new Sapper(5);
            string s = sap.FindMaxSafeZone().ToString();
            stopWatchSap.Stop();

            long timeSap = stopWatchSap.ElapsedMilliseconds;

            return timeSap;
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
