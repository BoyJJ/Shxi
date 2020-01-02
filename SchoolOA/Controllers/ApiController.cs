using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OALibrary.Service;
using SchoolOA.Models;

namespace SchoolOA.Controllers
{
    public class ApiController : Controller
    {
        /// <summary>
        /// 组合条件查询工作计划
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="state">工作计划状态　当state＝５时，不按state筛选工作计划</param>
        /// <returns>查询到的工作计划转Json</returns>
        public object QueryWorkPlan(string teacherid,uint state) 
        {
            PlantableService plantableService = new PlantableService();
            var planList = plantableService.QueryWorkPlan(teacherid, state);
            return Json(new
            {
                code = 200,
                msg = "WorkPlan query successfully!",
                count = (int)planList.ToArray().Length,
                planList = planList
            });
        }

        /// <summary>
        /// 批复工资计划
        /// </summary>
        /// <param name="id">工作计划表中记录的ID</param>
        /// <param name="state">工作计划的状态　当state＝５时，不按state筛选工作计划</param>
        /// <param name="issue">处理意见，有批复人员填写</param>
        /// <returns>批复成功后，返回所有计划列表</returns>
        public object ReplyWorkPlan(int id,uint state,string issue) 
        {
            PlantableService plantableService = new PlantableService();
            plantableService.ReplyWorkPlan(id, issue, state);
            var planList = plantableService.QueryWorkPlan("",5); //批复成功返回所有计划列表
            return Json(new
            {
                code = 200,
                msg = "Plan status changed successfully!",
                planList = planList
            });
        }

        /// <summary>
        /// 通过组合条件查询物资申请记录
        /// </summary>
        /// <param name="state">物资申请记录状态　当state＝３时，不按照state筛选物资申请记录</param>
        /// <param name="departmentid">部门ID</param>
        /// <returns>查询到的物资申请记录</returns>
        public object QueryMaterialApplication(int state,string departmentid) 
        {
            MaterialapplyService materialapplyService = new MaterialapplyService();
            var materialapplyList = materialapplyService.Query(departmentid, state);
            return Json(new
            {
                code = 200,
                msg = "Material application query successfully!",
                materialapplyList = materialapplyList
            });
        }

        /// <summary>
        /// 通过物资申请表的ID处理指定物资申请记录的状态
        /// </summary>
        /// <param name="id">物资申请表的ID</param>
        /// <param name="state">物资申请状态　当state＝３时，不按照state筛选物资申请记录</param>
        /// <returns></returns>
        public object HandleMaterialApplication(int id,int state) 
        {
            MaterialapplyService materialapplyService = new MaterialapplyService();
            materialapplyService.Update(id,state);
            var materialapplyList = materialapplyService.Query("", 3);
            return Json(new
            {
                code = 200,
                msg = "Handle application query successfully!",
                materialapplyList = materialapplyList
            });
        }

        /// <summary>
        /// 查询教师ID的某年或某月工资记录
        /// 每次请求teacherid必填，year 和time二选一
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="year">工资日期1：要查询的年份eg:2019</param>
        /// <param name="time">工资日期2：要查询的年月eg:2019-01-01、2019-12-01</param>
        /// <returns>查询到的工资记录</returns>
        public object QueryTeacherWage(string teacherid,string year,string time) 
        {
            WageService wageService = new WageService();
            var wageList = wageService.QueryTeacherWage(teacherid,year,time);
            return Json(new
            {
                code = 200,
                msg = "Wage query successfully!",
                wageList = wageList
            });
        }

        /// <summary>
        /// 查询部门（ID）内所有人的某月工资
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="time">要查询的年月eg:2019-01-01、2019-12-01</param>
        /// <returns>工资记录列表</returns>
        public object QueryDepartmentWage(string departmentid, string time)
        {
            WageService wageService = new WageService();
            var wageList = wageService.QueryDepartmentWage(departmentid, time);
            return Json(new
            {
                code = 200,
                msg = "Wage query successfully!",
                wageList = wageList
            });
        }

        ///// <summary>
        ///// 获取当月所有人工资记录
        ///// </summary>
        ///// <returns>当月所有人工资记录</returns>
        //public object ThisMonthWage()
        //{
        //    WageService wageService = new WageService();
        //    var wageList = wageService.QueryWage("", DateTime.Now.ToString("yyyy-MM-01"));//使用 DateTime 类获取当前时间
        //    return Json(new
        //    {
        //        code = 200,
        //        msg = "Wage query successfully!",
        //        wageList = wageList
        //    });
        //}

        ///// <summary>
        ///// 查询某年所有人工资
        ///// </summary>
        ///// <param name="year">年份</param>
        ///// <returns>工资记录列表</returns>
        //public object ThisYearWage(string year) 
        //{
        //    WageService wageService = new WageService();
        //    var wageList = wageService.QueryWagebyYear(year);
        //    return Json(new
        //    {
        //        code = 200,
        //        msg = year + " Wage query successfully!",
        //        wageList = wageList
        //    });
        //}

        /// <summary>
        /// 通过工资记录的表中ID修改工资信息
        /// 所有工资都必填，没有为0
        /// </summary>
        /// <param name="id">工资记录表ID</param>
        /// <param name="basicwage">基本工资</param>
        /// <param name="overtimewage">加班工资</param>
        /// <param name="welfare">福利工资</param>
        /// <param name="bonus">全勤奖</param>
        /// <returns>修改工资记录后，返回当月所有人工资记录</returns>
        public object UpdateWage(int id, float basicwage, float overtimewage, float welfare, float bonus) 
        {
            WageService wageService = new WageService();
            wageService.UpdateWage(id, basicwage, overtimewage, welfare, bonus);
            return Json(new
            {
                code = 200,
                msg = "Wage update/set successfully!",
            });
        }

        /// <summary>
        /// 新建人员指定时间工资
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="wagetime"> 工资日期，格式：“2019-08-01”代表2019-08的工资</param>
        /// <param name="basicwage">基本工资</param>
        /// <param name="overtimewage">加班工资</param>
        /// <param name="welfare">福利工资</param>
        /// <param name="bonus">全勤奖</param>
        /// <returns>添加工资记录后，返回当月所有人工资记录</returns>
        public object NewWage(string teacherid, string wagetime, float basicwage, float overtimewage, float welfare, float bonus) 
        {
            WageService wageService = new WageService();
            wageService.AddWage(teacherid, wagetime, basicwage, overtimewage, welfare, bonus);
            return Json(new
            {
                code = 200,
                msg = "Add Wage successfully!",
            });
        }

    }
}
