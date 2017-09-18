namespace Hand.Model
{
    /// <summary>
    /// 朱乾方
    /// 20170825
    /// 员工信息
    /// </summary>
    public class EmployeeInfo
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmpNo { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 员工手机号
        /// </summary>
        public string EmpMobile { get; set; }

        /// <summary>
        /// 员工邮件
        /// </summary>
        public string EmpEmail { get; set; }

        /// <summary>
        /// 员工职位
        /// </summary>
        public string EmpRoleName { get; set; }

        /// <summary>
        /// 是否在职
        /// </summary>
        public string EmpIsValid { get; set; }

        /// <summary>
        /// 员工工作地点
        /// </summary>
        public string EmpWorkAddress { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public string EmpJoinTime { get; set; }

        /// <summary>
        /// 员工所在部门
        /// </summary>
        public string EmpDeptName { get; set; }
    }
}
