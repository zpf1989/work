﻿using MyNet.Model.Card;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Card.Models
{
    public class SingleProcessParam
    {
        public CardInfo card { get; set; }
        public string opt { get; set; }
        public IDbTransaction tran { get; set; }
    }
}
