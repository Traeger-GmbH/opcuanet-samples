// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Client.SignalR
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// This use case demonstrates how to forward data changes of OPC UA nodes to a web browser
    /// using an ASP.NET Core SignalR hub.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
