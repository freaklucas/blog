using Blog.Controllers;
using Blog.Data;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDataContext>();
builder.Services.AddControllers();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddSingleton<TokenService>();
builder.Services.AddTransient<AccountController>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
