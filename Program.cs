using studentInj.DataClass;
using studentInj.Pages.Clients;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IDatabaseConnection>(e => new DatabaseConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<Student>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();

app.Run();

