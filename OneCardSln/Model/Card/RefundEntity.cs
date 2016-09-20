﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    public class RefundEntity
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string idcard { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string order { get; set; }

        /// <summary>
        /// 退款来源：手机端、电脑商城
        /// </summary>
        public string src { get; set; }

        /// <summary>
        /// 备注：手机端：deviceid，电脑商城：IP地址
        /// </summary>
        public string remark { get; set; }
    }
}
