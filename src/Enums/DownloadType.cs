using System;

namespace YouTubeDownloader.Enums;

[Flags]
public enum DownloadType
{
    None = 0,
    Audio = 1,
    Video = 2
}