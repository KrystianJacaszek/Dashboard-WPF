using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using DashboardLib.Models;

// Szablon elementu Kontrolka użytkownika jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234236

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
        void Load_News(object sender, RoutedEventArgs e)
        {
            SetupNews();
            LoadNews.Visibility = Visibility.Collapsed;
        }
        void Back_Click(object sender, RoutedEventArgs e)
        {
            if (newsModel.CurrentNewsPage > 1)
            {
                this.newsModel.CurrentNewsPage--;
                SetupNews();
            }
        }
        void Next_Click(object sender, RoutedEventArgs e)
        {
            if (newsModel.CurrentNewsPage < 15)
            {
                this.newsModel.CurrentNewsPage++;
                SetupNews();
            }
        }
        void Next_News(object sender, RoutedEventArgs e)
        {
            if (newsModel.CurrentNewsSource < newsModel.URL_Collection.Count - 1)
            {
                this.newsModel.CurrentNewsSource++;
                newsModel.CallAPI();
                SetupNews();
            }
        }
        void Back_News(object sender, RoutedEventArgs e)
        {
            if (newsModel.CurrentNewsSource > 1)
            {
                this.newsModel.CurrentNewsSource--;
                newsModel.CallAPI();
                SetupNews();
            }
        }
        private void SetupNews()
        {
            try
            {
                BitmapImage bi = new BitmapImage();
                bi.CreateOptions = BitmapCreateOptions.None;
                bi.UriSource = new Uri(newsModel.Root.articles[newsModel.CurrentNewsPage].urlToImage, UriKind.Absolute);

                Image.Source = bi;
                Title.Text = newsModel.Root.articles[newsModel.CurrentNewsPage].title;
                Date.Text = newsModel.Root.articles[newsModel.CurrentNewsPage].publishedAt + Environment.NewLine;
                Source.Text = newsModel.Root.articles[newsModel.CurrentNewsPage].source.name;
                Counter.Text = $"News Article: {newsModel.CurrentNewsPage}{Environment.NewLine}";
                NewsText.Text = newsModel.Root.articles[newsModel.CurrentNewsPage].description;
            }
            catch (Exception)
            {
                NewsText.Text = "Something went wrong try to refresh.";
            }
        }
    }
}
