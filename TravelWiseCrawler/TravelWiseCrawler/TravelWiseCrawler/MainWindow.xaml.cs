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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TripKraken.Service.CoreService;

namespace TravelWiseCrawler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TabControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                    
        }

        private void CrawlAllBtn_Click(object sender, RoutedEventArgs e)
        {
            lbCrawlInfo.Items.Clear();

            CrawlN CrawlService = new CrawlN();
            var crawlResult = CrawlService.GetPriceList();
            foreach (var item in crawlResult)
            {
                lbCrawlInfo.Items.Add(item);
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void JsonCitiesToObject_Click(object sender, RoutedEventArgs e)
        {
            CrawlN CrawlService = new CrawlN();
            var result = CrawlService.InsertAllCities();
            foreach (var item in result)
            {
                lbCrawlInfo.Items.Add(item.CityName+" ("+item.Country+")");
            }
        }
    }
}
