namespace TechnicianAllInOne;

public partial class TechnicianView : ContentPage
{
    protected override async void OnAppearing()
    {

        base.OnAppearing();

        if (UserInfo.language == "Espanol")
        {
            lblTask.Text = "Elige Una Tarea";
            BtnServChange.Text = "Crear un Informe de Cambio de Servicio";
            BtnMissServRep.Text = "Informar de un Servicio Perdido";
            BtnRecExp.Text = "Registrar un Gasto";
            lblReturn.Text = "Finalizar la Sesión";
        }
        else
        {
            lblTask.Text = "Choose a Task";
            BtnServChange.Text = "Create a Service Change Report";
            BtnMissServRep.Text = "Report a Missed Service";
            BtnRecExp.Text = "Record an Expense";
            lblReturn.Text = "Log Out";
        }
    }
    public TechnicianView()
    {
        InitializeComponent();

        if (UserInfo.language == "Espanol")
        {
            lblTask.Text = "Elige Una Tarea";
            BtnServChange.Text = "Crear un Informe de Cambio de Servicio";
            BtnMissServRep.Text = "Informar de un Servicio Perdido";
            BtnRecExp.Text = "Registrar un Gasto";
            lblReturn.Text = "Finalizar la Sesión";
        }
        else
        {
            lblTask.Text = "Choose a Task";
            BtnServChange.Text = "Create a Service Change Report";
            BtnMissServRep.Text = "Report a Missed Service";
            BtnRecExp.Text = "Record an Expense";
            lblReturn.Text = "Log Out";
        }
    }

    private async void ServChange(object sender, EventArgs e)
    {
        BtnServChange.BackgroundColor = Colors.Blue;
        await Task.Delay(100);
        BtnServChange.BackgroundColor = Colors.DarkBlue;
        await Task.Delay(100);

        await Shell.Current.GoToAsync("//ChangedServicePage");
    }
    private async void MissServRep(object sender, EventArgs e)
    {
        BtnMissServRep.BackgroundColor = Colors.Blue;
        await Task.Delay(100);
        BtnMissServRep.BackgroundColor = Colors.DarkBlue;
        await Task.Delay(100);
        //Application.Current.MainPage = App.Services.GetService<MissedServicePage>();
        await Shell.Current.GoToAsync("//MissedServicePage");
    }
    private async void RecExp(object sender, EventArgs e)
    {
        BtnRecExp.BackgroundColor = Colors.Blue;
        await Task.Delay(100);
        BtnRecExp.BackgroundColor = Colors.DarkBlue;
        await Task.Delay(100);

        //await Shell.Current.GoToAsync("//ExpenseReportPage");
        //this rubbish is done to make sure the cameraview loads
        //await Task.Delay(500);
        //await Shell.Current.GoToAsync("//ExpenseReportPage");
        await Task.Delay(500);
        await Shell.Current.GoToAsync("//ExpenseReportPage");

    }
    //private async void AltServRep(object sender, EventArgs e)
    //{
    //    BtnAltServRep.BackgroundColor = Colors.LightGrey;
    //    await Task.Delay(100);
    //    BtnAltServRep.BackgroundColor = Colors.DarkGray;
    //    await Task.Delay(100);
    //}
    //private async void AltExpRep(object sender, EventArgs e)
    //{
    //    BtnAltExpRep.BackgroundColor = Colors.LightGrey;
    //    await Task.Delay(100);
    //    BtnAltExpRep.BackgroundColor = Colors.DarkGray;
    //    await Task.Delay(100);
    //}
    private async void TapGestureRecognizer_Tapped_For_TechnicianView(object sender, EventArgs e)
    {
        lblReturn.TextColor = Colors.LightGray;
        await Task.Delay(100);
        lblReturn.TextColor = Colors.DarkGrey;
        await Task.Delay(100);


        //Application.Current.MainPage = App.Services.GetService<MainPage>();

        //await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        await Shell.Current.GoToAsync("//MainPage");


    }
}