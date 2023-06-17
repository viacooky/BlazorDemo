using System;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.AspNetCore.Hosting;

namespace BlazorApp.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ServerManager.RunServer(Array.Empty<string>());
    }

    protected override void OnExit(ExitEventArgs e)
    {
        ServerManager.StopServer();
        base.OnExit(e);
    }
}

public class ServerManager
{
    private static IHost _host;

    public static void RunServer(string[] args)
    {
        _host = CreateHostBuilder(args).Build();
        _host.Start();
    }

    public static void StopServer()
    {
        _host.Dispose();
        _host = null;
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   
                   .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseUrls("http://*:5000", "https://*:5001");
                    });
    }
}