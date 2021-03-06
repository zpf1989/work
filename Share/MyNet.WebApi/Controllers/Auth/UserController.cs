﻿using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using MyNet.Components.Result;
using MyNet.Model;
using MyNet.Model.Auth;
using MyNet.Service.Auth;
using MyNet.ViewModel.Auth.User;
using MyNet.WebApi.Extensions;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models;
using MyNet.ViewModel.Auth.User;
using System;
using System.Web.Http;

namespace MyNet.WebApi.Controllers.Auth
{
    [RoutePrefix("api/auth/user")]
    [TokenValidateFilter]
    [ValidateModelFilter]
    public class UserController : BaseController
    {

        //私有变量
        UserService _usrSrv;
        UserPermissionRelService _usrPerRelSrv;

        public UserController(UserService usrSrv, UserPermissionRelService usrPerRelSrv)
        {
            _usrSrv = usrSrv;
            _usrPerRelSrv = usrPerRelSrv;
        }

        [HttpPost]
        [Route("login")]
        [TokenValidateFilter(true)]
        public OptResult Login(LoginVM vmLogin)
        {
            OptResult rst = null;
            if (vmLogin == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空");
                return rst;
            }

            rst = _usrSrv.Login(vmLogin.username, vmLogin.pwd);

            if (rst.code == ResultCode.Success)
            {
                //生成JWT
                var payload = new TokenData
                {
                    iss = rst.data.user_id,
                    iat = (int)(DateTime.UtcNow - DateTimeExtension.GetMinUtcTime()).TotalSeconds
                };

                string token = JWT.JsonWebToken.Encode(payload, ApiContext.JwtSecretKey, JWT.JwtHashAlgorithm.HS256);
                rst = OptResult.Build(ResultCode.Success, "用户登录成功，并已生成token", new { token = token, usrid = rst.data.user_id });
            }

            return rst;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(UserVM vmUsr)
        {
            OptResult rst = null;

            //
            var token = base.ParseToken(ActionContext);

            var usr = OOMapper.Map<UserVM, User>(vmUsr);
            usr.user_creator = token.iss;
            rst = _usrSrv.Add(usr);
            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(UserVM vmUsr)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);
            var usr = OOMapper.Map<UserVM, User>(vmUsr);

            rst = _usrSrv.Update(usr);

            return rst;
        }

        [HttpPost]
        [Route("multidelete")]
        public OptResult DeleteBatch(DelByIdsViewModel vmDels)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);
            rst = _usrSrv.DeleteBatch(vmDels.pks);

            return rst;
        }

        [HttpPost]
        [Route("changepwd")]
        public OptResult ChangePwd(ChgPwdVM vmChangePwd)
        {
            OptResult rst = null;

            rst = _usrSrv.ChangePwd(vmChangePwd.userid, vmChangePwd.oldpwd, vmChangePwd.newpwd);

            return rst;
        }

        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            //rst = _usrSrv.QueryByPage(page);
            rst = _usrSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("get")]
        public OptResult GetUserInfo(GetByIdViewModel vmGetById)
        {
            OptResult rst = null;
            if (vmGetById == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            rst = _usrSrv.Find(vmGetById.pk);
            return rst;
        }

        [HttpPost]
        [Route("assign")]
        public OptResult AssignPermissions(AssignPermVM vmAssignPer)
        {
            OptResult rst = null;
            if (vmAssignPer == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            //
            rst = _usrPerRelSrv.AssignPermissions(vmAssignPer.userId, vmAssignPer.perIds, vmAssignPer.assignAll);

            return rst;
        }

        [HttpPost]
        [Route("getper")]
        public OptResult GetPermissions(GetByIdViewModel vmGetById)
        {
            OptResult rst = null;
            if (vmGetById == null)
            {
                rst = OptResult.Build(ResultCode.ParamError, "参数不能为空或格式不正确");
                return rst;
            }

            rst = _usrPerRelSrv.GetPermissions(vmGetById.pk);

            return rst;
        }
    }
}