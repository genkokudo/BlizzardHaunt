using BlizzardHaunt.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.UseStaticFiles();
// ��������ǉ�
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".data"] = "application/octet-stream";
provider.Mappings[".wasm"] = "application/wasm";
provider.Mappings[".br"] = "application/octet-stream";   // .br �t�@�C���ɃA�N�Z�X�ł���悤�ɒǉ�
provider.Mappings[".js"] = "application/javascript";     // ��̕ϊ��ׂ̈ɒǉ�

app.UseStaticFiles(new StaticFileOptions()
{
    ContentTypeProvider = provider,
    OnPrepareResponse = context =>
    {
        var path = context.Context.Request.Path.Value;
        var extension = Path.GetExtension(path);

        // �u.gz�v�u.br�v�t�@�C���ɃA�N�Z�X�����ꍇ�� Content-Type �� Content-Encoding ��ݒ肷��
        if (extension == ".gz" || extension == ".br")
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path) ?? "";
            if (provider.TryGetContentType(fileNameWithoutExtension, out string? contentType))
            {
                context.Context.Response.ContentType = contentType;
                context.Context.Response.Headers.Add("Content-Encoding", extension == ".gz" ? "gzip" : "br");
            }
        }
    },
});
// �����܂Œǉ�

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
