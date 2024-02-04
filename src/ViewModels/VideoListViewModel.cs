using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.ViewModels;

internal class VideoListViewModel : ViewModelBase
{
    internal string SearchTerm { get; set; } = string.Empty;

    internal ObservableCollection<YouTubeVideo> Videos { get; } = new();
    
    internal async Task Search()
    {
        if (string.IsNullOrEmpty(SearchTerm) || string.IsNullOrWhiteSpace(SearchTerm))
            return;
        Videos.Clear();

        var youtubeService = MainWindowViewModel.YouTubeServiceInstance;
        if (youtubeService is null)
            return;
        
        var searchListRequest = youtubeService.Search.List("snippet");
        searchListRequest.Q = SearchTerm;
        searchListRequest.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.None;
        searchListRequest.MaxResults = 50;
        searchListRequest.Type = "video";
        searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;

        var searchListResponse = await searchListRequest.ExecuteAsync();
        foreach (SearchResult result in searchListResponse.Items)
        {
            var video = new YouTubeVideo(result);
            Videos.Add(video);
        }
    }
}