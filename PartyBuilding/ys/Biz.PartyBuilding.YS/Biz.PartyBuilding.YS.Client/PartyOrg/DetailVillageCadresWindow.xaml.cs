﻿using Biz.PartyBuilding.YS.Client.PartyOrg.Models;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.PartyOrg
{
    /// <summary>
    /// DetailVillageCadresWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DetailVillageCadresWindow : BaseWindow
    {
        private DetailVillageCadresWindow()
        {
            InitializeComponent();


            CmbModel model = cmbSex.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsSex);

            model = cmbNation.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsNation);

            model = cmbXL.DataContext as CmbModel;
            model.Bind(PartyBuildingContext.CmbItemsXL);

            ctlImg.ImgFile = "pack://application:,,,/Biz.PartyBuilding.YS.Client;component/Resources/村干部.jpg";
        }

        public DetailVillageCadresWindow(Org2NewViewModel vm = null)
            : this()
        {
        }

    }
}
