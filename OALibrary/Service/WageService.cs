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
        public void AddWage(string teacherid, string wagetime, float basicwage, float overtimewage, float welfare, float bonus)
        {
            wageRepository.Add(teacherid, wagetime, basicwage, overtimewage, welfare, bonus);
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
        /// 通过数据库记录ID查询工资记录
        /// </summary>
        /// <param name="id">数据库记录ID</param>
        /// <returns></returns>
        public Wage QuerybyID(int id) 
        {
            return wageRepository.QuerybyID(id);
        }

        /// <summary>
        /// 查询教师ID的工资记录（按年、按月）年、年月二选一,不可都传参
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="year">年份（eg:2019--string类型）</param>
        /// <param name="yearmonth">年月（eg:2019-01-01表示2019年1月工资）</param>
        /// <returns></returns>
        public List<Wage> QueryTeacherWage(string teacherid, string year, string yearmonth)
        {
            if (string.IsNullOrEmpty(year)) //year为空，按年月查询某一月工资
            {
                return new List<Wage> { wageRepository.QuerybyTidMonth(teacherid, yearmonth) };//类型转化 ：Wage --> List<Wage>
            }
            return wageRepository.QueryAllbyTidYear(teacherid,year);
        }

        /// <summary>
        /// 查询某部门（ID）内的所有人某月工资
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="time">工资日期（月份输入举例 eg：2019-01-01表示查询2019年1月工资）</param>
        /// <returns>工资记录列表</returns>
        public List<Wage> QueryDepartmentWage(string departmentid,string time)
        {
            return wageRepository.QueryAllbyDidMonth(departmentid, time);
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
            var wage = wageRepository.QuerybyID(id);
            wage.Basicwage = basicwage;
            wage.Overtimewage = overtimewage;
            wage.Welfare = welfare;
            wage.Bonus = bonus;
            wage.Totalwage = basicwage + overtimewage + welfare + bonus;
            wageRepository.Update(wage);
        }

    }
}
