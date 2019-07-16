using Amaury.Abstractions.Bus;
using Amaury.Abstractions.Stores;
using Amaury.Bus;
using Amaury.Sample.MediatR.Domain.Contracts.Repositories;
using Amaury.Sample.MediatR.Domain.Entities;
using Amaury.Sample.MediatR.Infrastructure.Repositories;
using Amaury.Store.DynamoDb;
using Amazon;
using Amazon.DynamoDBv2;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Amaury.Sample.MediatR
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            
            services.AddScoped<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient("", "", RegionEndpoint.USWest2));

            services.AddScoped<ICelebrityEventStore<Customer>, DynamoDbEventStore<Customer>>();
            services.AddScoped<ICelebrityEventsBus<Customer>, CelebrityEventsBus<Customer>>();

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
