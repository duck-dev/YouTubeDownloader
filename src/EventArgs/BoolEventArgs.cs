namespace YouTubeDownloader.EventArgs;

public class BoolEventArgs : System.EventArgs
{
    public BoolEventArgs(bool newValue)
    {
        NewValue = newValue;
    }
    
    public bool NewValue { get; set; }
}