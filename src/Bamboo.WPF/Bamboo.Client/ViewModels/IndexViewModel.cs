using Bamboo.Client.Core.ViewModels;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class IndexViewModel : NavigationViewModel
    {

        private ICalendar mainCalendar;
        private ICalendar subsidiaryCalendar;

        private int year;
        private int month;
        private int day;

        private CultureInfo localCultureInfo = new CultureInfo(CultureInfo.CurrentUICulture.ToString());
        private UniformGrid calendarDisplayUniformGrid;

        private string[] WeekDays = new string[]{
            LunarCalendar.Properties.Resources.Sunday,
            LunarCalendar.Properties.Resources.Monday,
            LunarCalendar.Properties.Resources.Tuesday,
            LunarCalendar.Properties.Resources.Wednesday,
            LunarCalendar.Properties.Resources.Thursday,
            LunarCalendar.Properties.Resources.Friday,
            LunarCalendar.Properties.Resources.Saturday
        };

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="provider"></param>
        public IndexViewModel(IRegionManager regionManager, IContainerProvider provider)
            : base(regionManager, provider)
        {
            HeaderText = "首页          1";
        }
        #endregion

        public void DisplayCalendar(int year, int month)
        {
            int dayNum = DateTime.DaysInMonth(year, month);
            calendarDisplayUniformGrid = GetCalendarUniformGrid(CalendarListBox);

            DateTime dateTime = new DateTime(year, month, 1);
            int firstDayIndex = (int)(dateTime.DayOfWeek);
            calendarDisplayUniformGrid.FirstColumn = firstDayIndex;
            if (firstDayIndex + dayNum > 35)
            {
                calendarDisplayUniformGrid.Rows = 6;
            }
            else if (firstDayIndex + dayNum > 28)
            {
                calendarDisplayUniformGrid.Rows = 5;
            }
            else
            {
                calendarDisplayUniformGrid.Rows = 4;
            }
            List mainDateList = mainCalendar.GetDaysOfMonth(year, month);
            List subsidiaryDateList = null;
            if (subsidiaryCalendar != null)
            {
                subsidiaryDateList = subsidiaryCalendar.GetDaysOfMonth(year, month);
            }

            for (int i = 0; i < dayNum; i++)
            {
                bool isCurrentDay = (dateTime.Date == DateTime.Now.Date);
                Label mainDateLabel = new Label();
                mainDateLabel.HorizontalAlignment = HorizontalAlignment.Center;
                mainDateLabel.VerticalAlignment = VerticalAlignment.Center;
                if (isCurrentDay)
                {
                    mainDateLabel.Background = Brushes.Orange;
                }
                else
                {
                    mainDateLabel.Background = Brushes.Black;
                }

                mainDateLabel.Padding = new Thickness(0, 0, 0, 0);
                mainDateLabel.Margin = new Thickness(0, 0, 0, 0);

                Label hiddenLabel = new Label();
                hiddenLabel.HorizontalAlignment = HorizontalAlignment.Stretch;
                hiddenLabel.VerticalAlignment = VerticalAlignment.Stretch;
                hiddenLabel.Visibility = Visibility.Collapsed;

                mainDateLabel.FontSize = (localCultureInfo.ToString() == "zh-CN") ? 31 : 42;

                if (IsWeekEndOrFestival(ref dateTime, mainDateList, i))
                {
                    mainDateLabel.Foreground = Brushes.Red;
                    if (mainDateList[i].IsFestival)
                    {
                        hiddenLabel.Content = mainDateList[i].Text;
                    }
                }
                else
                {
                    hiddenLabel.Content = "";
                    mainDateLabel.Foreground = Brushes.White;
                }

                mainDateLabel.Content = mainDateList[i].DateOfMonth.ToString(NumberFormatInfo.CurrentInfo);

                Label subsidiaryDateLabel = null;
                if (subsidiaryDateList != null)
                {
                    subsidiaryDateLabel = new Label();
                    subsidiaryDateLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    subsidiaryDateLabel.VerticalAlignment = VerticalAlignment.Center;
                    if (isCurrentDay)
                    {
                        subsidiaryDateLabel.Background = Brushes.Orange;
                    }
                    else
                    {
                        subsidiaryDateLabel.Background = Brushes.Black;
                    }
                    subsidiaryDateLabel.Padding = new Thickness(0, 0, 0, 0);
                    subsidiaryDateLabel.FontSize = 21;

                    subsidiaryDateLabel.Foreground = subsidiaryDateList[i].IsFestival ? Brushes.Red : Brushes.White;
                    subsidiaryDateLabel.Content = subsidiaryDateList[i].Text;
                }

                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.VerticalAlignment = VerticalAlignment.Center;

                stackPanel.Children.Add(hiddenLabel);
                stackPanel.Children.Add(mainDateLabel);
                if (subsidiaryDateLabel != null)
                {
                    stackPanel.Children.Add(subsidiaryDateLabel);
                }

                Button dayButton = new Button();
                dayButton.HorizontalAlignment = HorizontalAlignment.Center;
                dayButton.VerticalAlignment = VerticalAlignment.Center;
                dayButton.Content = stackPanel;
                dayButton.BorderBrush = Brushes.Red;
                dayButton.BorderThickness = new Thickness(1);
                dayButton.Background = Brushes.Black;
                dayButton.Width = 68;

                CalendarListBox.Items.Add(dayButton);

                //Display the current day in another color
                if (isCurrentDay)
                {
                    mainDateLabel.Foreground = Brushes.Red;
                    if (subsidiaryDateLabel != null)
                    {
                        subsidiaryDateLabel.Foreground = Brushes.Red;
                    }
                    dayButton.Background = Brushes.Orange;
                }
                dateTime = dateTime.AddDays(1);
            }

        }

        private static bool IsWeekEndOrFestival(ref DateTime dateTime, List mainDateList, int i)
        {
            return ((int)dateTime.DayOfWeek == 6) || ((int)dateTime.DayOfWeek == 0) || mainDateList[i].IsFestival;
        }

        private void HighlightCurrentDate()
        {
            UIElementCollection dayCollection = calendarDisplayUniformGrid.Children;
            ListBoxItem today;
            int index = DateTime.Now.Day - 1;
            today = (ListBoxItem)(dayCollection[index]);
            today.Foreground = Brushes.Blue;
        }

        private UniformGrid GetCalendarUniformGrid(DependencyObject uniformGrid)
        {
            UniformGrid tempGrid = uniformGrid as UniformGrid;

            if (tempGrid != null)
            {
                return tempGrid;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(uniformGrid); i++)
            {
                DependencyObject gridReturn =
                    GetCalendarUniformGrid(VisualTreeHelper.GetChild(uniformGrid, i));
                if (gridReturn != null)
                {
                    return gridReturn as UniformGrid;
                }
            }
            return null;
        }

        private void WeekdayLabelsConfigure()
        {
            Label tempLabel;

            for (int i = 0; i < 7; i++)
            {
                tempLabel = new Label();
                tempLabel.Width = 650 / 7;
                tempLabel.FontSize = 21;

                tempLabel.HorizontalAlignment = HorizontalAlignment.Stretch;
                tempLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                tempLabel.VerticalAlignment = VerticalAlignment.Stretch;
                tempLabel.VerticalContentAlignment = VerticalAlignment.Center;

                tempLabel.Foreground = (i == 0 || i == 6) ? Brushes.Red : Brushes.White;
                tempLabel.Content = WeekDays[i];
                this.stackPanel1.Children.Add(tempLabel);
            }
        }

        private void TimeSwitchButtonsConfigure()
        {
            PreviousYearButton.Click += PreviousYearOnClick;
            NextYearButton.Click += NextYearOnClick;
            PreviousMonthButton.Click += PreviousMonthOnClick;
            NextMonthButton.Click += NextMonthOnClick;
            CurrentMonthButton.Click += CurrentMonthOnClick;
        }

        //Event handler
        private void WindowOnLoad(Object sender, EventArgs e)
        {
            DisplayCalendar(year, month);
            lblDisplayMonthName.Content = DateTime.Now.ToShortDateString();
            HighlightCurrentDate();
        }

        private void WindowOnClose(Object sender, EventArgs e)
        {
            this.Close();
        }

        private void WindowOnMin(Object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void WindowOnMove(Object sender, EventArgs e)
        {
            this.DragMove();
        }

        private void UpdateMonth()
        {
            CalendarListBox.BeginInit();
            CalendarListBox.Items.Clear();
            DisplayCalendar(year, month);
            CalendarListBox.EndInit();
            lblDisplayMonthName.Content = (new DateTime(year, month, 1)).ToString("Y", localCultureInfo);
            CalendarListBox.SelectedItem = null;

            //Check the calendar range and disable corresponding buttons
            CheckRange();
        }

        private void PreviousYearOnClick(Object sender, RoutedEventArgs e)
        {
            if (year <= 1902)
            {
                return;
            }

            year -= 1;
            UpdateMonth();
        }

        private void NextYearOnClick(Object sender, RoutedEventArgs e)
        {
            if (year >= 2100)
            {
                return;
            }

            year += 1;
            UpdateMonth();
        }

        private void PreviousMonthOnClick(Object sender, RoutedEventArgs e)
        {
            if (month == 1 && year == 1902)
            {
                return;
            }

            month -= 1;
            if (month == 0)
            {
                month = 12;
                year--;
            }
            UpdateMonth();
        }

        private void NextMonthOnClick(Object sender, RoutedEventArgs e)
        {
            if (month == 12 && year == 2100)
            {
                return;
            }

            month += 1;
            if (month > 12)
            {
                month = 1;
                year++;
            }
            UpdateMonth();
        }

        private void CurrentMonthOnClick(Object sender, RoutedEventArgs e)
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;

            UpdateMonth();
            HighlightCurrentDate();
        }

        private void CheckRange()
        {
            //The calendar range is between 01/01/1902 and 12/31/2100
            PreviousYearButton.IsEnabled = (year <= 1902) ? false : true;
            PreviousMonthButton.IsEnabled = (month == 01 && year <= 1902) ? false : true;
            NextYearButton.IsEnabled = (year >= 2100) ? false : true;
            NextMonthButton.IsEnabled = (month == 12 && year >= 2100) ? false : true;
        }

        private void btnSaveImageFile_Click(object sender, RoutedEventArgs e)
        {
            SavePhoto(@"c:\ChineseLunarCalendar.jpg", this.calendarWindow);
            MessageBox.Show("OK");
        }

        protected void SavePhoto(string fileName, Visual visual)
        {
            // 利用RenderTargetBitmap对象，以保存图片
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)this.Width, (int)this.Height, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(visual);

            // 利用JpegBitmapEncoder，对图像进行编码，以便进行保存
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            // 保存文件
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            encoder.Save(fileStream);
            // 关闭文件流
            fileStream.Close();
        }
    }
}
}
