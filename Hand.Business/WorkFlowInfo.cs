using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Hand.Model;
using Hand.Enum;

namespace Hand.Business
{
    public class WorkFlowInfo
    {
        public static testFrameEntities DbEntities = new testFrameEntities();

        /// <summary>
        /// 朱乾方
        /// 20170829
        /// 获取当前登陆人的工作流信息
        /// </summary>
        /// <returns></returns>
        public List<WorkFlowViewMoel> GetWorkFlow(int? empNo)
        {
            List<WorkFlowViewMoel> workFlow;
            var isLeader = (from dept in DbEntities.Department.ToList()
                            where dept.dept_leaderNo == empNo
                            select dept).ToList();
            if (isLeader.Count > 0)
            {
                workFlow = (from work in DbEntities.WorkFlow.ToList()
                            join dept in DbEntities.Department.ToList() on work.Work_DeptId equals dept.dept_id
                            where work.Work_ApproverNo == empNo
                            select new WorkFlowViewMoel
                            {
                                WorkTitle = work.Work_Title,
                                WorkContent = work.Work_Content,
                                WorkCreateTime = Convert.ToDateTime(work.Work_CreateTime).ToString("yyyy-MM-dd"),
                                WorkStatus = EnumHelper.GetDescription((WorkStatusEnum)work.Work_Status.GetHashCode()),
                                ArtificialNumbers = work.Work_EmpNo
                            }).ToList();
            }
            else
            {
                workFlow = (from work in DbEntities.WorkFlow.ToList()
                            join dept in DbEntities.Department.ToList() on work.Work_DeptId equals dept.dept_id
                            where work.Work_EmpNo == empNo
                            select new WorkFlowViewMoel
                            {
                                WorkTitle = work.Work_Title,
                                WorkContent = work.Work_Content,
                                WorkCreateTime = Convert.ToDateTime(work.Work_CreateTime).ToString("yyyy-MM-dd"),
                                WorkStatus = EnumHelper.GetDescription((WorkStatusEnum)work.Work_Status.GetHashCode()),
                                DeptLeader = dept.dept_leader
                            }).ToList();
            }

            return workFlow;
        }

        /// <summary>
        /// 朱乾方
        /// 20170829
        /// 添加工作流信息
        /// </summary>
        /// <param name="workFlow"></param>
        /// <returns></returns>
        public string AddWorkFlow(WorkFlow workFlow)
        {
            var strMsg = new StringBuilder();
            try
            {
                var approverNo = (from w in DbEntities.WorkFlow.ToList()
                                  join dept in DbEntities.Department.ToList() on w.Work_DeptId equals dept.dept_id
                                  where dept.dept_id == workFlow.Work_DeptId
                                  select dept.dept_leaderNo
                                  ).FirstOrDefault();
                var work = new WorkFlow
                {
                    Work_EmpNo = workFlow.Work_EmpNo,
                    Work_Title = workFlow.Work_Title,
                    Work_Content = workFlow.Work_Content,
                    Work_CreateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                    Work_DeptId = workFlow.Work_DeptId,
                    Work_Status = WorkStatusEnum.Approval.GetHashCode(),
                    Work_ApproverNo = approverNo
                };
                DbEntities.WorkFlow.Add(work);
                DbEntities.SaveChanges();
                strMsg.Append("添加成功");
            }
            catch (Exception ex)
            {
                strMsg.AppendFormat("添加失败：{0}", ex.Message);
            }
            return strMsg.ToString();
        }

        /// <summary>
        ///  朱乾方
        ///  20170829
        ///  根据工作流Id查询
        /// </summary>
        /// <param name="workId">工作流Id</param>
        /// <returns></returns>
        public WorkFlow GetWorkFlowById(int? workId)
        {
            var workFlow = DbEntities.WorkFlow.FirstOrDefault(w => w.Work_Id == workId);
            return workFlow;
        }

        /// <summary>
        ///  朱乾方
        ///  20170829
        ///  根据工作流Id编辑工作流信息
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="workFlow"></param>
        /// <returns></returns>
        public string EditWorkFlow(int? workId, WorkFlow workFlow)
        {
            var strMsg = new StringBuilder();
            try
            {
                if (workFlow != null)
                {
                    var work = DbEntities.WorkFlow.FirstOrDefault(w => w.Work_Id == workId);
                    if (work != null)
                    {
                        work.Work_Title = workFlow.Work_Title;
                        work.Work_Content = workFlow.Work_Content;
                        work.Work_Status = workFlow.Work_Status;
                        DbEntities.Entry(work).State = EntityState.Modified;
                        DbEntities.SaveChanges();
                    }
                }
                strMsg.Append("修改成功");
            }
            catch (Exception ex)
            {
                strMsg.AppendFormat("修改失败:{0}", ex.Message);
            }
            return strMsg.ToString();

        }
    }
}
