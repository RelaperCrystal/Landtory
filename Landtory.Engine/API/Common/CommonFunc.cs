using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Landtory.Engine.API.Common
{
    public class CommonFunc
    {
        public static int GetRandomNumber(int min, int max)
        {
            Random SeedRandom = new Random();
            int seed = SeedRandom.Next();
            Random random2 = new Random(seed);
            return random2.Next(min, max);
        }

        public static bool GetRandomBool(int min = 0, int max = 3, int target = 2)
        {
            int Randomnew = GetRandomNumber(min, max);
            if (Randomnew == target) return true;
            else return false;
        }
    }
}
