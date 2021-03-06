﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Client.Public
{
    public class OperationDesc
    {
        public const string OpenFunc = "打开功能菜单";
        public const string GetUsrs = "获取用户信息";

        public const string Validate = "输入验证";
        public const string Login = "登录";
        public const string GetUsr = "获取用户信息";

        public const string ChangePwd = "修改密码";

        public const string DoAction = "执行动作";

        public const string Add = "新增";
        public const string Delete = "删除";
        public const string Edit = "修改";
        public const string Save = "保存";
        public const string Assign = "分配用户权限";
        public const string Search = "查询";

        /**********************CustomQuery*********************/
        public const string Cq_GetDbTables = "获取数据库表信息";
        public const string Cq_GetDbFields = "获取数据库字段信息";
        public const string Cq_InitTables = "初始化查询表信息";
        public const string Cq_InitFields = "初始化查询字段信息";

        public const string Cq_AddJoinTable = "添加关联表";
        public const string Cq_DelJoinTable = "删除关联表";

        public const string Cq_AddRelField = "添加关联字段";
        public const string Cq_DelRelField = "删除关联字段";

        public const string Cq_ExecQuery = "执行自定义查询";
    }
}
