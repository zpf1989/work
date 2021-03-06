﻿using MyNet.Components.Result;
using MyNet.Model.CustomQuery;
using MyNet.Service.CustomQuery;
using MyNet.WebApi.Controllers;
using MyNet.WebApi.Extensions;
using MyNet.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyNet.WebApi.Controllers.CustomQuery
{
    [RoutePrefix("api/customquery/query")]
    [ValidateModelFilter]
    public class CustomQueryController : BaseController
    {
        private CustomQueryService _cqSrv;
        public CustomQueryController(CustomQueryService cqSrv)
        {
            _cqSrv = cqSrv;
        }
        [HttpPost]
        [Route("do")]
        public OptResult Query(QueryModel model)
        {
            OptResult rst = null;

            var token = base.ParseToken(ActionContext);
            rst = _cqSrv.Query(model);

            return rst;
        }
    }
}
