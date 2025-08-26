using VaccinationCard.Api.Middlewares;
using VaccinationCard.Application;
using VaccinationCard.Infrastructure;
using VaccinationCard.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependecy injection
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Configuring application CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll",
        policy => policy.AllowAnyOrigin()// Replace with your frontend URL
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(TokenSettings.TokenConfiguration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors("allowAll");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
