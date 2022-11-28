using Api.AccountTransactions.Filter;
using Api.AccountTransactions.Models;
using Api.AccountTransactions.Services;
using Api.AccountTransactions.Swagger;
using Api.AccountTransactions.Validator;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using Api.AccountTransactions.Dtos;

namespace Api.AccountTransactions
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TransactionDbContext>(options => options.UseInMemoryDatabase("Transactions"));

            services
                .AddMvc(options =>
                    options.Filters.Add(typeof(ValidationFilterAttribute))
                    )
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(options =>
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

            services.AddControllers(options =>
                    options.Filters.Add(new HttpResponseExceptionFilter()))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(Startup)));

            services.AddSingleton<IValidator<Customer>, CustomerValidator>()
                .AddSingleton<IValidator<Transaction>, TransactionValidator>()
                .AddScoped<ITransactionService, TransactionService>()
                .AddScoped<ValidationFilterAttribute>();

            // Authentication
            services.AddAuthentication("Basic");
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
