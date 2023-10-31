using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4_sapper
{
    internal class Sapper
    {
        private int[,] field;

        public Sapper(int v)
        {
            field=new int [v,v];
            
            Random random = new Random();

            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    field[i, j] = random.Next(2);
                }
            }
        }

        public int MaximalRectangle()
        {
            
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            int[] left = new int[cols];
            int[] right = new int[cols];
            int[] height = new int[cols];

            for (int i = 0; i < right.Length; i++)
            {
                right[i] = cols;
            }

            int maxArea = 0;

            for (int i = 0; i < rows; i++)
            {
                int currentLeft = 0;
                int currentRight = cols;

                for (int j = 0; j < cols; j++)
                {
                    if (field[i, j] == 1)
                    {
                        height[j] = 0;
                        left[j] = 0;
                        currentLeft = j + 1;
                    }
                    else
                    {
                        height[j]++;
                        left[j] = Math.Max(left[j], currentLeft);
                    }
                }

                for (int j = cols - 1; j >= 0; j--)
                {
                    if (field[i, j] == 1)
                    {
                        right[j] = cols;
                        currentRight = j;
                    }
                    else
                    {
                        right[j] = Math.Min(right[j], currentRight);
                    }

                    maxArea = Math.Max(maxArea, height[j] * (right[j] - left[j]));
                }
            }

            return maxArea;
        }


        public void PrintField()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public int[,] Field
        {
            get { return field; }
        }


        //неправильно выполняется алгоритм при больших n
        public int FindMaxSafeZone()
        {
            var a = (int[,])field.Clone();

            List<int> allSafeZone = new List<int>();

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == 0)
                    {
                        allSafeZone.Add(ExploreSafeZone(ref a, i, j));
                    }

                }
            }
            return allSafeZone.Max();
        }

        private int ExploreSafeZone(ref int[,] mas, int x, int y)
        {
            int maxArea = 0;

            if (mas[x, y] == 0)
            {
                mas[x, y] = 1;

                maxArea++;
                if (x != 0)
                {

                    maxArea += ExploreSafeZone(ref mas, x - 1, y);
                }
                if (x != mas.GetLength(0) - 1)
                {
                    maxArea += ExploreSafeZone(ref mas, x + 1, y);

                }
                if (y != 0)
                {
                    maxArea += ExploreSafeZone(ref mas, x, y - 1);

                }
                if (y != mas.GetLength(1) - 1)
                {
                    maxArea += ExploreSafeZone(ref mas, x, y + 1);

                }
            }




            return maxArea;
        }


    }
}
