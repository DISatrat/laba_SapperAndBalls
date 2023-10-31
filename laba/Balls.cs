using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4_sapper
{
    internal class Balls
    {
        //private int[] ballsHeigh;
        public int direction { get; set; } // 1 - вверх, -1 - вниз

        public int height;
        public int startHeight { get; set; }

        public Balls(int start)
        {
            direction = -1;

            startHeight = start;
            height = start;

            //Random random = new Random();

            //startHeight = random.Next(5, 51);
            //height = startHeight;


        }



        public static int CountBalls7Days(Balls[] balls)
        {
            Random random = new Random();
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Balls(random.Next(2, 60));
            }

            int count = 0;

            for (int i = 0; i < 7; i++) // симулируем неделю
            {
                for (int j = 0; j < 24; j++)
                {
                    for (int k = 0; k < 60; k++)
                    {
                        for (int l = 0; l < 60; l++)
                        {
                            List<int> heights = new List<int>();

                            for (int t = 0; t < balls.Length; t++)
                            {
                                if (balls[t].direction == -1) // падение мячика
                                {
                                    balls[t].height--;
                                }
                                else
                                {
                                    balls[t].height++;
                                }


                                if (balls[t].height == 0) // отскок мячика
                                {
                                    balls[t].direction = 1;
                                }
                                else if (balls[t].height == balls[t].startHeight) // мячик вернулся на исходную высоту
                                {
                                    balls[t].direction = -1;
                                }

                                if (!heights.Contains(balls[t].height))
                                {
                                    heights.Add(balls[t].height);
                                }
                            }

                            if (heights.Count == 1)
                            {
                                count++;
                            }
                        }
                    }
                }

            }

            return count;
        }



        //public int BallCount { get { return ballsHeigh.Length; } }
        //public int[] BallHeights { get { return ballsHeigh; } }
        public int Height { get { return height; } }
        public int Diraction { get { return direction; } }

    }
}
