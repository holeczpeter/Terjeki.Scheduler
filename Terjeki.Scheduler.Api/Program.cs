using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorDev", policy =>
    {
        policy
            .AllowAnyOrigin()
          
          .AllowAnyHeader()
          .AllowAnyMethod();
        // Ha hiteles�t�st is haszn�lsz: .AllowCredentials();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
       .AddScoped<AuthenticationStateProvider, TerjekiAuthenticationStateProvider>();

// illetve, ha MockDatabase ezt k�zvetlen�l k�ri:
builder.Services
       .AddScoped<TerjekiAuthenticationStateProvider>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
builder.Services.AddApplicationServices();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowBlazorDev");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
