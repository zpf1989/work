﻿using EmitMapper.MappingConfiguration;
using MyNet.Components.Extensions;
using MyNet.Components.Mapper;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Models;
using MyNet.Model.CustomQuery;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Client.Models.CustomQuery
{
    public class FieldViewModel : CheckableModel, ICopyable
    {
        public string id { get; set; }
        public string tbid { get; set; }
        private string _fieldname;
        [Required(ErrorMessage = "字段名称不能为空")]
        [StringLength(20, ErrorMessage = "字段名称最大为20个字符")]
        public string fieldname
        {
            get { return _fieldname; }
            set
            {
                if (_fieldname == value)
                {
                    return;
                }
                _fieldname = value;
                base.RaisePropertyChanged("fieldname");
            }
        }

        private string _displayname;
        [MaxLength(40, ErrorMessage = "显示名称最大为40个字符")]
        public string displayname
        {
            get { return _displayname; }
            set
            {
                if (_displayname == value)
                {
                    return;
                }
                _displayname = value;
                base.RaisePropertyChanged("displayname");
            }
        }

        private string _fieldtype;
        [Required(ErrorMessage = "字段类型不能为空")]
        [MaxLength(20, ErrorMessage = "字段类型最大为20个字符")]
        public string fieldtype
        {
            get { return _fieldtype; }
            set
            {
                if (_fieldtype == value)
                {
                    return;
                }
                _fieldtype = value;
                base.RaisePropertyChanged("fieldtype");
            }
        }

        private string _remark;
        [MaxLength(255, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string remark
        {
            get { return _remark; }
            set
            {
                if (_remark == value)
                {
                    return;
                }
                _remark = value;
                base.RaisePropertyChanged("remark");
            }
        }

        private string _visible;
        public string visible
        {
            get { return _visible; }
            set
            {
                if (_visible == value)
                {
                    return;
                }
                _visible = value;
                base.RaisePropertyChanged("visible");
            }
        }

        [JsonIgnore]
        private string _tbname;
        public string tbname
        {
            get { return _tbname; }
            set
            {
                if (_tbname != value)
                {
                    _tbname = value;
                    base.RaisePropertyChanged("tbname");
                    if (string.IsNullOrEmpty(_tbname))
                    {
                        tbid = "";
                    }
                }
            }
        }

        [JsonIgnore]
        public string fieldfullname { get { return string.Format("{0}.{1}[{2}]", tbname, displayname, fieldname); } }
        [JsonIgnore]
        private int _order;
        [JsonIgnore]
        public int order
        {
            get { return _order; }
            set
            {
                if (_order != value)
                {
                    _order = value;
                    base.RaisePropertyChanged("order");
                }
            }
        }

        private string _mydisplayname;
        /// <summary>
        /// 查询指定显示名称
        /// </summary>
        public string mydisplayname
        {
            get
            {
                if (_mydisplayname.IsEmpty())
                {
                    return displayname;
                }
                return _mydisplayname;
            }
            set
            {
                if (_mydisplayname == value)
                {
                    return;
                }
                _mydisplayname = value;
                base.RaisePropertyChanged("mydisplayname");
            }
        }

        public void CopyTo(object target)
        {
            if (target == null)
            {
                return;
            }
            var vmField = (FieldDetailViewModel)target;
            vmField.id = this.id;
            vmField.tbid = this.tbid;
            vmField.tbname = this.tbname;
            vmField.displayname = this.displayname;
            vmField.fieldname = this.fieldname;
            vmField.fieldtype = this.fieldtype;
            vmField.visible = this.visible;
            vmField.remark = this.remark;
        }

        public static FieldViewModel Parse(DbField dbField)
        {
            if (dbField == null)
            {
                return null;
            }
            FieldViewModel fvm = OOMapper.Map<DbField, FieldViewModel>(dbField,
                new DefaultMapConfig()
                .ConvertUsing<DbField, FieldViewModel>(f => new FieldViewModel
                {
                    fieldname = f.column_name,
                    displayname = f.column_comment,
                    fieldtype = f.field_type,
                    tbname = f.table_name
                }));
            return fvm;
        }

        public void ResetOrder()
        {
            order = 0;
        }


        public override string ToString()
        {
            return fieldfullname;
        }
    }
}
