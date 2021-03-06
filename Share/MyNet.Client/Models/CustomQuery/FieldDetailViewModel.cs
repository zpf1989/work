﻿using MyNet.Client.Help;
using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Http;
using MyNet.Components.Result;
using MyNet.Components.Validation;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.Client.Models.CustomQuery
{
    public class FieldDetailViewModel : FieldViewModel
    {
        IList<DataGridColModel> Cols = new List<DataGridColModel>
                {
                    new DataGridColModel(field:"comment",header:"表注释"),
                    new DataGridColModel(field:"tbname",header:"表名称")
                };
        [JsonIgnore]
        [ValidateIgnore]
        public BaseWindow Window { get; set; }

        [JsonIgnore]
        [ValidateIgnore]
        public bool IsNew
        {
            get { return base.id.IsEmpty(); }
        }

        [JsonIgnore]
        [ValidateIgnore]
        private CmbItem _selFieldType;
        [JsonIgnore]
        [ValidateIgnore]
        public CmbItem SelFieldType
        {
            get { return _selFieldType; }
            set
            {
                if (_selFieldType != value)
                {
                    _selFieldType = value;
                    if (_selFieldType != null && base.fieldtype != _selFieldType.Id)
                    {
                        base.fieldtype = _selFieldType.Id;
                    }
                    base.RaisePropertyChanged("SelFieldType");
                }
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        private CmbItem _selVisible;
        [JsonIgnore]
        [ValidateIgnore]
        public CmbItem SelVisible
        {
            get { return _selFieldType; }
            set
            {
                if (_selVisible != value)
                {
                    _selVisible = value;
                    base.visible = value.Id;
                    base.RaisePropertyChanged("SelVisible");
                }
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        private ICommand _saveCmd;
        [JsonIgnore]
        [ValidateIgnore]
        public ICommand SaveCmd
        {
            get
            {
                if (_saveCmd == null)
                {
                    _saveCmd = new DelegateCommand(SaveAction);
                }
                return _saveCmd;
            }
        }
        private void SaveAction(object parameter)
        {
            if (!this.IsValid)
            {
                MessageWindow.ShowMsg(MessageType.Warning, OperationDesc.Validate, this.Error);
                return;
            }
            var url = ApiUtils.GetApiUrl(this.IsNew ? ApiKeys.Cq_FieldAdd : ApiKeys.Cq_FieldUpdate);
            var rst = HttpUtils.PostResult(url, (FieldViewModel)this, ClientContext.Token);
            string opt = this.IsNew ? OperationDesc.Add : OperationDesc.Edit;
            if (rst.code != ResultCode.Success)
            {
                MessageWindow.ShowMsg(MessageType.Error, opt, rst.msg);
                return;
            }
            MessageWindow.ShowMsg(MessageType.Info, opt, MsgConst.Msg_Succeed);
            if (Window != null)
            {
                Window.DialogResult = true;
                Window.CloseCmd.Execute(null);
            }
        }

        [JsonIgnore]
        [ValidateIgnore]
        private ICommand _tableHelpCmd;
        [JsonIgnore]
        [ValidateIgnore]
        public ICommand TableHelpCmd
        {
            get
            {
                if (_tableHelpCmd == null)
                {
                    _tableHelpCmd = new DelegateCommand(OpenTableHelp);
                }
                return _tableHelpCmd;
            }
        }

        private void OpenTableHelp(object parameter)
        {
            GridHelpWindow.ShowHelp(
                title: "查询表信息帮助",
                multiSel: false,
                dataProvider: GetTables,
                singleSelAction: AfterSelect,
                cols: Cols);
        }

        private IEnumerable<CheckableModel> GetTables()
        {
            return TableMngViewModel.GetTables(new PageQuery { pageIndex = 1, pageSize = 1000 });
        }

        private void AfterSelect(CheckableModel m)
        {
            var table = (TableViewModel)m;
            base.tbid = table.id;
            this.tbname = table.comment;
        }
    }
}
