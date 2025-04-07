namespace MBADevExpertModulo1.WebAPI.Configurations;

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        return builder;
    }
}