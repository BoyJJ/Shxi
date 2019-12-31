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
            PlantableService plantableService = new PlantableService();
            var planList = plantableService.QueryWorkPlan("T16060201001", 5);
            Console.WriteLine(planList[0].Teacherid);
            Console.ReadLine();
        }
    }
}
