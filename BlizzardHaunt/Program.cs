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
// ここから追加
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".data"] = "application/octet-stream";
provider.Mappings[".wasm"] = "application/wasm";
provider.Mappings[".br"] = "application/octet-stream";   // .br ファイルにアクセスできるように追加
provider.Mappings[".js"] = "application/javascript";     // 後の変換の為に追加

app.UseStaticFiles(new StaticFileOptions()
{
    ContentTypeProvider = provider,
    OnPrepareResponse = context =>
    {
        var path = context.Context.Request.Path.Value;
        var extension = Path.GetExtension(path);

        // 「.gz」「.br」ファイルにアクセスした場合は Content-Type と Content-Encoding を設定する
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
app.UseStaticFiles();
// ここまで追加

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
