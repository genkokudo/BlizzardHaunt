using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var host = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        //config.AddJsonFile("aaaa.settings.json", optional: true, reloadOnChange: true);   // �C�ӂ̐ݒ�t�@�C�����K�v�Ȃ�ǉ��i���m�F�j
        //config.AddEnvironmentVariables();   // �v��H
    })
    .ConfigureServices((hostContext, services) =>
    {
        // local.settings.json����C�ӂ̐ݒ荀�ڂ�ǂݍ��ޕ��@
        services.AddOptions<UserSettings>()
            .Configure<IConfiguration>((settings, configuration) => configuration.Bind("UserSettings", settings));

        // DB�ɐڑ�������@
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")));  // local.settings.json��ǂނ�
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();

// �ݒ��ǂݍ���ł݂�
public class UserSettings
{
    public string? Name { get; set; }
}

