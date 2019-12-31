using OALibrary.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OALibrary.Infrastructure
{
    public class WageRepository
    {
        /// <summary>
        /// 新建工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="wagetime"> 工资日期，格式：“2019-08-01”代表2019-08的工资</param>
        /// <param name="basicwage">基本工资</param>
        /// <param name="overtimewage">加班工资</param>
        /// <param name="welfare">福利工资</param>
        /// <param name="bonus">全勤奖</param>
        public void Add(string teacherid, string wagetime, float basicwage, float? overtimewage, float? welfare, float? bonus)
        {
            using (SchoolOAContext context = new SchoolOAContext())
            {
                DateTime dt = Convert.ToDateTime(wagetime);
                Wage wage = new Wage
                {
                    Teacherid = teacherid,
                    Wagetime = dt,
                    Basicwage = basicwage,
                    Overtimewage = overtimewage,
                    Welfare = welfare,
                    Bonus = bonus,
                    Totalwage = basicwage + overtimewage + welfare + bonus
                };
                context.Wage.Add(wage);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 用基本工资创建工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="wagetime">工资日期</param>
        /// <param name="basicwage">基本工资</param>
        public void Add(string teacherid, string wagetime, float basicwage)
        {
            using (SchoolOAContext context = new SchoolOAContext())
            {
                DateTime dt = Convert.ToDateTime(wagetime);
                Wage wage = new Wage
                {
                    Teacherid = teacherid,
                    Wagetime = dt,
                    Basicwage = basicwage,
                    Totalwage = basicwage
                };
                context.Wage.Add(wage);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 依据工资记录的ID删除工资
        /// </summary>
        /// <param name="id">工资记录的ID</param>
        public void Delete(int id) 
        {
            using (SchoolOAContext context = new SchoolOAContext())
            {
                var wage = context.Wage.FirstOrDefault(x => x.Id == id);
                context.Wage.Remove(wage);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 通过工资记录ID查询指定工资记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Wage Query(int id) 
        {
            using (var context = new SchoolOAContext())
            {
                var wage = context.Wage.FirstOrDefault(x => x.Id == id);
                context.SaveChanges();
                return wage;
            }
        }

        /// <summary>
        /// 查询某教师所有工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <returns>指定教师ID的所有工资记录</returns>
        public List<Wage> QueryAllbyTid(string teacherid)
        {
            List<Wage> wageList = new List<Wage>();
            using (var context = new SchoolOAContext())
            {
                wageList = (from u in context.Wage
                            where u.Teacherid.Equals(teacherid)
                            select u
                                ).ToList();
                context.SaveChanges();
            }
            return wageList;
        }

        /// <summary>
        /// 按工资日期查找工资记录
        /// </summary>
        /// <param name="wagetime">工资日期</param>
        /// <returns>指定日期的所有人的工资记录</returns>
        public List<Wage> QueryAllbyTime(string wagetime)
        {
            List<Wage> wageList = new List<Wage>();
            DateTime dt = Convert.ToDateTime(wagetime);
            using (var context = new SchoolOAContext())
            {
                wageList = (from u in context.Wage
                            where 0==DateTime.Compare(u.Wagetime, dt)
                            select u
                                ).ToList();
                context.SaveChanges();
            }
            return wageList;
        }

        /// <summary>
        /// 按教师ID和工资日期精确查询工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="wagetime">工资日期</param>
        /// <returns>指定工资记录</returns>
        public Wage AccurateQuery(string teacherid,string wagetime)
        {
            Wage wage = new Wage();
            DateTime dt = Convert.ToDateTime(wagetime);
            using (var context = new SchoolOAContext())
            {
                wage = (from u in context.Wage
                        where (0 == DateTime.Compare(u.Wagetime, dt))&&(u.Teacherid.Equals(teacherid))
                        select u
                            ).FirstOrDefault();
                context.SaveChanges();
            }
            return wage;
        }

        /// <summary>
        /// 更新指定工资记录对象
        /// </summary>
        /// <param name="wage">工资记录对象</param>
        public void Update(Wage wage) 
        {
            using (var context = new SchoolOAContext())
            {
                context.Wage.Update(wage);
                context.SaveChanges();
            }
        }
    }
}
