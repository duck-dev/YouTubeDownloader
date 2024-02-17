using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Google.Apis.YouTube.v3.Data;
using YouTubeDownloader.Enums;
using YouTubeDownloader.Interfaces;
using YouTubeDownloader.UtilityCollection;
using YouTubeDownloader.ViewModels;

namespace YouTubeDownloader.Models;

public class YouTubeVideo : INotifyPropertyChangedHelper
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private const string YouTubeLink = "https://www.youtube.com/watch?v=";
    
    private readonly Bitmap? _defaultThumbnail = Utilities.CreateImage(Path.Combine(Utilities.AssetsPath, "avalonia-logo.ico")); // TODO: DefaultThumbnail.png

    private string _title = string.Empty;
    private string _description = string.Empty;
    private string _channelName = string.Empty;
    private string _formattedTimeString = string.Empty;
    private Bitmap? _thumbnailImage;

    private bool _addedToQueue;
    private bool _isAudioQueued;
    private bool _isVideoQueued;
    
    public YouTubeVideo(SearchResult searchResult)
    {
        SearchResultSnippet snippet = searchResult.Snippet;

        string id = searchResult.Id.VideoId;
        VideoUrl = $"{YouTubeLink}{id}";
        
        Title = snippet.Title;
        Description = snippet.Description;
        ChannelName = snippet.ChannelTitle;
        
        DateTimeOffset? uploadDateNullable = snippet.PublishedAtDateTimeOffset;
        FormattedTimeString = uploadDateNullable is not {} uploadDate ? string.Empty : Utilities.FormatTimeDifference(uploadDate);

        ThumbnailImage = _defaultThumbnail;
        Task.Run(async () =>
        {
            Bitmap? result = await Utilities.DownloadImage(snippet.Thumbnails.Default__.Url);
            if(result is not null)
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
    
    internal string VideoUrl { get; }
    
    internal bool IsAudioQueued
    {
        get => _isAudioQueued;
        set
        {
            _isAudioQueued = value;
            NotifyPropertyChanged();
        }
    }
    
    internal bool IsVideoQueued
    {
        get => _isVideoQueued;
        set
        {
            _isVideoQueued = value;
            NotifyPropertyChanged();
        }
    }

    public void NotifyPropertyChanged(string propertyName = "") 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    
    internal void AddToQueue(DownloadType type)
    {
        if (QueueViewModel.Instance is not { } queueInstance)
            return;
        
        var queue = queueInstance.Queue;
        if (_addedToQueue)
        {
            QueueElement element = queue.First(x => ReferenceEquals(x.Video, this));
            if (!element.Type.HasFlag(type))
                element.Type |= type;
        }
        else
        {
            queue.Add(new QueueElement(this, type));
        }
        
        _addedToQueue = true;
        ToggleQueued(true, type);
    }

    internal void ToggleQueued(bool isQueued, DownloadType type)
    {
        if (type.HasFlag(DownloadType.Audio))
            IsAudioQueued = isQueued;
        if (type.HasFlag(DownloadType.Video))
            IsVideoQueued = isQueued;
    }
}