using MBADevExpertModulo1.Infrastructure.IoC;
using MBADevExpertModulo1.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiConfig()
       .AddSwaggerConfig()
       .AddDbContextConfig()
       .AddIdentityConfig();

builder.Services.InitializeInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
