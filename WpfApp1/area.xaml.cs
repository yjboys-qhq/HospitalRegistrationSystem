using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Visifire.Charts;
using Entity;

namespace WpfApp1
{
    /// <summary>
    /// area.xaml 的交互逻辑
    /// </summary>
    public partial class area : Page
    {
        public area()
        {
            InitializeComponent();
            init();
        }
        private List<DateTime> LsTime;
        private List<string> count;

        private void init()
        {
            Simon.Children.Clear();
            CharData c = new CharData();
            c.setData();
            this.LsTime = c.LsTime1;
            this.count = c.Count;
            CreateChartArea("挂号人数统计", LsTime, count);
        }

        #region 折线图
        public void CreateChartArea(string name, List<DateTime> lsTime, List<string> count)
        {
            //创建一个图标
            Chart chart = new Chart();
            //设置图标的宽度和高度
            chart.Width = 580;
            chart.Height = 380;
            chart.Margin = new Thickness(100, 5, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;
            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = false;//3D效果显示
            //创建一个标题的对象
            Title title = new Title();
            //设置标题的名称
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);
            //向图标添加标题
            chart.Titles.Add(title);
            //初始化一个新的Axis
            Axis xaxis = new Axis();
            //设置Axis的属性
            //图表的X轴坐标按什么来分类，如时分秒
            xaxis.IntervalType = IntervalTypes.Months;
            //图表的X轴坐标间隔如2,3,20等，单位为xAxis.IntervalType设置的时分秒。
            xaxis.Interval = 1;
            //设置X轴的时间显示格式为7-10 11：20           
            xaxis.ValueFormatString = "d日/MMM";
            //给图标添加Axis            
            chart.AxesX.Add(xaxis);
            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "人次";
            chart.AxesY.Add(yAxis);
            // 创建一个新的数据线。                   
            DataSeries dataSeriesPineapple = new DataSeries();
            // 设置数据线的格式。         
            dataSeriesPineapple.RenderAs = RenderAs.Area;//折线图
            dataSeriesPineapple.XValueType = ChartValueTypes.DateTime;
            // 设置数据点              
            DataPoint dataPoint2;
            for (int i = 0; i < lsTime.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint2 = new DataPoint();
                // 设置X轴点                    
                dataPoint2.XValue = lsTime[i];
                //设置Y轴点                   
                dataPoint2.YValue = double.Parse(count[i]);
                dataPoint2.MarkerSize = 8;
                //dataPoint2.Tag = tableName.Split('(')[0];
                //设置数据点颜色                  
                // dataPoint.Color = new SolidColorBrush(Colors.LightGray);                   
                dataPoint2.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeriesPineapple.DataPoints.Add(dataPoint2);
            }
            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeriesPineapple);
            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);
            Simon.Children.Add(gr);
        }
        #endregion
        void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //DataPoint dp = sender as DataPoint;
            //MessageBox.Show(dp.YValue.ToString());
        }
    }
}
