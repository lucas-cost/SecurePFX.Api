using Microsoft.Extensions.Options;
using SecurePFX.Api.Extensions;
using SecurePFX.Api.Mapping;
using SecurePFX.Application.Interfaces;
using SecurePFX.Application.Mapping;
using SecurePFX.Application.Services;
using SecurePFX.Application.Settings;
using SecurePFX.Domain.Interfaces.Repositories;
using SecurePFX.Infrastructure.Authentication;
using SecurePFX.Infrastructure.Data.Contexts;
using SecurePFX.Infrastructure.Data.Repositories;
using SecurePFX.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Configura��o b�sica da API
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

// Configura��es
builder.Services.Configure<CertificateSettings>(builder.Configuration.GetSection("CertificateSettings"));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Autentica��o JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper(typeof(CertificateProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));

// MongoDB
builder.Services.AddSingleton<MongoDbContext>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoDbContext(Options.Create(settings));
});

// Reposit�rios
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();

// Servi�os de Aplica��o
builder.Services.AddScoped<ICertificateService, CertificateService>();

// Swagger (apenas para desenvolvimento)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

builder.Services.AddRabbitMqServices(builder.Configuration);

var app = builder.Build();

// Pipeline de requisi��es HTTP
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