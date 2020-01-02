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
            WageService wageService = new WageService();
            var a = wageService.QueryDepartmentWage("zjc2016001", "2019-12-01");
            //Console.WriteLine(a[0].Wagetime);
            foreach (var aa in a) 
            {
                Console.WriteLine(aa.Wagetime.ToString()+" "+aa.Totalwage);
            }

            Console.ReadLine();
        }
    }
}
