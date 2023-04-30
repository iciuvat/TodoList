using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TodoDbSettings>(
    builder.Configuration.GetSection("TodoDatabase"));

builder.Services.AddSingleton<TodosService>();
builder.Services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();