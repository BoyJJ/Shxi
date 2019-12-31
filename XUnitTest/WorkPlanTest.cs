using OALibrary.Infrastructure;
using System;
using Xunit;

namespace XUnitTest
{
    public class WorkPlanTest
    {
        [Fact]
        public void TestQueryAllPlantable()
        {
            PlantableRepository plantableRepository = new PlantableRepository();
            var a = plantableRepository.QueryAllPlantable();
            Assert.Equal("T14050302002", a[0].Teacherid);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestQueryAllPlantable2(int i)
        {
            PlantableRepository plantableRepository = new PlantableRepository();
            var a = plantableRepository.QueryAllPlantable();
            Console.WriteLine(a[i].Teacherid);
        }

        [Fact]
        public void TestAddPlan() 
        {
            PlantableRepository plantableRepository = new PlantableRepository();
            DateTime dt = DateTime.Now;
            plantableRepository.Add("T13030201034", "D:/teachplan/plan15", "plan15",dt,0,"");
        }

        [Fact]
        public void TestDel() 
        {
            PlantableRepository plantableRepository = new PlantableRepository();
            plantableRepository.Delete(12);
        }

        [Fact]
        public void TestUpdate()
        {
            PlantableRepository plantableRepository = new PlantableRepository();
            var p = plantableRepository.Query(10);
            Assert.Equal("plan10", p.Filename);
            p.Filename = "plan666";
            plantableRepository.Update(p);
            var pp = plantableRepository.Query(10);
            Assert.Equal("plan666", pp.Filename);
        }
    }
}
