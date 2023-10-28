using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            int numRows = field.GetLength(0);
            int numCols = field.GetLength(1);
            int maxArea = 0;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (field[i, j] == 0)
                    {
                        int area = ExploreSafeZone(i, j, numRows, numCols);
                        maxArea = Math.Max(maxArea, area);
                    }
                }
            }

            return maxArea;
        }

        private int ExploreSafeZone(int row, int col, int numRows, int numCols)
        {
            int maxArea = 0;
            int height = 0;

            for (int i = row; i < numRows; i++)
            {
                if (field[i, col] == 1)
                {
                    break;
                }

                int width = 0;

                for (int j = col; j < numCols; j++)
                {
                    if (field[i, j] == 1)
                    {
                        break;
                    }

                    width++;
                }

                height++;
                maxArea = Math.Max(maxArea, width * height);
            }

            return maxArea;
        }
    }
}
