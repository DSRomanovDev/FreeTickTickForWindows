using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Настройка конфигурации
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");

        var configuration = builder.Build();

        var config = new AppConfig();
        configuration.Bind(config);

        // Настройка DI контейнера
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterInstance(config).As<AppConfig>();
        containerBuilder.RegisterType<LocalSettings>().AsSelf().SingleInstance();
        containerBuilder.RegisterType<UserDao>().AsSelf().SingleInstance();

        var container = containerBuilder.Build();

        // Инициализация UserDao
        UserDao.Initialize(container.Resolve<AppConfig>());

        var localSettings = container.Resolve<LocalSettings>();

        // Пример использования
        Console.WriteLine("Initial IsPro: " + localSettings.IsPro); // Должно быть true из-за ForceProStatus

        localSettings.IsPro = false;
        Console.WriteLine("Modified IsPro: " + localSettings.IsPro); // Все еще должно быть true

        // Проверка через UserDao
        Console.WriteLine("UserDao IsPro: " + UserDao.IsPro()); // Должно быть true
    }
}
