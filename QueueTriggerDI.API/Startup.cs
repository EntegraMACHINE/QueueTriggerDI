using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QueueTriggerDI.Context.Mapping;
using QueueTriggerDI.Context.Repositories;
using QueueTriggerDI.Context.Services;
using QueueTriggerDI.Cosmos;
using QueueTriggerDI.Cosmos.Repositories;
using QueueTriggerDI.Cosmos.Services;
using QueueTriggerDI.Queues;
using QueueTriggerDI.Queues.Services;
using QueueTriggerDI.Tables;
using QueueTriggerDI.Tables.Mapping;
using QueueTriggerDI.Tables.Repositories;
using QueueTriggerDI.Tables.Services;

namespace QueueTriggerDI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IQueueClientService, QueueClientService>();
            services.AddTransient<IQueueStorageService, QueueStorageService>();

            services.AddTransient<ITableService, TableService>();

            services.AddTransient<ITableClientService, TableClientService>();

            services.AddTransient(typeof(ITableEntityService<>), typeof(TableEntityService<>));
            services.AddTransient(typeof(ITableEntityRepository<>), typeof(TableEntityRepository<>));

            services.AddTransient<ICosmosDatabaseService, CosmosDatabaseService>();
            services.AddTransient(typeof(ICosmosItemService<>), typeof(CosmosItemService<>));
            services.AddTransient(typeof(ICosmosItemRepository<>), typeof(CosmosItemRepository<>));

            services.AddTransient<IBookService, BookService>();

            services.AddSingleton<IDbConnectionService, DbConnectionService>();
            services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>));

            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookContentRepository, BookContentRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QueueTriggerDI.API", Version = "v1" });
            });

            services.AddLogging();

            services.AddSingleton(
                new MapperConfiguration(config =>
                {
                    config.AddProfile(new TableMappingProfile());
                    config.AddProfile(new MappingProfile());
                })
                .CreateMapper());

            services.Configure<TableServiceSettings>(Configuration.GetSection(TableServiceSettings.SettingsSectionName));
            services.Configure<QueueServiceSettings>(Configuration.GetSection(QueueServiceSettings.SettingsSectionName));
            services.Configure<CosmosServiceSettings>(Configuration.GetSection(CosmosServiceSettings.SettingsSectionName));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QueueTriggerDI.API v1"));
            }

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
