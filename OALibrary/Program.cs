using OALibrary.Infrastructure;
using OALibrary.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALibrary
{
    public class Program
    {
        public static void Main()
        {


            WageRepository wageRepository = new WageRepository();
            var a = wageRepository.QueryAllbyYear("2020");
            //Console.WriteLine(a[0].Wagetime);
            foreach (var aa in a) 
            {
                Console.WriteLine(aa.Wagetime.ToString()+" "+aa.Totalwage);
            }
            Console.ReadLine();
        }
    }
}
