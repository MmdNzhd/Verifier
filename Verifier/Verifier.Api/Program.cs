using Microsoft.EntityFrameworkCore;
using Verifier.Application.Users;
using Verifier.Infrastructure.Persistence;
using Verifier.Infrastructure.Users;

// Test comment

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<VerifierDbContext>(options =>
{
    options.UseSqlite("Data Source=verifier.db");
});

builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
