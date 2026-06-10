using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using laba9modules.Services;
using laba9modules.ViewModels;
using laba9modules.Views;

namespace laba9modules
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            /* 
             * НАВИГАЦИОННЫЙ СЕРВИС
             * Lifetime: Singleton (Одиночка)
             * Обоснование: Нам нужен один общий экземпляр сервиса на всё приложение, 
             * чтобы и Оболочка, и другие экраны ссылались на одну и ту же точку управления экранами.
             */
            services.AddSingleton<INavigationService, NavigationService>();

            /* 
             * ГЛАВНОЕ ОКНО И ОБОЛОЧКА (VIEW / VIEWMODEL)
             * Lifetime: Singleton (Одиночка)
             * Обоснование: MainWindow является корневым элементом приложения и живет на протяжении 
             * всего жизненного цикла программы. Повторное создание не требуется.
             */
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();

            /* 
             * ЭКРАН "СПИСОК КОНТАКТОВ" (БИЗНЕС-ЛОГИКА)
             * Lifetime: Transient (Временный / Переходный)
             * Обоснование: Каждый раз при навигации или запросе мы можем получать обновленный 
             * или чистый экземпляр, сбрасывающий или заново вычитывающий состояние, 
             * предотвращая утечки памяти долгоживущих объектов UI. 
             * (Примечание: Если требуется сохранять состояние фильтров/выделения при переключениях меню, 
             * можно изменить на AddSingleton).
             */
            services.AddTransient<ContactsListView>();

            /* 
             * ЭКРАН "О ПРОГРАММЕ"
             * Lifetime: Transient (Временный)
             * Обоснование: Экран статический, не хранит важного изменяемого состояния. 
             * Создается в памяти только тогда, когда пользователь открывает вкладку, и уничтожается сборщиком мусора после.
             */
            services.AddTransient<AboutViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Разрешаем зависимость главного окна через DI-контейнер и отображаем его
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
