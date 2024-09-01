using DataSetsDemo.ViewModels;
using System.Net.Http.Headers;
using DataSetsDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddDotVVM<DotvvmStartup>();

builder.Services.AddScoped<ProductDatabaseService>();
builder.Services.AddScoped<GitHubNextTokenService>();
builder.Services.AddScoped<GitHubNextTokenHistoryService>();
builder.Services.AddScoped<ProductMultiSortDatabaseService>();

builder.Services.AddSingleton<GitHubService>();
builder.Services.AddHttpClient<GitHubService>(client =>
{
    var token = builder.Configuration["GitHubToken"];
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    client.DefaultRequestHeaders.Add("User-Agent", builder.Configuration["GitHubUser"]);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseDotVVM<DotvvmStartup>();
app.MapDotvvmHotReload();

app.Run();