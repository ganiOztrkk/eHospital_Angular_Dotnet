using System.Security.Claims;
using DefaultCorsPolicyNugetPackage;
using E_HospitalServer.Business;
using E_HospitalServer.DataAccess;
using E_HospitalServer.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDefaultCors();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusiness();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

CreateAdminUserMiddleware.CreateFirstUser(app);

app.UseHttpsRedirection();

app.MapControllers()
    .RequireAuthorization(policy =>
    {
        policy.RequireClaim(ClaimTypes.NameIdentifier);
        policy.AddAuthenticationSchemes("Bearer");
    });

app.Run();