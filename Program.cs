using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using codeFirstApprochExample.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<codeFirstApprochExampleContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("temp") ?? throw new InvalidOperationException("Connection string 'codeFirstApprochExampleContext' not found.")));

//For Lazy Loading Connection UseLazyLoadingProxies()

// Add services to the container.
builder.Services.AddControllersWithViews().ConfigureApiBehaviorOptions(options =>{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Add services to the container.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
