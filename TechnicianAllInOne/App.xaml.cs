namespace TechnicianAllInOne
{
    //public partial class App : Application
    //{
    //    public static IServiceProvider Services;

    //    public App(IServiceProvider services)
    //    {
    //        Services = services;

    //        InitializeComponent();

    //        MainPage = App.Services.GetService<MainPage>();
    //    }
    //}

    //prior code
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
