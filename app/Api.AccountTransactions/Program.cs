using Api.AccountTransactions.Dtos;
using Api.AccountTransactions.Filter;
using Api.AccountTransactions.Models;
using Api.AccountTransactions.Services;
using Api.AccountTransactions.Swagger;
using Api.AccountTransactions.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


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
