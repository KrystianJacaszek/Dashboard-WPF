using DashboardLib.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dashboard.CustomControls
{
    public sealed partial class NewsWidget : UserControl
    {
        NewsModel newsModel = new NewsModel();
        public NewsWidget()
        {
            newsModel.SetupList();
            newsModel.CallAPI();

            InitializeComponent();
        }
        void Today_News(object sender, RoutedEventArgs e)
        {
            SetupForm();
            Today.Visibility = Visibility.Collapsed;
        }
        void Back_Click(object sender, RoutedEventArgs e)
        {
            if (newsModel.NewsCounter > 1)
            {
                this.newsModel.NewsCounter--;
                SetupForm();
            }
        }
        void Next_Click(object sender, RoutedEventArgs e)
        {
            if (newsModel.NewsCounter < 15)
            {
                this.newsModel.NewsCounter++;
                SetupForm();
            }
        }
        void Next_News(object sender, RoutedEventArgs e)
        {
            if (newsModel.SourceCounter < newsModel.URL_Collection.Count - 1)
            {
                this.newsModel.SourceCounter++;
                newsModel.CallAPI();
                SetupForm();
            }
        }
        void Back_News(object sender, RoutedEventArgs e)
        {
            if (newsModel.SourceCounter > 1)
            {
                this.newsModel.SourceCounter--;
                newsModel.CallAPI();
                SetupForm();
            }
        }
        private void SetupForm()
        {
            try
            {
                BitmapImage bi = new BitmapImage();
                bi.CreateOptions = BitmapCreateOptions.None;
                bi.UriSource = new Uri(newsModel.Root.articles[newsModel.NewsCounter].urlToImage, UriKind.Absolute);

                Image.Source = bi;
                Title.Text = newsModel.Root.articles[newsModel.NewsCounter].title;
                Date.Text = newsModel.Root.articles[newsModel.NewsCounter].publishedAt + Environment.NewLine;
                Source.Text = newsModel.Root.articles[newsModel.NewsCounter].source.name;
                Counter.Text = $"News Article: {newsModel.NewsCounter}{Environment.NewLine}";
                NewsText.Text = newsModel.Root.articles[newsModel.NewsCounter].description;
            }
            catch (Exception)
            {
                NewsText.Text = "Something goes wrong try to refresh.";
            }            
        }
    }
}
