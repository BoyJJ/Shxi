using OALibrary.Infrastructure;
using System;
using Xunit;


namespace XUnitTest
{
    public class WageTest
    {
        [Fact]
        public void TestAddWage() 
        {
            WageRepository wageRepository = new WageRepository();
            wageRepository.Add("T13030201034", "2019-6-01", 5000,5000,5000,5000);
        }

    }
}
