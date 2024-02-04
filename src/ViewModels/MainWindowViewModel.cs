using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YouTubeDownloader.ViewModels;

internal sealed class MainWindowViewModel : ViewModelBase
{
    internal MainWindowViewModel()
    {
        YouTubeServiceInstance = new YouTubeService(new BaseClientService.Initializer
        {
            ApiKey = Program.ApiKey,
            ApplicationName = "YouTubeDownloader"
        });
    }
    
    internal static YouTubeService? YouTubeServiceInstance { get; private set; }
}