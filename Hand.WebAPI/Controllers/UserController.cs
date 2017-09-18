using Hand.Business;
using System.Web.Http;
using Hand.Model;

namespace Hand.WebAPI.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        public Employee Emp;

        public UserController()
        {
            Emp = new Employee();
        }

        /// <summary>
        ///  朱乾方
        ///  20170824
        ///  获取所有员工信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getEmp")]
        public IHttpActionResult GetEmp()
        {
            var userInfo = Emp.GetEmp();
            return Ok(userInfo);
        }

        /// <summary>
        /// 朱乾方
        /// 20170828
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="empId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getEmp/{empId}")]
        public IHttpActionResult GetEmpByNo(int? empId)
        {
            var empInfo = Emp.GetEmployeeByNo(empId);
            return Ok(empInfo);
        }

        /// <summary>
        /// 朱乾方
        /// 20170828
        /// 员工的编辑
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [HttpPut]
        [Route("edit/{empId}")]
        public IHttpActionResult UpdateEmp(int? empId, employee emp)
        {
            var empInfo = Emp.EditEmp(empId, emp);
            return Ok(empInfo);
        }

        /// <summary>
        /// 朱乾方
        /// 20170828
        /// 添加员工信息
        /// </summary>
        /// <param name="emp">员工实体信息</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddEmp(employee emp)
        {
            var empInfo = Emp.AddEmp(emp);
            return Ok(empInfo);
        }

        /// <summary>
        /// 20170828
        /// 朱乾方
        /// 根据用户Id删除员工信息
        /// </summary>
        /// <param name="empId">用户Id</param>
        /// <returns></returns>
        [HttpOptions]
        [HttpPut]
        [Route("delete/{empId}")]
        public IHttpActionResult DeleteEmp(int? empId)
        {
            var emp = Emp.DeleteEmp(empId);
            return Ok(emp);
        }

        /// <summary>
        /// 朱乾方
        /// 20170901
        /// 员工注册账号
        /// </summary>
        /// <param name="empNo">员工工号</param>
        /// <param name="pwd">员工密码</param>
        /// <returns></returns>
        [Route("/account/{empNo}/{pwd}")]
        [HttpOptions]
        [HttpPut]
        public IHttpActionResult Account(int? empNo, string pwd)
        {
            Employee emp = new Employee();
            var reg = emp.RegisteredAccount(empNo, pwd);
            return Ok(reg);
        }
    }
}