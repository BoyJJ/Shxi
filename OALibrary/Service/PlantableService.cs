using OALibrary.Entities;
using OALibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALibrary.Service
{
    public class PlantableService
    {
        readonly PlantableRepository plantableRepository = new PlantableRepository();

        /// <summary>
        /// 新建工作计划
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="filepath">文件目录</param>
        /// <param name="filename">文件名称</param>
        /// <param name="state">计划状态</param>
        /// <param name="issue">批复意见</param>
        public void AddWorkPlan(string teacherid, string filepath, string filename, uint state, string issue)
        {
            
            DateTime dt = DateTime.Now;//使用 DateTime 类获取当前时间
            plantableRepository.Add(teacherid,filepath,filename,dt,state,issue);
        }

        /// <summary>
        /// 按ID删除指定工作计划
        /// </summary>
        /// <param name="id">工作计划ID</param>
        public void DeleteWorkPlan(int id)
        {
            plantableRepository.Delete(id);
        }

        /// <summary>
        /// 按组合条件查询工作计划
        /// 当teacherid为空时，只安装state作为条件筛选；当state=5时不依据state筛选；当teacherid为空&state=6时，返回所有计划
        /// </summary>
        /// <param name="teacherid">教师ID</param>
        /// <param name="state">工作计划状态 0：等待审核，1：已通过，2：等待修正，3：已修正，4：已驳回 5：不依据state筛选</param>
        /// <returns>查询到的工作计划列表</returns>
        public List<Plantable> QueryWorkPlan(string teacherid,uint state) 
        {
            var plantableList = plantableRepository.QueryAllPlantable(); //获取所有工作计划
            if (state == 5)//state = 5 ：不依据state筛选，是否继续筛选要看teacherID
            {
                if (string.IsNullOrEmpty(teacherid)) //教师ID为空不再筛选计划，全部返回
                {
                    return plantableRepository.QueryAllPlantable();
                }
                else //教师ID非空还有再筛选计划，之后返回
                {
                    return plantableRepository.QueryPlantablebyTid(teacherid);
                }
            }
            if (string.IsNullOrEmpty(teacherid))//若state！=5且教师ID为空，则按state筛选计划
            {
                var plantableListLocal = new List<Plantable>();//计划列表局部变量
                foreach (var plantableelm in plantableList)
                {
                    if (plantableelm.State.Equals(state)) plantableListLocal.Add(plantableelm);
                }
                return plantableListLocal;
            }
            else //若state！=5且教师ID非空，则按state和教师ID共同筛选计划
            {
                var plantableListLocal = new List<Plantable>();//计划列表局部变量
                foreach (var plantableelm in plantableList)
                {
                    if (plantableelm.State==state&&plantableelm.Teacherid.Equals(teacherid)) plantableListLocal.Add(plantableelm);
                }
                return plantableListLocal;
            }
        }

        /// <summary>
        /// 批复工作计划
        /// </summary>
        /// <param name="id">工作计划ID</param>
        /// <param name="issue">批复意见（非必填）</param>
        /// <param name="state">工作计划状态 0：等待审核，1：已通过，2：等待修正，3：已修正，4：已驳回</param>
        public void ReplyWorkPlan(int id,string issue,uint state)
        {
            Plantable plantable = plantableRepository.Query(id);
            plantable.State = state;
            if (!string.IsNullOrEmpty(issue))  //issue非必填，有内容再修改
            {
                plantable.Issue = issue;
            }
            plantableRepository.Update(plantable);
        }
    }
}
