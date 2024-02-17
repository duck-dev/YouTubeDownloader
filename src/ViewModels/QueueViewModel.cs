using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using YouTubeDownloader.EventArgs;
using YouTubeDownloader.Models;

namespace YouTubeDownloader.ViewModels;

internal class QueueViewModel : ViewModelBase
{
    public QueueViewModel()
    {
        Instance = this;
        Queue.CollectionChanged += (sender, args) =>
        {
            if (args.Action != NotifyCollectionChangedAction.Add || args.NewItems is null) 
                return;
            
            foreach (QueueElement element in args.NewItems)
                element.OnVideoSelectedChanged += VideoSelectedChanged;
        };
    }

    private void VideoSelectedChanged(object? sender, BoolEventArgs e)
    {
        if (sender is not QueueElement element)
            return;
        
        if(e.NewValue == true && !SelectedItems.Contains(element))
            SelectedItems.Add(element);
        else if (e.NewValue == false)
            SelectedItems.Remove(element);
    }

    internal static QueueViewModel? Instance { get; private set; }
    
    internal ObservableCollection<QueueElement> Queue { get; } = new();

    internal List<QueueElement> SelectedItems { get; } = new();

    internal void RemoveQueueElement(QueueElement element)
    {
        SelectedItems.Remove(element);
        Queue.Remove(element);
        element.Video.ToggleQueued(false, element.Type);
        element.IsSelected = false;
    }
}