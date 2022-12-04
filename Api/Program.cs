using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        //config.AddJsonFile("aaaa.settings.json", optional: true, reloadOnChange: true);   // 任意の設定ファイルが必要なら追加（未確認）
        //config.AddEnvironmentVariables();   // 要る？
    })
    .ConfigureServices((hostContext, services) =>
    {
        // local.settings.jsonから任意の設定項目を読み込む方法
        services.AddOptions<UserSettings>()
            .Configure<IConfiguration>((settings, configuration) => configuration.Bind("UserSettings", settings));

        // DBに接続する方法
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")));  // local.settings.jsonを読むよ
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();

// 設定を読み込んでみる
public class UserSettings
{
    public string? Name { get; set; }
}

