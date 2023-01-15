using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjWeb.Models
{
    public class Class1
    {
        public string getnumber()
        {
            Console.WriteLine("888");
            Random r = new Random();    //產生出來是double
            int cp;
            string lotto = "";
            string lotto1 = "";
            int[] ar = new int[6];
            for (int i = 0; i < 6; i++)
            {
                int n = r.Next(1, 50);
                while (Array.IndexOf(ar, n) >= 0)
                {
                    n = r.Next(1, 50);
                    cp = Array.IndexOf(ar, n);
                    lotto1 += $"{cp}. ";
                }
                cp = Array.IndexOf(ar, n);
                ar[i] = n;
                lotto1 += $"{cp}. ";
            }

            for (int i = 0; i < ar.Length; i++)
            {
                for (int j = 0; j < ar.Length - 1; j++)
                {
                    if (ar[j] > ar[j + 1])
                    {
                        int big = ar[j];
                        ar[j] = ar[j + 1];
                        ar[j + 1] = big;
                    }
                }

            }

            foreach (int i in ar)
            {
                lotto += $"{i}. ";
            }
            return lotto + "||" + lotto1;
        }

    }
}