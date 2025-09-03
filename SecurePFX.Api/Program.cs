using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecurePFX.Api.Extensions;
using SecurePFX.Api.Mapping;
using SecurePFX.Application.Interfaces;
using SecurePFX.Application.Mapping;
using SecurePFX.Application.Services;
using SecurePFX.Application.Settings;
using SecurePFX.Domain.Interfaces.Repositories;
using SecurePFX.Domain.Interfaces.UoW;
using SecurePFX.Infrastructure.Authentication;
using SecurePFX.Infrastructure.Data.Contexts;
using SecurePFX.Infrastructure.Data.Repositories;
using SecurePFX.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

// Configuração básica da API
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

// Configurações
builder.Services.Configure<CertificateSettings>(builder.Configuration.GetSection("CertificateSettings"));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autenticação JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper(typeof(CertificateProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAutoMapper(typeof(CompanyProfile));
builder.Services.AddAutoMapper(typeof(AuthorizeCompanyProfile));

// MongoDB
builder.Services.AddSingleton<MongoDbContext>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoDbContext(Options.Create(settings));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositórios
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();    

// Serviços de Aplicação
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAuthorizeCompanyService, AuthorizeCompanyService>();

// Swagger (apenas para desenvolvimento)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

builder.Services.AddRabbitMqServices(builder.Configuration);

var app = builder.Build();

// Pipeline de requisições HTTP
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