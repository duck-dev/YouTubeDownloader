using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using YouTubeDownloader.ViewModels;

namespace YouTubeDownloader;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        string name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type is null) 
            return new TextBlock {Text = "Not Found: " + name};
        
        var control = (Control)Activator.CreateInstance(type)!;
        control.DataContext = data;
        return control;

    }

    public bool Match(object? data) => data is ViewModelBase;
}