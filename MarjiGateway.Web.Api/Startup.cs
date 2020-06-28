using FluentValidation.AspNetCore;
using MarjiGateway.Application.RequestHandlers.ProcessPayment;
using MarjiGateway.Web.Api.Extensions;
using MarjiGateway.Web.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;


namespace MarjiGateway.Web.Api
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
            services
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddMediatR(typeof(ProcessPayment))
                .AddServices()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Info
                    {
                        Title = "Marji Gateway API",
                        Version = "v1"
                    });
                    options.OperationFilter<ExamplesOperationFilter>();
                })
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(GlobalHttpExceptionFilter));
                    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(_ => $"Value of {_} is not valid");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<ProcessPayment>();
                });

            UpdateServiceConfiguration(services);
        }

        protected virtual void UpdateServiceConfiguration(IServiceCollection services)
        {
            // this method allows us to selectively update our dependencies to support acceptance testing
            // with the ASP.Net Core test server and, importantly, without having the Startup code
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseHttpsRedirection();
            app.UseMvc();
            if (!env.IsProduction())
            {
                ConfigureSwagger(app);
            }
        }

        public void ConfigureSwagger(IApplicationBuilder app)
        {
            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marji Gateway API");
                });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);
        }
    }
}
