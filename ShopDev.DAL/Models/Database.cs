using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using ShopDev.DAL.Migrations;
using ShopDev.DAL.Repositories;
using System;

namespace ShopDev.DAL.Models;

public class Database
{
    public string ConnString { get; }

    public Database(string connString)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        DapperExtensions.DapperExtensions.SqlDialect
            = new DapperExtensions.Sql.MySqlDialect();
        DapperExtensions.DapperExtensions.DefaultMapper
            = typeof(DapperExtensions.Mapper.PluralizedAutoClassMapper<>);
        DapperExtensions.DapperAsyncExtensions.SqlDialect
            = new DapperExtensions.Sql.MySqlDialect();
        DapperExtensions.DapperAsyncExtensions.DefaultMapper
            = typeof(DapperExtensions.Mapper.PluralizedAutoClassMapper<>);

        ConnString = connString;
    }

    public void ConfigureMigrate(IServiceCollection sc)
    {
        sc.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString(ConnString)
                .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    public void Migrate(IServiceProvider sp)
    {
        // Instantiate the runner
        using var serviceScrope = sp.CreateScope();
        var service = serviceScrope.ServiceProvider;
        var runner = service.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }

    public void ConfigureRepositories(IServiceCollection sc)
    {
        sc.AddSingleton(new LogRepository(this));
        sc.AddSingleton(new SettingRepository(this));
        sc.AddSingleton(new UserRepository(this));
    }
}