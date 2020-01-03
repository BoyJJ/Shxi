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
        public Wage QuerybyID(int id) 
        {
            using (var context = new SchoolOAContext())
            {
                var wage = context.Wage.FirstOrDefault(x => x.Id == id);
                context.SaveChanges();
                //wage.Wagetime = wage.Wagetime.ToString("yyyy-MM");
                return wage;
            }
        }

        /// <summary>
        /// 查询某教师某年所有工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="year">年份</param>
        /// <returns>指定教师ID的某年的所有工资记录</returns>
        public List<Wage> QueryAllbyTidYear(string teacherid,string year)
        {
            List<Wage> wageList = new List<Wage>();
            List<Wagestringtime> wageListstring = new List<Wagestringtime>();
            string Jan = "-01-01";
            string Dec = "-12-01";
            string Begin = year + Jan;
            string End = year + Dec;
            DateTime BeginTime = Convert.ToDateTime(Begin);
            DateTime EndTime = Convert.ToDateTime(End);
            using (var context = new SchoolOAContext())
            {
                wageList = (from u in context.Wage
                            where u.Teacherid.Equals(teacherid) && DateTime.Compare(u.Wagetime, BeginTime) >= 0 && DateTime.Compare(u.Wagetime, EndTime) <= 0
                            select u
                                ).ToList();
                context.SaveChanges();
            }
            return wageList;
        }

        /// <summary>
        /// 查询教师ID的某月的工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="time">工资日期（月份输入举例 eg：2019-01-01表示查询2019年1月工资）</param>
        /// <returns>指定工资记录</returns>
        public Wage QuerybyTidMonth(string teacherid, string time)
        {
            Wage wage = new Wage();
            DateTime dt = Convert.ToDateTime(time);
            using (var context = new SchoolOAContext())
            {
                wage = (from u in context.Wage
                        where (0 == DateTime.Compare(u.Wagetime, dt)) && (u.Teacherid.Equals(teacherid))
                        select u
                            ).FirstOrDefault();
                context.SaveChanges();
            }
            return wage;
        }

        ///// <summary>
        ///// 按工资日期（某个月）查找工资记录
        ///// </summary>
        ///// <param name="wagetime">工资日期</param>
        ///// <returns>指定日期（某个月）的所有人的工资记录</returns>
        //public List<Wage> QueryAllbyTime(string wagetime)
        //{
        //    List<Wage> wageList = new List<Wage>();
        //    DateTime dt = Convert.ToDateTime(wagetime);
        //    using (var context = new SchoolOAContext())
        //    {
        //        wageList = (from u in context.Wage
        //                    where 0==DateTime.Compare(u.Wagetime, dt)
        //                    select u
        //                        ).ToList();
        //        context.SaveChanges();
        //    }
        //    return wageList;
        //}

        ///// <summary>
        ///// 查询某一年所有人的工资
        ///// </summary>
        ///// <param name="year">年份</param>
        ///// <returns>指定年份工资记录列表</returns>
        //public List<Wage> QueryAllbyYear(string year)
        //{
        //    List<Wage> wageList = new List<Wage>();
        //    string Jan = "-01-01";
        //    string Dec = "-12-01";
        //    string Begin = year + Jan;
        //    string End = year + Dec;

        //    DateTime BeginTime = Convert.ToDateTime(Begin);
        //    DateTime EndTime = Convert.ToDateTime(End);
        //    using (var context = new SchoolOAContext())
        //    {
        //        wageList = (from u in context.Wage
        //                    where DateTime.Compare(u.Wagetime, BeginTime) >=0 && DateTime.Compare(u.Wagetime, EndTime) <= 0
        //                    select u
        //                        ).ToList();
        //        context.SaveChanges();
        //    }
        //    return wageList;
        //}

        /// <summary>
        /// 查询某部门（ID）内的所有人某月工资
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="time">工资日期（月份输入举例 eg：2019-01-01表示查询2019年1月工资）</param>
        /// <returns>工资记录列表</returns>
        public List<Wage> QueryAllbyDidMonth(string departmentid, string time)
        {
            List<Wage> wageList = new List<Wage>();
            DateTime Time = Convert.ToDateTime(time);
            using (var context = new SchoolOAContext())
            {
                wageList = (from u in context.Wage
                            from v in context.Teacherinfomation
                            where u.Teacherid.Equals(v.Teacherid) &&v.Departmentid.Equals(departmentid) && DateTime.Compare(u.Wagetime, Time) == 0
                            select u
                                ).ToList();
                context.SaveChanges();
            }
            return wageList;
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

        public List<Wagestringtime> FixTime(List<Wage> wageList) 
        {
            List<Wagestringtime> wagestringtimeList = new List<Wagestringtime>();
            foreach (var u in wageList) 
            {
                wagestringtimeList.Add(new Wagestringtime { Id = u.Id, Teacherid = u.Teacherid, Wagetime = u.Wagetime.ToString("yyyy-MM"), Basicwage = u.Basicwage, Overtimewage = u.Overtimewage, Welfare = u.Welfare, Bonus = u.Bonus, Totalwage = u.Totalwage });
            }
            return wagestringtimeList;
        }
    }

    public class Wagestringtime
    {
        public int Id { get; set; }
        public string Teacherid { get; set; }
        public string Wagetime { get; set; }
        public float Basicwage { get; set; }
        public float? Overtimewage { get; set; }
        public float? Welfare { get; set; }
        public float? Bonus { get; set; }
        public float? Totalwage { get; set; }
    }
}
