using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YouTubeDownloader.Interfaces;

public interface INotifyPropertyChangedHelper : INotifyPropertyChanged
{
    void NotifyPropertyChanged([CallerMemberName] string propertyName = "");
}