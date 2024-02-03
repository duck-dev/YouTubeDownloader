using Avalonia;
using Avalonia.Media;
using Utilities = YouTubeDownloader.UtilityCollection.Utilities;

namespace YouTubeDownloader.Resources;

public static partial class BaseResources
{
    private const int StyleIndex = 2;
    
    // SolidColorBrush
    
    public static readonly SolidColorBrush FullyTransparentBrush =
        Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "FullyTransparent", StyleIndex)
        ?? new SolidColorBrush(Color.Parse("#0000FFFF"));
    public static readonly SolidColorBrush AppRedBrush =
        Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "AppRed", StyleIndex)
        ?? new SolidColorBrush(Color.Parse("#D24545"));
    public static readonly SolidColorBrush TransparentAppPurpleBrush =
        Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "TransparentAppPurple", StyleIndex)
        ?? new SolidColorBrush(Color.Parse("#26660099"));
    public static readonly SolidColorBrush WhiteBrush =
        Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "White", StyleIndex)
        ?? new SolidColorBrush(Color.Parse("#FFFFFF"));
    public static readonly SolidColorBrush BlackBrush =
        Utilities.GetResourceFromStyle<SolidColorBrush, Application>(Application.Current, "Black", StyleIndex)
        ?? new SolidColorBrush(Color.Parse("#0A0A0A"));
    
    // Color
    
    public static readonly Color FullyTransparent = FullyTransparentBrush.Color;
    public static readonly Color AppRed = AppRedBrush.Color;
    public static readonly Color TransparentAppPurple = TransparentAppPurpleBrush.Color;
    public static readonly Color White = WhiteBrush.Color;
    public static readonly Color Black = BlackBrush.Color;
}