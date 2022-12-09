global using Api.AccountTransactions.Dtos;
global using Api.AccountTransactions.Filter;
global using Api.AccountTransactions.Models;
global using Api.AccountTransactions.Services;
global using Api.AccountTransactions.Swagger;
global using Api.AccountTransactions.Validator;
global using FluentValidation;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.Filters;
global using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TransactionDbContext>(options => options.UseInMemoryDatabase("Transactions"));

builder.Services
    .AddMvc(options =>
        options.Filters.Add(typeof(ValidationFilterAttribute))
        );

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = typeof(Program).Assembly.GetName().Name,
            Version = typeof(Program).Assembly.GetName().Version.ToString(),
            Description = "This is a simple account transactions API"
        });
    options.ExampleFilters();
})
    .AddSwaggerExamplesFromAssemblies(typeof(SwaggerExamples).Assembly);

builder.Services.AddControllers(options =>
        options.Filters.Add(new HttpResponseExceptionFilter()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionValidator>();


builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>()
    .AddSingleton<IValidator<Transaction>, TransactionValidator>()
    .AddScoped<ITransactionService, TransactionService>()
    .AddScoped<ValidationFilterAttribute>();

// Authentication
builder.Services.AddAuthentication("Basic");
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
    });

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
