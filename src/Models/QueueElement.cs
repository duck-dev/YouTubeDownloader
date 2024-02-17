using System;
using System.ComponentModel;
using YouTubeDownloader.Enums;
using YouTubeDownloader.EventArgs;
using YouTubeDownloader.Interfaces;

namespace YouTubeDownloader.Models;

public class QueueElement : INotifyPropertyChangedHelper
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<BoolEventArgs>? OnVideoSelectedChanged; 
    
    private bool _isSelected;
    
    public QueueElement(YouTubeVideo video, DownloadType type)
    {
        Video = video;
        Type = type;
    }
    
    internal YouTubeVideo Video { get; }
    
    internal DownloadType Type { get; set; }

    internal bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            NotifyPropertyChanged();
            OnVideoSelectedChanged?.Invoke(this, new BoolEventArgs(_isSelected));
        }
    }

    public void NotifyPropertyChanged(string propertyName = "") 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}