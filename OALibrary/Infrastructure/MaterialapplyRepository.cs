using OALibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OALibrary.Infrastructure
{
    public class MaterialapplyRepository
    {
        /// <summary>
        /// 创建物资申请
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="teacherid">教师ID</param>
        /// <param name="goodsname">物资名称</param>
        /// <param name="goodsnum">物资数量</param>
        /// <param name="goodsprice">物资价格</param>
        /// <param name="remarks">申请原因</param>
        public void Add(string departmentid, string teacherid, string goodsname, int goodsnum, float goodsprice, string remarks)
        {
            using (SchoolOAContext context = new SchoolOAContext())
            {
                Materialapply materialapply = new Materialapply
                {
                    Departmentid = departmentid,
                    Tid = teacherid,
                    Goodsname = goodsname,
                    Goodsnum = goodsnum,
                    Goodsprice = goodsprice,
                    Remarks = remarks,
                    State = 0  //新建物资申请状态默认未处理；0-未处理1-同意2-拒绝
                };
                context.Materialapply.Add(materialapply);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 通过物资申请记录ID删除指定物资申请记录
        /// </summary>
        /// <param name="id">物资申请记录ID</param>
        public void Delete(int id)
        {
            using (SchoolOAContext context = new SchoolOAContext())
            {
                var materialapply = context.Materialapply.FirstOrDefault(x => x.Id == id);
                context.Materialapply.Remove(materialapply);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// 通过物资申请记录ID查询指定物资申请记录
        /// </summary>
        /// <param name="id">物资申请记录ID</param>
        /// <returns>指定物资申请记录对象</returns>
        public Entities.Materialapply Query(int id)
        {
            using (var context = new SchoolOAContext())
            {
                var materialapply = context.Materialapply.FirstOrDefault(x => x.Id == id);
                context.SaveChanges();
                return materialapply;
            }
        }

        /// <summary>
        /// 获取所有物资申请
        /// </summary>
        /// <returns>所有物资申请</returns>
        public List<Materialapply> QueryAllMaterialapply()
        {
            List<Materialapply> materialapplyList = new List<Materialapply>();
            using (var context = new SchoolOAContext())
            {
                materialapplyList = (from u in context.Materialapply
                                 select u
                                ).ToList();
                context.SaveChanges();
            }
            return materialapplyList;
        }

        /// <summary>
        /// 通过部门ID查询物资申请记录列表
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <returns>物资申请记录列表</returns>
        public List<Materialapply> QueryMaterialapplybyDid(string departmentid) 
        {
            List<Materialapply> materialapplyList = new List<Materialapply>();
            using (var context = new SchoolOAContext())
            {
                materialapplyList = (from u in context.Materialapply
                                where u.Departmentid.Equals(departmentid)
                                 select u
                                ).ToList();
                context.SaveChanges();
            }
            return materialapplyList;
        }

        /// <summary>
        /// 通过物资申请状态查询物资申请记录列表
        /// </summary>
        /// <param name="state">物资申请状态</param>
        /// <returns>物资申请记录列表</returns>
        public List<Materialapply> QueryMaterialapplybyState(int state)
        {
            List<Materialapply> materialapplyList = new List<Materialapply>();
            using (var context = new SchoolOAContext())
            {
                materialapplyList = (from u in context.Materialapply
                                     where u.State == state
                                     select u
                                ).ToList();
                context.SaveChanges();
            }
            return materialapplyList;
        }

        /// <summary>
        /// 通过部门ID和物资申请状态查询物资申请记录
        /// </summary>
        /// <param name="departmentid">部门ID</param>
        /// <param name="state">物资申请状态</param>
        /// <returns>物资申请记录列表</returns>
        public List<Materialapply> QueryMaterialapplybyDS(string departmentid,int state)
        {
            List<Materialapply> materialapplyList = new List<Materialapply>();
            using (var context = new SchoolOAContext())
            {
                materialapplyList = (from u in context.Materialapply
                                     where u.State == state && u.Departmentid.Equals(departmentid)
                                     select u
                                ).ToList();
                context.SaveChanges();
            }
            return materialapplyList;
        }

        /// <summary>
        /// 更新指定物资申请记录
        /// </summary>
        /// <param name="materialapply">指定物资申请记录</param>
        public void Update(Materialapply materialapply)
        {
            using (var context = new SchoolOAContext())
            {
                context.Materialapply.Update(materialapply);
                context.SaveChanges();
            }
        }
    }
}
