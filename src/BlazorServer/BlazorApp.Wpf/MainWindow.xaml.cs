using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;

namespace BlazorApp.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        webview.Source = new Uri("https://127.0.0.1:5001");
        webview.CoreWebView2InitializationCompleted += (a, c) =>
        {
            webview.CoreWebView2.ContextMenuRequested += (sender, args) =>
            {
                var newItem = webview.CoreWebView2.Environment.CreateContextMenuItem(
                    "在浏览器中打开", null, CoreWebView2ContextMenuItemKind.Command);

                newItem.CustomItemSelected += (send, ex) =>
                {
                    System.Diagnostics.Process.Start("explorer.exe", args.ContextMenuTarget.PageUri);
                };

                args.MenuItems.Insert(args.MenuItems.Count, newItem);
            };
        };
    }
}