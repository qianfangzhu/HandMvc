using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Hand.Business;

namespace Hand.Web.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 朱乾方
        /// 20170824
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        /// <summary>
        /// 朱乾方
        /// 20170824
        /// 首页页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }

        ///<Summary>
        /// 
        /// 
        /// </Summary>
        /// <returns></returns>
        public int LoginConfirm()
        {
            int empNo = int.Parse(Request["empNo"]);
            string pwd = Request["pwd"];
            Employee e = new Employee();
            if (e.QueryEmployee(empNo, pwd))
            {
                Response.Cookies["empNo"].Value = empNo.ToString();
                Response.Cookies["empNo"].Expires = DateTime.Now.AddDays(1);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 朱乾方
        /// 20170830
        /// 用户编辑页面
        /// </summary>
        /// <param name="empId">员工编号</param>
        /// <returns></returns>
        public ActionResult EditEmployee(int? empId)
        {
            ViewBag.SelectItem = new List<SelectListItem>
            {
                new SelectListItem {Text="请选择",Value="0" },
                new SelectListItem {Text="技术支持",Value="1" },
                new SelectListItem {Text="开发组长",Value="2" },
                new SelectListItem {Text="项目经理",Value="3" }
            };
            ViewBag.EmpId = empId;
            return View();
        }
    }
}