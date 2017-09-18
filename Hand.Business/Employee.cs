using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hand.Model;
using Hand.Enum;

namespace Hand.Business
{
    /// <summary>
    /// 人员信息
    /// </summary>
    public class Employee
    {

        public testFrameEntities DbEntities;

        public Employee()
        {
            DbEntities = new testFrameEntities();
        }

        /// <summary>
        /// 朱乾方
        /// 2017-08-23
        /// 根据用户名和密码查询员工
        /// </summary>
        public bool QueryEmployee(int empNo, string pwd)
        {
            using (DbEntities)
            {
                var test = from a in DbEntities.employee
                           where a.emp_No == empNo
                           select a;
                foreach (employee value in test)
                {
                    if (value.emp_pwd == pwd)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 朱乾方
        /// 20170824
        /// 获取所有员工信息
        /// </summary>
        /// <returns></returns>
        public List<EmployeeInfo> GetEmp()
        {
            var empInfo = (from emp in DbEntities.employee.ToList()
                           join dept in DbEntities.Department.ToList() on emp.emp_dept_id equals dept.dept_id
                           join role in DbEntities.Role.ToList() on emp.emp_role_id equals role.role_id
                           select new EmployeeInfo
                           {
                               EmpId = emp.emp_id,
                               EmpNo = emp.emp_No.ToString("00000"),
                               EmpName = emp.emp_name,
                               EmpMobile = emp.emp_mobile,
                               EmpEmail = emp.emp_email,
                               EmpRoleName = role.role_name,
                               EmpIsValid = EnumHelper.GetDescription((CommonEnum)emp.emp_isvalid.GetHashCode()),
                               EmpWorkAddress = emp.emp_workaddress,
                               EmpJoinTime = Convert.ToDateTime(emp.emp_jointime).ToString("yyyy-MM-dd"),
                               EmpDeptName = dept.dept_name
                           }).ToList();
            return empInfo;
        }

        /// <summary>
        /// 朱乾方
        /// 20170824
        /// 添加员工
        /// </summary>
        /// <returns></returns>
        public string AddEmp(employee emp)
        {
            var strMsg = new StringBuilder();
            try
            {
                if (emp != null)
                {
                    employee employee = new employee
                    {
                        emp_No = AutoGengerEmpNo() + 1,
                        emp_name = emp.emp_name,
                        emp_email = emp.emp_email,
                        emp_mobile = emp.emp_mobile,
                        emp_role_id = emp.emp_role_id,
                        emp_workaddress = emp.emp_workaddress,
                        emp_jointime = DateTime.Now,
                        emp_dept_id = emp.emp_dept_id,
                        emp_isvalid = CommonEnum.Job.GetHashCode()
                    };
                    DbEntities.employee.Add(employee);
                    DbEntities.SaveChanges();
                    strMsg.Append("添加成功");
                }
            }
            catch (Exception ex)
            {
                strMsg.AppendFormat("添加失败:{0}", ex);
            }
            return strMsg.ToString();
        }

        /// <summary>
        /// 20170828
        /// 朱乾方
        /// 根据用户Id获取员工信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public EmployeeInfo GetEmployeeByNo(int? id)
        {
            var empInfo = (from emp in DbEntities.employee.ToList()
                           join dept in DbEntities.Department.ToList() on emp.emp_dept_id equals dept.dept_id
                           join role in DbEntities.Role.ToList() on emp.emp_role_id equals role.role_id
                           where emp.emp_id == id
                           select new EmployeeInfo
                           {
                               EmpId = emp.emp_id,
                               EmpNo = emp.emp_No.ToString("00000"),
                               EmpName = emp.emp_name,
                               EmpMobile = emp.emp_mobile,
                               EmpEmail = emp.emp_email,
                               EmpIsValid = EnumHelper.GetDescription((CommonEnum)emp.emp_isvalid.GetHashCode()),
                               EmpWorkAddress = emp.emp_workaddress,
                               EmpJoinTime = Convert.ToDateTime(emp.emp_jointime).ToString("yyyy-MM-dd"),
                               EmpRoleName = role.role_name,
                               EmpDeptName = dept.dept_name
                           }).FirstOrDefault();
            return empInfo;
        }

        /// <summary>
        /// 朱乾方
        /// 20170824
        /// 编辑
        /// </summary>
        /// <param name="empId">用户Id</param>
        /// <param name="employee">用户信息实体</param>
        /// <returns></returns>
        public string EditEmp(int? empId, employee employee)
        {
            var strMsg = new StringBuilder();
            try
            {
                if (empId > 0)
                {
                    if (employee != null)
                    {
                        employee emp = DbEntities.employee.FirstOrDefault(e => e.emp_id == empId); //先查找出要修改的对象
                        if (emp != null)
                        {
                            emp.emp_No = employee.emp_No;
                            emp.emp_name = employee.emp_name;
                            emp.emp_email = employee.emp_email;
                            emp.emp_mobile = employee.emp_mobile;
                            emp.emp_role_id = employee.emp_role_id;
                            emp.emp_workaddress = employee.emp_workaddress;
                            emp.emp_dept_id = employee.emp_dept_id;
                            DbEntities.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                            DbEntities.SaveChanges();
                            strMsg.Append("修改成功");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                strMsg.AppendFormat("修改失败:{0}", ex);
            }
            return strMsg.ToString();

        }

        /// <summary>
        /// 朱乾方
        /// 20170824
        /// 根据用户Id将人员无效
        /// </summary>
        /// <param name="empId">用户Id</param>
        /// <returns></returns>
        public string DeleteEmp(int? empId)
        {
            var strMsg = new StringBuilder();
            try
            {
                if (empId > 0)
                {
                    employee emp = DbEntities.employee.FirstOrDefault(e => e.emp_id == empId);//先查找出要修改的对象
                    if (emp != null)
                    {
                        emp.emp_isvalid = 0;
                        DbEntities.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                        DbEntities.SaveChanges();
                        strMsg.Append("删除成功");
                    }
                }
            }
            catch (Exception ex)
            {
                strMsg.AppendFormat("删除失败：{0}", ex);
            }

            return strMsg.ToString();
        }

        /// <summary>
        /// 朱乾方
        /// 20170901
        /// 自动生成员工工号
        /// </summary>
        /// <returns></returns>
        public int AutoGengerEmpNo()
        {
            var empNo = (from emp in DbEntities.employee
                         group emp by emp.emp_id
                into c
                         select c.Max(e => e.emp_No)).FirstOrDefault();
            return empNo;
        }

        /// <summary>
        /// 朱乾方
        /// 20170901
        /// 注册账号(md5加密)
        /// </summary>
        /// <param name="empNo">员工工号</param>
        /// <param name="pwd">用户密码</param>
        /// <returns></returns>
        public string RegisteredAccount(int? empNo, string pwd)
        {
            var strMsg = new StringBuilder();
            try
            {
                var empInfo = (from emp in DbEntities.employee
                               where emp.emp_No == empNo
                               select emp).FirstOrDefault();
                if (empInfo != null)
                {
                    #region md5加密

                    if (!string.IsNullOrEmpty(pwd))
                    {
                        string password = pwd;
                        MD5 md5 = MD5.Create(); //实例化一个md5对像
                        byte[] p = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                        empInfo.emp_pwd = Convert.ToBase64String(p);
                        DbEntities.Entry(empInfo).State = System.Data.Entity.EntityState.Modified;
                        DbEntities.SaveChanges();
                        strMsg.AppendFormat("注册成功");
                    }

                    #endregion
                }
                else
                {
                    strMsg.AppendFormat("该员工不存在");
                }
            }
            catch (Exception ex)
            {

                strMsg.AppendFormat("注册失败:{0}", ex.Message);
            }

            return strMsg.ToString();
        }
    }
}
