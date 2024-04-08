using Blog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDataContext>();
builder.Services.AddControllers();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
