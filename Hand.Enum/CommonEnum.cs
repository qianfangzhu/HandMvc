using System.ComponentModel;
namespace Hand.Enum
{
    /// <summary>
    /// 朱乾方
    /// 20170829
    /// 是否在职
    /// </summary>
    public enum CommonEnum
    {
        [Description("离职")]
        //离职
        Quit = 0,

        [Description("在职")]
        //在职
        Job = 1
    }

    /// <summary>
    /// 朱乾方
    /// 20170829
    /// 工作流审批状态
    /// </summary>
    public enum WorkStatusEnum
    {
        //已通过
        [Description("已通过")]
        HavePassed = 0,

        //已驳回
        [Description("已驳回")]
        Rejected = 1,

        //审批中
        [Description("审批中")]
        Approval = 2
    }

    /// <summary>
    /// 朱乾方
    /// 20170901
    /// 员工角色
    /// </summary>
    public enum ResponseEnum
    {
        //技术支持
        [Description("技术支持")]
        TechnicalSupport = 1,

        //开发组长
        [Description("开发组长")]
        DevLead = 2,

        //项目经理
        [Description("项目经理")]
        ProjectManager = 3
    }
}
