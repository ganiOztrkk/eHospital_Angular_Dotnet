using System.Security.Claims;
using E_HospitalServer.DataAccess;
using E_HospitalServer.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CreateAdminUserMiddleware.CreateFirstUser(app);

app.UseHttpsRedirection();

app.MapControllers()
    .RequireAuthorization(policy =>
    {
        policy.RequireClaim(ClaimTypes.NameIdentifier);
        policy.AddAuthenticationSchemes("Bearer");
    });

app.Run();