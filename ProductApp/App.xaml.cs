using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using ProductApp.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProductApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? app { get; private set; }

        public App()
        {
            app = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddScoped<MainWindow>();
                services.AddDbContext<DataContexts>(x => x.UseSqlServer(@"Server=tcp:ilona-sqlserver.database.windows.net,1433;Initial Catalog=sql-db;Persist Security Info=False;User ID=SqlAdmin;Password=BytMig123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
                
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await app!.StartAsync();
            var MainWindow = app.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
