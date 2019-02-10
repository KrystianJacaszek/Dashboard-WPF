using DashboardLib.ApiModules;
using DashboardLib.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardLib.ViewModels
{
    public class NewsWidgetViewModel : BaseViewModel
    {
        private NewsApi newsApi = NewsApi.Instance;

        public void ChangePage(sbyte modifier)
        {
            short targetPage = Convert.ToInt16(CurrentPage + modifier);
            if (targetPage >= 1 && targetPage <= newsList.Count)
            {
                CurrentPage += modifier;
            }
        }

        public override async Task Initialize()
        {
            NewsApi.Rootobject newsData = await newsApi.LoadNews("a");

            if (newsData != null)
            {
                NewsModel[] newsListArray = newsData.articles.Select(item =>
                {
                    NewsModel entry = new NewsModel(item.title, item.publishedAt, item.description, item.urlToImage, item.url, item.source.name, item.content);

                    return entry;

                }).ToArray();
                NewsList = new ObservableCollection<NewsModel>(newsListArray);
            }
        }

        public override Task Destroy()
        {
            return Task.CompletedTask;
        }

        private ObservableCollection<NewsModel> newsList = new ObservableCollection<NewsModel>();

        public ObservableCollection<NewsModel> NewsList
        {
            get { return newsList; }
            set
            {
                if (value != newsList)
                {
                    if (value != newsList)
                    {
                        newsList = value;
                        NotifyPropertyChanged("NewsList");
                        NotifyPropertyChanged("CurrentlyDisplayedNews");
                    }
                }
            }
        }

        private short currentPage = 1;
        public short CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (value != currentPage)
                {
                    if (value != currentPage)
                    {
                        currentPage = value;
                        NotifyPropertyChanged("CurrentPage");
                        NotifyPropertyChanged("CurrentlyDisplayedNews");
                    }
                }
            }
        }

        public NewsModel CurrentlyDisplayedNews => newsList.Count > 0 ? newsList[currentPage - 1] : null;
    }
}
