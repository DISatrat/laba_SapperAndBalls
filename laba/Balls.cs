using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4_sapper
{
    internal class Balls
    {
        private int[] ballsHeigh;

        public Balls(int balls) {
            
            ballsHeigh= new int[balls];

            Random random= new Random();

            for (int i = 0; i < balls; i++)
            {

                ballsHeigh[i] = random.Next(1,500);
            }
        }

        //наименьшее общее кратное
        public int LCM(int[] numbers)
        {
            int lcm = 1;

            foreach (int number in numbers)
            {
                lcm = GetLCM(lcm, number);
            }

            return lcm;
        }

       public  int GetLCM(int a, int b)
        {
            //int max = Math.Max(a, b);
            //int min = Math.Min(a, b);

            //int lcm = max;

            //while (lcm % min != 0)
            //{
            //    lcm += max;
            //}

            //return lcm;


            return (a * b) / GCD(a, b);
        }

        //наибольший общий делитель
        public int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }


        public int BallCount { get { return ballsHeigh.Length; } }
        public int[] BallHeights { get { return ballsHeigh; } }

    }
}
