using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using Humanizer;
using Humanizer.Localisation;

namespace YouTubeDownloader.UtilityCollection;

public static partial class Utilities
{
    internal const string AssetsPath = "avares://YouTubeDownloader/Assets/";
    
    /// <summary>
    /// Log a message to the debug output (for debugging purposes).
    /// </summary>
    /// <param name="message">The message to be logged as a string.</param>
    internal static void Log(string? message)
    {
        Console.WriteLine(message);
        System.Diagnostics.Trace.WriteLine(message);
    }
    
    /// <summary>
    /// Retrieves a resource from a specified <see cref="IResourceNode"/> and tries to cast it to the specified type.
    /// </summary>
    /// <param name="element">The element to retrieve the resource from.</param>
    /// <param name="resourceName">The name of the resource you want to retrieve (key).</param>
    /// <typeparam name="T">The actual type of the resource you want to retrieve.</typeparam>
    /// <returns>The resource as it's actual type.</returns>
    internal static T? GetResource<T>(IResourceNode element, string resourceName) where T : class
    {
        element.TryGetResource(resourceName, ThemeVariant.Default, out object? resource);
        return resource as T;
    }

    /// <summary>
    /// Retrieves a resource from a specified <see cref="Style"/>, which is in turns contained
    /// in the <see cref="Styles"/> of an <see cref="IStyleHost"/>, and tries to cast it to the specified type.
    /// </summary>
    /// <param name="element">The element to retrieve the resource from.</param>
    /// <param name="resourceName">The name of the resource you want to retrieve (key).</param>
    /// <param name="styleIndex">The index of the <see cref="Style"/> inside the <see cref="Styles"/> collection
    /// of the <paramref name="element"/>.</param>
    /// <typeparam name="TResource">The actual type of the resource you want to retrieve.</typeparam>
    /// <typeparam name="TElement">The type of the element to retrieve the resource from.
    /// This type must implement <see cref="IResourceNode"/>.</typeparam>
    /// <returns>The resource as it's actual type.</returns>
    internal static TResource? GetResourceFromStyle<TResource, TElement>(TElement? element, string resourceName, int styleIndex)
        where TResource : class
        where TElement : IStyleHost, IResourceNode
    {
        var styleInclude = element?.Styles[styleIndex] as StyleInclude;
        return (styleInclude?.Loaded is Style style ? GetResource<TResource>(style, resourceName) : null);
    }
    
    /// <summary>
    /// Creates a <see cref="Bitmap"/> image, based on an image-file whose path is specified as a passed argument.
    /// </summary>
    /// <param name="path">The path of the image-file.</param>
    /// <returns>The <see cref="Bitmap"/> image or null if the operation was successful due to a wrong path for example.</returns>
    internal static Bitmap? CreateImage(string path)
    {
        var uri = new Uri(path);
        Stream? asset = null;
        try
        {
            asset = AssetLoader.Open(uri);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return asset is null ? null : new Bitmap(asset);
    }

    internal static async Task<Bitmap?> DownloadImage(string url)
    {
        using var client = new HttpClient();
        try
        {
            byte[] result = await client.GetByteArrayAsync(url);
            using var stream = new MemoryStream(result);
            return new Bitmap(new MemoryStream(result));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    internal static string FormatTimeDifference(DateTimeOffset dateTime)
    {
        TimeSpan span = DateTimeOffset.Now.Subtract(dateTime);
        return $"{span.Humanize(maxUnit: TimeUnit.Year, culture: CultureInfo.InvariantCulture)} ago";
    }
}