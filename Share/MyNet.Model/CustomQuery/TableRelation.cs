﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.CustomQuery
{
    public class TableRelation
    {
        public string PrimeTable { get; set; }
        public IList<JoinTable> JoinTables { get; set; }

        public TableRelation()
        {
            JoinTables = new List<JoinTable>();
        }
    }

    public class JoinTable
    {
        public string Table { get; set; }
        public TableJoinType JoinType { get; set; }
        /// <summary>
        /// 关联字段集合
        /// </summary>
        public IList<RelationField> RelFields { get; set; }

        public JoinTable()
        {
            JoinType = TableJoinType.Left;
            RelFields = new List<RelationField>();
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", Table, JoinType);
        }
    }

    public enum TableJoinType
    {
        [Description("左连接")]
        Left,
        [Description("右连接")]
        Right,
        [Description("内连接")]
        Inner
    }

    public class RelationField
    {
        public string Table2 { get; set; }
        /// <summary>
        /// 关联字段1
        /// </summary>
        public string Field1 { get; set; }
        /// <summary>
        /// 关联字段2
        /// </summary>
        public string Field2 { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", Field1, Field2);
        }
    }
}
