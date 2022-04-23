using System;
using System.Collections.Generic;

namespace Bamboo.Server.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class StringToIntComparer : IComparer<string>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            int X, Y;

            // Get the fractional part of the first number.
            try
            {
                X = int.Parse(x);
            }
            catch
            {
                X = 0;
            }
            // Get the fractional part of the second number.
            try
            {
                Y = int.Parse(y);
            }
            catch
            {
                Y = 0;
            }
            if (X == 0 && Y == 0)
                return 0;
            else if (X == 0)
                return 1;
            else if (Y == 0)
                return -1;
            else if (X > Y)
                return 1;
            else
                return -1;

        }
    }
}
