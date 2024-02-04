using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Google.Apis.YouTube.v3.Data;
using YouTubeDownloader.Interfaces;
using YouTubeDownloader.UtilityCollection;

namespace YouTubeDownloader.Models;

public class YouTubeVideo : INotifyPropertyChangedHelper
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private readonly Bitmap? _defaultThumbnail = Utilities.CreateImage(Path.Combine(Utilities.AssetsPath, "avalonia-logo.ico")); // TODO: DefaultThumbnail.png

    private string _title = string.Empty;
    private string _description = string.Empty;
    private string _channelName = string.Empty;
    private string _formattedTimeString = string.Empty;
    private Bitmap? _thumbnailImage;
    
    public YouTubeVideo(SearchResult searchResult)
    {
        SearchResultSnippet snippet = searchResult.Snippet;
        
        Title = snippet.Title;
        Description = snippet.Description;
        ChannelName = snippet.ChannelTitle;
        
        DateTimeOffset? uploadDateNullable = snippet.PublishedAtDateTimeOffset;
        FormattedTimeString = uploadDateNullable is not {} uploadDate ? string.Empty : Utilities.FormatTimeDifference(uploadDate);

        ThumbnailImage = _defaultThumbnail;
        Task.Run(async () =>
        {
            Bitmap? result = await Utilities.DownloadImage(snippet.Thumbnails.Default__.Url);
            ThumbnailImage = result;
        });
    }

    internal string Title
    {
        get => _title;
        set
        {
            _title = value;
            NotifyPropertyChanged();
        }
    }

    internal string Description
    {
        get => _description;
        set
        {
            _description = value;
            NotifyPropertyChanged();
        }
    }

    internal string ChannelName
    {
        get => _channelName;
        set
        {
            _channelName = value;
            NotifyPropertyChanged();
        }
    }

    internal string FormattedTimeString
    {
        get => _formattedTimeString;
        set
        {
            _formattedTimeString = value;
            NotifyPropertyChanged();
        }
    }

    internal Bitmap? ThumbnailImage
    {
        get => _thumbnailImage;
        private set
        {
            _thumbnailImage = value;
            NotifyPropertyChanged();
        }
    }

    public void NotifyPropertyChanged(string propertyName = "") 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}