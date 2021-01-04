using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerbNurbsSharp.Core
{
    public class Binomial
    {
        public static double Get(int n, int k)
        {
            if (k == 0) return 1.0d;
            if (n == 0 || k > n) return 0.0d;
            if (k > n - k)
                k = n - k;
            if (MemoExists(n, k))
                return GetMemo(n, k);
            double r = 1.0d;
            var n_o = n;
            for (int d = 1; d < k+1; d++)
            {
                if (MemoExists(n_o, d))
                {
                    n--;
                    r = GetMemo(n_o, d);
                    continue;
                }
                r *= n--;
                r /= d;
                Memorize(n_o, d, r);
            }
            return r;
        }

        private static void Memorize(int n_o, int d, double r)
        {
            throw new NotImplementedException();
        }

        private static double GetMemo(int n, int k)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private static double GetNoMemo(int n, int k)
        {
            if (k == 0) return 1.0d;
            if (n == 0 || k > n) return 0.0d;
            if (k > n - k)
                k = n - k;
           
            double r = 1.0d;
            var n_o = n;
            for (int d = 1; d < k + 1; d++)
            {
                r *= n--;
                r /= d;
            }
            return r;
        }

        private static bool MemoExists(int n, int k) => ()
        {
            throw new NotImplementedException();
        }
    }
}
