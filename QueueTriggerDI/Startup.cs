using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueTriggerDI.Context.Entities;
using QueueTriggerDI.Context.Mapping;
using QueueTriggerDI.Context.Repositories;
using QueueTriggerDI.Context.Services;
using QueueTriggerDI.Storage;
using QueueTriggerDI.Storage.Services;
using System.IO;

[assembly: FunctionsStartup(typeof(QueueTriggerDI.Startup))]
namespace QueueTriggerDI
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IBlobClientService, BlobClientService>();
            builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();
            builder.Services.AddTransient<IBookService, BookService>();

            builder.Services.AddSingleton<IDbConnectionService, DbConnectionService>();
            builder.Services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>));

            builder.Services.AddTransient<IBookRepository, BookRepository>();
            builder.Services.AddTransient<IBookContentRepository, BookContentRepository>();

            builder.Services.AddLogging();

            FunctionsHostBuilderContext context = builder.GetContext();

            builder.Services.AddSingleton<IConfiguration>(
                    new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
                        .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                        .AddEnvironmentVariables()
                        .Build()
                );

            string sectionName = BlobServiceSettings.ServiceSettings;
            builder.Services.AddOptions<BlobServiceSettings>()
                .Configure<IConfiguration>((options, configuration) =>
                {
                    configuration.GetSection(sectionName).Bind(options);
                });

            builder.Services.AddSingleton(
                new MapperConfiguration(config => { config.AddProfile(new MappingProfile()); })
                .CreateMapper());
        }
    }
}
