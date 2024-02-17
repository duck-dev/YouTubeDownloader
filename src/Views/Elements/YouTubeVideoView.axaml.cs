using Avalonia;
using Avalonia.Controls;

namespace YouTubeDownloader.Views.Elements;

public partial class YouTubeVideoView : UserControl
{
    public static readonly StyledProperty<bool> ShowAddQueueButtonsProperty =
        AvaloniaProperty.Register<YouTubeVideoView, bool>(nameof(ShowAddQueueButtons));
    public static readonly StyledProperty<bool> ShowQueueTypeProperty =
        AvaloniaProperty.Register<YouTubeVideoView, bool>(nameof(ShowQueueType));
    
    public YouTubeVideoView()
    {
        InitializeComponent();
    }
    
    public bool ShowAddQueueButtons
    {
        get => GetValue(ShowAddQueueButtonsProperty);
        set => SetValue(ShowAddQueueButtonsProperty, value);
    }
    
    public bool ShowQueueType
    {
        get => GetValue(ShowQueueTypeProperty);
        set => SetValue(ShowQueueTypeProperty, value);
    }
}