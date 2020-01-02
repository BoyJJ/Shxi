using OALibrary.Entities;
using OALibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALibrary.Service
{
    public class WageService
    {
        readonly WageRepository wageRepository = new WageRepository();

        /// <summary>
        /// 为一个教师ID新建一个工资记录
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="wagetime"> 工资日期，格式：“2019-08-01”代表2019-08的工资</param>
        /// <param name="basicwage">基本工资</param>
        /// <param name="overtimewage">加班工资</param>
        /// <param name="welfare">福利工资</param>
        /// <param name="bonus">全勤奖</param>
        public void AddWage(string teacherid,string wagetime,float basicwage,float overtimewage,float welfare,float bonus ) 
        {
            wageRepository.Add(teacherid,wagetime,basicwage,overtimewage,welfare,bonus);
        }

        /// <summary>
        /// 通过工资记录ID删除指定工资记录
        /// </summary>
        /// <param name="id">工资记录ID</param>
        public void DeleteWage(int id) 
        {
            wageRepository.Delete(id);
        }

        /// <summary>
        /// 组合条件查询工资记录
        /// </summary>
        /// <param name="teacherid">教师ID（选填，为空查询选择月份所有人工资）</param>
        /// <param name="wagetime">工资日期（必填）</param>
        /// <returns></returns>
        public List<Wage> QueryWage(string teacherid,string wagetime) 
        {
            if (string.IsNullOrEmpty(teacherid)) 
            {
                return wageRepository.QueryAllbyTime(wagetime);
            }
            return new List<Wage> 
            {
                wageRepository.AccurateQuery(teacherid, wagetime) 
                };
        }

        /// <summary>
        /// 查询某一年所有人的工资
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>指定年份工资记录列表</returns>
        public List<Wage> QueryWagebyYear(string year)
        {
            return wageRepository.QueryAllbyYear(year);
        }

        /// <summary>
        /// 通过工资记录ID更新工资记录
        /// </summary>
        /// <param name="id">工资记录</param>
        /// <param name="basicwage">基本工资</param>
        /// <param name="overtimewage">加班工资</param>
        /// <param name="welfare">福利工资</param>
        /// <param name="bonus">全勤奖</param>
        public void UpdateWage(int id,float basicwage,float overtimewage,float welfare,float bonus) 
        {
            var wage = wageRepository.Query(id);
            wage.Basicwage = basicwage;
            wage.Overtimewage = overtimewage;
            wage.Welfare = welfare;
            wage.Bonus = bonus;
            wageRepository.Update(wage);
        }

    }
}
