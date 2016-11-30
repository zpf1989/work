﻿using LiveCharts;
using LiveCharts.Wpf;
using MyNet.Components.Misc;
using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biz.PartyBuilding.YS.Client.PartyOrg.Query
{
    /// <summary>
    /// df.xaml 的交互逻辑
    /// </summary>
    public partial class df : Page
    {
        IEnumerable<dynamic> dfAll = PartyBuildingContext.Df;

        public SeriesCollection ColSeries { get; set; }
        public SeriesCollection PieSeries { get; set; }
        public List<string> ColLabels { get; set; }
        public Func<ChartPoint, string> PiePointLabel { get; set; }
        public NumberRange[] BaseRanges = new NumberRange[]
        {
            new NumberRange {Max=3000,Unit="元" },
            new NumberRange {Min=3000,Max=5000,Unit="元" },
            new NumberRange {Min=5000,Max=10000,Unit="元" },
            new NumberRange {Min=10000,Unit="元"}
        };

        public df()
        {
            InitializeComponent();
            DataContext = this;

            ColSeries = new SeriesCollection();
            PieSeries = new SeriesCollection();
            ColLabels = new List<string>();

            PiePointLabel = ChartHelper.PiePointLabel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtDfTotal.Text = dfAll.Sum(m => m.df_year_actual).ToString();

            radioDzz.Command.Execute("dzz");
            radioDzz.IsChecked = true;

            radioPieDzz.Command.Execute("dzz");
            radioPieDzz.IsChecked = true;
        }

        ICommand _cmdLoadColChart;
        public ICommand CmdLoadColChart
        {
            get
            {
                if (_cmdLoadColChart == null)
                {
                    _cmdLoadColChart = new DelegateCommand(LoadColChartAction);
                }
                return _cmdLoadColChart;
            }
        }

        private void LoadColChartAction(object parameter)
        {
            ColSeries.Clear();
            ColLabels.Clear();

            if (parameter == null)
            {
                return;
            }
            var type = parameter.ToString();
            //colChart.AxisX[0].Title = type == "dzz" ? "党组织" : "党员";
            IEnumerable<IGrouping<string, dynamic>> groups = dfAll.GroupBy(m => (string)(type == "dzz" ? m.dy_party : m.dy_name));
            IEnumerable<IGrouping<string, dynamic>> gpYears = dfAll.GroupBy(m => (string)m.df_year);

            foreach (var gp in groups)
            {
                ColLabels.Add(gp.Key);
            }

            StackedColumnSeries series = null;
            foreach (var gpY in gpYears)
            {
                series = new StackedColumnSeries { Title = gpY.Key, StackMode = StackMode.Values, DataLabels = true, Values = new ChartValues<double>() };
                foreach (var gp in groups)
                {
                    series.Values.Add((double)gpY.Where(m => (type == "dzz" ? m.dy_party : m.dy_name) == gp.Key)
                                        .Sum(m => m.df_year_actual));
                }
                ColSeries.Add(series);
            }


        }

        ICommand _cmdPies;
        public ICommand CmdLoadPies
        {
            get
            {
                if (_cmdPies == null)
                {
                    _cmdPies = new DelegateCommand(CmdLoadPiesAction);
                }
                return _cmdPies;
            }
        }

        private void CmdLoadPiesAction(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            PieSeries.Clear();
            string pieType = parameter.ToString();
            switch (pieType)
            {
                case "dzz":
                    ChartHelper.LoadPies(PieSeries, dfAll.GroupBy(m => (string)m.dy_party), PiePointLabel);
                    break;
                case "year":
                    ChartHelper.LoadPies(PieSeries, dfAll.GroupBy(m => (string)m.df_year), PiePointLabel);
                    break;
                case "base":
                    double min, max;
                    foreach (var range in BaseRanges)
                    {
                        min = range.Min.HasValue ? (double)range.Min : 0;
                        max = range.Max.HasValue ? (double)range.Max : 10000000;
                        ChartHelper.AddAPie(PieSeries, range.Title,
                            new ChartValues<double> { dfAll.Count(m => m.df_base >= min && m.df_base < max) }
                            , PiePointLabel);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
