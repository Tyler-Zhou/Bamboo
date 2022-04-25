using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Bamboo.Client.Core.Views
{
    /// <summary>
    /// Pagination.xaml 的交互逻辑
    /// </summary>
    public partial class Pagination : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public static RoutedEvent FirstPageEvent;
        /// <summary>
        /// 
        /// </summary>
        public static RoutedEvent PreviousPageEvent;
        /// <summary>
        /// 
        /// </summary>
        public static RoutedEvent NextPageEvent;
        /// <summary>
        /// 
        /// </summary>
        public static RoutedEvent LastPageEvent;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TotalPageProperty;
        /// <summary>
        /// 
        /// </summary>
        public string CurrentPage
        {
            get { return (string)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TotalPage
        {
            get { return (string)GetValue(TotalPageProperty); }
            set { SetValue(TotalPageProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Pagination()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        static Pagination()
        {
            FirstPageEvent = EventManager.RegisterRoutedEvent("FirstPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Pagination));
            PreviousPageEvent = EventManager.RegisterRoutedEvent("PreviousPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Pagination));
            NextPageEvent = EventManager.RegisterRoutedEvent("NextPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Pagination));
            LastPageEvent = EventManager.RegisterRoutedEvent("LastPage", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(Pagination));

            CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(string), typeof(Pagination), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnCurrentPageChanged)));
            TotalPageProperty = DependencyProperty.Register("TotalPage", typeof(string), typeof(Pagination), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTotalPageChanged)));
        }
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler FirstPage
        {
            add { AddHandler(FirstPageEvent, value); }
            remove { RemoveHandler(FirstPageEvent, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler PreviousPage
        {
            add { AddHandler(PreviousPageEvent, value); }
            remove { RemoveHandler(PreviousPageEvent, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler NextPage
        {
            add { AddHandler(NextPageEvent, value); }
            remove { RemoveHandler(NextPageEvent, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public event RoutedEventHandler LastPage
        {
            add { AddHandler(LastPageEvent, value); }
            remove { RemoveHandler(LastPageEvent, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnTotalPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pagination? p = d as Pagination;

            if (p != null)
            {
                Run rTotal = (Run)p.FindName("rTotal");

                rTotal.Text = (string)e.NewValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Pagination? p = d as Pagination;

            if (p != null)
            {
                Run rCurrrent = (Run)p.FindName("rCurrent");

                rCurrrent.Text = (string)e.NewValue;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(FirstPageEvent, this));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(PreviousPageEvent, this));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(NextPageEvent, this));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(LastPageEvent, this));
        }
    }
}
