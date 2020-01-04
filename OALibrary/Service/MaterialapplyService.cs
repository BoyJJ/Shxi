using OALibrary.Entities;
using OALibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace OALibrary.Service
{
    public class MaterialapplyService
    {
        readonly MaterialapplyRepository materialapplyRepository = new MaterialapplyRepository();

        /// <summary>
        /// 新建物资申请
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="teacherid">教师ID</param>
        /// <param name="goodsname">物资名称</param>
        /// <param name="goodsnum">物资数量</param>
        /// <param name="goodsprice">物资价格</param>
        /// <param name="remarks">申请原因</param>
        public void AddWage(string departmentid, string teacherid, string goodsname, int goodsnum, float goodsprice, string remarks)
        {
            materialapplyRepository.Add(departmentid, teacherid, goodsname, goodsnum, goodsprice, remarks);
        }

        /// <summary>
        /// 通过工资记录ID删除指定工资记录
        /// </summary>
        /// <param name="id">工资记录ID</param>
        public void DeleteWage(int id) 
        {
            if(!string.IsNullOrEmpty(materialapplyRepository.Query(id).Departmentid))materialapplyRepository.Delete(id); // 存在才能删除
        }

        /// <summary>
        /// 通过组合条件查询物资申请记录列表
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="state">物资申请状态</param>
        /// <returns>物资申请记录列表</returns>
        public List<Materialapply> QuerybyDS(string departmentid,int state) 
        {
            if (state == 3) //物资申请记录的状态不作为对记录筛选条件
            {
                if (string.IsNullOrEmpty(departmentid)) //部门ID为空
                {
                    return materialapplyRepository.QueryAllMaterialapply();
                }
                return materialapplyRepository.QueryMaterialapplybyDid(departmentid);
            }
            if (string.IsNullOrEmpty(departmentid))//部门ID为空
            {
                return materialapplyRepository.QueryMaterialapplybyState(state);
            }
            else //部门ID非空，物资申请记录的状态不作为对记录筛选条件
            {
                return materialapplyRepository.QueryMaterialapplybyDS(departmentid,state);
            }
        }

        /// <summary>
        /// 根据记录ID查询指定记录
        /// </summary>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public Materialapply QuerybyID(int id) 
        {
            return materialapplyRepository.Query(id);
        }

        public void Update(int id,int state)   //EF 生成的这个State是int型
        {
            var materialapply = materialapplyRepository.Query(id);
            if (materialapply.Id==id) //如果记录存在再修改
            {
                materialapply.State = state;
                materialapplyRepository.Update(materialapply);
            }

        }

    }
}
