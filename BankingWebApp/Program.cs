using BankingWebApp.Services;
using BankingWebApp.Services.Interfaces;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection("ApiConfiguration"));
builder.Services.AddSingleton<IApiConfiguration>(sp => sp.GetRequiredService<IOptions<ApiConfiguration>>().Value);

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddHttpClient("BankingApi", client =>
{
    var apiConfig = builder.Configuration.GetSection("ApiConfiguration").Get<ApiConfiguration>();
    client.BaseAddress = new Uri(apiConfig.BaseUrl ?? throw new InvalidOperationException("BaseUrl is not configured"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var assetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(assetsPath),
    RequestPath = "/Assets"
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();