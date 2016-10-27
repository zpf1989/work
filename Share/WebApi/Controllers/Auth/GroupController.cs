﻿using MyNet.Components.Result;
using MyNet.Service.Auth;
using MyNet.WebApi.Filters;
using MyNet.WebApi.Models.Auth.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyNet.WebApi.Extensions;
using MyNet.Components.Mapper;
using MyNet.Model.Auth;
using MyNet.WebApi.Models;
using MyNet.Model;

namespace MyNet.WebApi.Controllers.Auth
{
    [RoutePrefix("api/auth/group")]
    [TokenValidateFilter]
    public class GroupController : BaseController
    {
        //私有变量
        GroupService _groupSrv;
        public GroupController(GroupService groupSrv)
        {
            _groupSrv = groupSrv;
        }

        [HttpPost]
        [Route("add")]
        public OptResult Add(AddGroupViewModel vmAddGroup)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            //
            var token = base.ParseToken(ActionContext);
            var group = OOMapper.Map<AddGroupViewModel, Group>(vmAddGroup);
            rst = _groupSrv.Add(group);

            return rst;
        }

        [HttpPost]
        [Route("update")]
        public OptResult Update(EditGroupViewModel vmEditGroup)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }
            //
            var token = base.ParseToken(ActionContext);

            var group = OOMapper.Map<EditGroupViewModel, Group>(vmEditGroup);
            rst = _groupSrv.Update(group);

            return rst;
        }

        [HttpPost]
        [Route("delete")]
        public OptResult Delete(DelByPkViewModel vmDel)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _groupSrv.Delete(vmDel.pk);

            return rst;
        }

        [HttpPost]
        [Route("multidelete")]
        public OptResult Delete(DelByIdsViewModel vmDels)
        {
            OptResult rst = null;
            if (!ModelState.IsValid)
            {
                rst = OptResult.Build(ResultCode.ParamError, ModelState.Parse());
                return rst;
            }

            var token = base.ParseToken(ActionContext);

            rst = _groupSrv.DeleteBatch(vmDels.pks);

            return rst;
        }
        [HttpPost]
        [Route("querybypage")]
        public OptResult QueryByPage(PageQuery page)
        {
            OptResult rst = null;

            rst = _groupSrv.QueryByPage(page);

            return rst;
        }

        [HttpPost]
        [Route("getall")]
        public OptResult GetAllGroups()
        {
            OptResult rst = null;

            rst = _groupSrv.GetAll();

            return rst;
        }
    }
}