using OALibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OALibrary.Infrastructure
{
    public class PlantableRepository
    {
        /// <summary>
        /// 新建工作计划
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="filepath">文件目录</param>
        /// <param name="filename">文件名称</param>
        /// <param name="committime">提交时间</param>
        /// <param name="state">计划状态</param>
        /// <param name="issue">批复意见</param>
        public void Add(string teacherid, string filepath, string filename, DateTime committime, uint state, string issue)
        {
            using (SchoolOAContext context = new SchoolOAContext()) {
                Entities.Plantable plantable = new Entities.Plantable
                {
                    Teacherid = teacherid,
                    Filepath = filepath,
                    Filename = filename,
                    Committime = committime,
                    State = state,
                    Issue = issue
                };
                context.Plantable.Add(plantable);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 按ID删除工作计划
        /// </summary>
        /// <param name="id">工作计划ID</param>
        public void Delete(int id)
        {
            using (SchoolOAContext context = new SchoolOAContext())
            {
                var plantable = context.Plantable.FirstOrDefault(x => x.Id == id);
                context.Plantable.Remove(plantable);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 通过ID查询工作计划
        /// </summary>
        /// <param name="id">要查询的工作计划ID</param>
        /// <returns></returns>
        public Entities.Plantable Query(int id)
        {
            using (var context = new SchoolOAContext())
            {
                var plantable = context.Plantable.FirstOrDefault(x => x.Id == id);
                context.SaveChanges();
                return plantable;
            } 
        }

        /// <summary>
        /// 获取所有工作计划
        /// </summary>
        /// <returns>工作计划列表</returns>
        public List<Plantable> QueryAllPlantable()
        {
            List<Plantable> plantableList = new List<Plantable>();
            using (var context = new SchoolOAContext())
            {
                plantableList = (from u in context.Plantable
                                    select u
                                ).ToList();
                context.SaveChanges();
            }
            return plantableList;
        }

        /// <summary>
        /// 通过教师ID查询工作计划列表
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <returns>工作计划列表</returns>
        public List<Plantable> QueryPlantablebyTid(string teacherid)
        {
            List<Plantable> plantableList = new List<Plantable>();
            using (var context = new SchoolOAContext())
            {
                plantableList = (from u in context.Plantable
                                 where u.Teacherid.Equals(teacherid)
                                 select u
                                ).ToList();
                context.SaveChanges();
            }
            return plantableList;
        }

        /// <summary>
        /// 通过工作计划状态值查询工作计划列表
        /// </summary>
        /// <param name="state">工作计划状态值</param>
        /// <returns>工作计划列表</returns>
        public List<Plantable> QueryPlantablebyTid(uint state)
        {
            List<Plantable> plantableList = new List<Plantable>();
            using (var context = new SchoolOAContext())
            {
                plantableList = (from u in context.Plantable
                                 where u.State==state
                                 select u
                                ).ToList();
                context.SaveChanges();
            }
            return plantableList;
        }

        /// <summary>
        /// 获取所有未处理工作计划
        /// </summary>
        /// <returns>工作计划列表</returns>
        public List<Plantable> QueryUnprocessedPlantable()
        {
            List<Plantable> plantableList = new List<Plantable>();
            using (var context = new SchoolOAContext())
            {
                plantableList = (from u in context.Plantable
                                    where u.State != 1
                                    select u
                                ).ToList();
                context.SaveChanges();

            }
            return plantableList;
        }
        /// <summary>
        /// 更新指定工作计划
        /// </summary>
        /// <param name="plantable">工作计划对象</param>
        public void Update(Entities.Plantable plantable)
        {
            using (var context = new SchoolOAContext())
            {
                context.Plantable.Update(plantable);
                context.SaveChanges();
            }
        }
    }

}
