using System.Diagnostics;
using System.Net.WebSockets;
using Camera.MAUI;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using Microsoft.Maui.Devices;
using TechnicianAllInOne.Data;
using TechnicianAllInOne.Enc;
using TechnicianAllInOne.Models;
using System.Net.Http.Json;
using Microsoft.Maui.Controls;
using System.Net.Http;
namespace TechnicianAllInOne;


public partial class ExpenseReportPage : ContentPage
{
    private readonly LocalDBService _dbService;
    private int _editServiceId;
    //string ImageHost = "http://10.0.2.2:5000/api/UploadFile";
    string ImageHost = "http://10.0.2.2:5057/api/UploadFile";



    public ExpenseReportPage(LocalDBService dbService)
    {
        InitializeComponent();
        _dbService = dbService;

        

        //listView.IsVisible = false;

        Task.Run(async () => listView.ItemsSource = await _dbService.GetExpenseById(UserInfo.id));

        //Shell.Current.GoToAsync("//TechnicianView");

        if (UserInfo.language == "Espanol")
        {
            HeaderText.Text = "Informe de gastos\r\n";
            BtnSubmit.Text = "Captura y Entregar";
            lblReturn.Text = "Volver al Menú";
            lblEntries.Text = "Entradas Anteriores";

            lblGasButton.Text = "¿Esta Entrada es un Gasto Relacionado Con el Gas?";

            CityEntry.Placeholder = "Cuidad";
            TotalEntry.Placeholder = "Total Gastado";
            GallonsEntry.Placeholder = "Galones Comprados";
            MileageEntry.Placeholder = "Kilometraje";
            ImageEntry.Text = "Nombre del archivo";
            //Shutter.Text = "Captura";
            //UploadImagebtn.Text = "Subir una Imagen";
        }
        else
        {
            HeaderText.Text = "Expense Report";
            BtnSubmit.Text = "Capture and Submit";
            lblReturn.Text = "Return to Menu";
            lblEntries.Text = "Abrir cámara";

            lblGasButton.Text = "Is this Entry a Gas-Related Expense?";

            CityEntry.Placeholder = "City";
            TotalEntry.Placeholder = "Total Spent";
            GallonsEntry.Placeholder = "Gallons Purchased";
            MileageEntry.Placeholder = "Mileage";
            ImageEntry.Text = "FileName";
            //Shutter.Text = "Capture";
            //UploadImagebtn.Text = "Open Camera";

        }

        TechName.Text = UserInfo.name;

    }



    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //listView.IsVisible = false;

        try
        {
            Task.Run(async () => listView.ItemsSource = await _dbService.GetExpenseById(UserInfo.id));
        }
        catch
        {

        }

        if (UserInfo.language == "Espanol")
        {
            HeaderText.Text = "Informe de Servicio Omitido";
            BtnSubmit.Text = "Captura y Entregar";
            lblReturn.Text = "Volver al Menú";
            lblEntries.Text = "Entradas Anteriores";

            lblGasButton.Text = "¿Esta Entrada es un Gasto Relacionado Con el Gasolina?";

            CityEntry.Placeholder = "Cuidad";
            TotalEntry.Placeholder = "Total gastado";
            GallonsEntry.Placeholder = "Galones comprados";
            MileageEntry.Placeholder = "kilometraje";
            ImageEntry.Text = "Nombre del archivo";
        }
        else
        {
            HeaderText.Text = "Expense Report";
            BtnSubmit.Text = "Capture and Submit";
            lblReturn.Text = "Return to Menu";
            lblEntries.Text = "Your Past Entries";

            lblGasButton.Text = "Is this Entry a Gas-Related Expense?";

            CityEntry.Placeholder = "City";
            TotalEntry.Placeholder = "Total Spent";
            GallonsEntry.Placeholder = "Gallons Purchased";
            MileageEntry.Placeholder = "Mileage";
            ImageEntry.Text = "FileName";
        }

        TechName.Text = UserInfo.name;

    }

    private async void OnGasButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (GasButton.IsChecked == true)
        {
            DescriptionEntry.IsVisible = false;
            GallonsEntry.IsVisible = true;
            MileageEntry.IsVisible = true;

            DescriptionImage.IsVisible = false;
            GallonsImage.IsVisible = true;
            MileageImage.IsVisible = true;

            GallonsEntry.Text = null;
            MileageEntry.Text = null;
        }
        else      
        {
            DescriptionEntry.IsVisible = true;
            GallonsEntry.IsVisible = false;
            MileageEntry.IsVisible = false;

            DescriptionImage.IsVisible = true;
            GallonsImage.IsVisible = false;
            MileageImage.IsVisible = false;

            DescriptionEntry.Text = null;
        }
    }

    private async void BtnSubmit_Tapped(object sender, EventArgs e)
    {

        int i = 0;
        BtnSubmit.IsInProgress = true;

        if (CityEntry.Text != null)
        {
            if (TotalEntry.Text != null)
            {
                if (GasButton.IsChecked == false)
                {
                    if (DescriptionEntry.Text != null)
                    {
                        try
                        {
                            if (_editServiceId == 0)
                            {
                                int timeout = 20000;

                                var uploadFile = await MediaPicker.CapturePhotoAsync();

                                var task = SqlDataSpecialties.UploadImageAPI(ImageHost, UserInfo.id, uploadFile);
                                    if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                                    {
                                        // task completed within timeout
                                        ImageEntry.Text = SqlDataSpecialties.ImageEntry;

                                        int gas;
                                        if (GasButton.IsChecked == true)
                                        {
                                        gas = 1;
                                        }
                                        else
                                        {
                                        gas = 0;
                                        }

                                    await _dbService.CreateExpense(new ExpenseReport
                                    {
                                        V2_UserId = UserInfo.id,
                                        V2_Date = DateEntry.Date,
                                        V2_City = CityEntry.Text,
                                        V2_Image_Name = ImageEntry.Text,
                                        V2_Total = TotalEntry.Text,
                                        V2_Is_Gas = gas,
                                        V2_Description = DescriptionEntry.Text,
                                        V2_Gallons = GallonsEntry.Text,
                                        V2_Mileage = MileageEntry.Text,
                                    });
                                    _editServiceId = 0;

                                    CityEntry.Text = null;
                                    TotalEntry.Text = null;
                                    DescriptionEntry.Text = null;
                                    GallonsEntry.Text = null;
                                    MileageEntry.Text = null;
                                    ImageEntry.Text = "FileName";
                                    CityEntry.Focus();
                                    i = 1;
                                    }
                                    else
                                    {
                                        // timeout logic
                                        await DisplayAlert("Server Not Connecting", "The Server is not connecting, Please Reupload once you have a secure wifi connection", "OK");
                                    i = 0;
                                    }
                            }
                            else
                            {
                                int timeout = 20000;

                                var uploadFile = await MediaPicker.CapturePhotoAsync();

                                var task = SqlDataSpecialties.UploadImageAPI(ImageHost, UserInfo.id, uploadFile);
                                if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                                {
                                    // task completed within timeout
                                    ImageEntry.Text = SqlDataSpecialties.ImageEntry;

                                    int gas;
                                    if (GasButton.IsChecked == true)
                                    {
                                        gas = 1;
                                    }
                                    else
                                    {
                                        gas = 0;
                                    }

                                    await _dbService.UpdateExpense(new ExpenseReport
                                    {
                                        V2_Id = _editServiceId,
                                        V2_UserId = UserInfo.id,
                                        V2_Date = DateEntry.Date,
                                        V2_City = CityEntry.Text,
                                        V2_Image_Name = ImageEntry.Text,
                                        V2_Total = TotalEntry.Text,
                                        V2_Is_Gas = gas,
                                        V2_Description = DescriptionEntry.Text,
                                        V2_Gallons = GallonsEntry.Text,
                                        V2_Mileage = MileageEntry.Text,
                                    });
                                    _editServiceId = 0;

                                    CityEntry.Text = null;
                                    TotalEntry.Text = null;
                                    DescriptionEntry.Text = null;
                                    GallonsEntry.Text = null;
                                    MileageEntry.Text = null;
                                    ImageEntry.Text = "FileName";
                                    CityEntry.Focus();
                                    i = 1;
                                }
                                else
                                {
                                    // timeout logic
                                    await DisplayAlert("Server Not Connecting", "The Server is not connecting, Please Reupload once you have a secure wifi connection", "OK");
                                    i = 0;
                                }
                            }      
                        }
                        catch (Exception ex)
                        {

                            if (UserInfo.language == "Espanol")
                            {
                                await DisplayAlert("Fallido", "La Solicitud Ha Fallado, Tendrás Que Enviar de Nuevo!" + " : " + ex, "OK");
                            }
                            else
                            {
                                await DisplayAlert("Failed", "The Request Failed, You Will Need to Reupload!" + " : " + ex, "OK");
                            }
                            i = 0;
                        }

                        BtnSubmit.IsInProgress = false;
                        BtnSubmit.BackgroundColor = Colors.DarkBlue;

                        if (i == 1)
                        {
                            DateEntry.Date = DateTime.Now;


                            if (UserInfo.language == "Espanol")
                            {
                                await DisplayAlert("Exito!!!", "La Información Ha Sido Publicada", "OK");
                            }
                            else
                            {
                                await DisplayAlert("Success!!!", "The Information Has Been Posted!", "OK");
                            }
                            listView.ItemsSource = await _dbService.GetExpenseById(UserInfo.id);
                            //await _dbService.DeleteService(MissedService);
                        }
                    }
                    else
                    {
                        if (UserInfo.language == "Espanol")
                        {
                            await DisplayAlert("Descripción", "la Descripción está en blanco", "OK");

                        }
                        else
                        {
                            await DisplayAlert("Description", "Description is blank", "OK");
                        }

                        i = 0;
                    }
                }
                else
                {
                    if (GallonsEntry.Text != null)
                    {
                        if (MileageEntry.Text != null)
                        {
                            try
                            {
                                if (_editServiceId == 0)
                                {
                                    int timeout = 20000;

                                    var uploadFile = await MediaPicker.CapturePhotoAsync();

                                    var task = SqlDataSpecialties.UploadImageAPI(ImageHost, UserInfo.id, uploadFile);
                                    if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                                    {
                                        // task completed within timeout
                                        ImageEntry.Text = SqlDataSpecialties.ImageEntry;

                                        int gas;
                                        if (GasButton.IsChecked == true)
                                        {
                                            gas = 1;
                                        }
                                        else
                                        {
                                            gas = 0;
                                        }

                                        await _dbService.CreateExpense(new ExpenseReport
                                    {

                                        V2_UserId = UserInfo.id,
                                        V2_Date = DateEntry.Date,
                                        V2_City = CityEntry.Text,
                                        V2_Image_Name = ImageEntry.Text,
                                        V2_Total = TotalEntry.Text,
                                        V2_Is_Gas = gas,
                                        V2_Description = DescriptionEntry.Text,
                                        V2_Gallons = GallonsEntry.Text,
                                        V2_Mileage = MileageEntry.Text,
                                    });
                                    _editServiceId = 0;

                                    CityEntry.Text = null;
                                    TotalEntry.Text = null;
                                    DescriptionEntry.Text = null;
                                    GallonsEntry.Text = null;
                                    MileageEntry.Text = null;
                                    ImageEntry.Text = "FileName";
                                    CityEntry.Focus();
                                    i = 1;
                                    }
                                    else
                                    {
                                        // timeout logic
                                        await DisplayAlert("Server Not Connecting", "The Server is not connecting, Please Reupload once you have a secure wifi connection", "OK");
                                        i = 0;
                                    }
                                }
                                else
                                {
                                    int timeout = 20000;

                                    var uploadFile = await MediaPicker.CapturePhotoAsync();

                                    var task = SqlDataSpecialties.UploadImageAPI(ImageHost, UserInfo.id, uploadFile);
                                    if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                                    {
                                        // task completed within timeout
                                        ImageEntry.Text = SqlDataSpecialties.ImageEntry;

                                        int gas;
                                        if (GasButton.IsChecked == true)
                                        {
                                            gas = 1;
                                        }
                                        else
                                        {
                                            gas = 0;
                                        }

                                        await _dbService.UpdateExpense(new ExpenseReport
                                        {
                                            V2_Id = _editServiceId,
                                            V2_UserId = UserInfo.id,
                                            V2_Date = DateEntry.Date,
                                            V2_City = CityEntry.Text,
                                            V2_Image_Name = ImageEntry.Text,
                                            V2_Total = TotalEntry.Text,
                                            V2_Is_Gas = gas,
                                            V2_Description = DescriptionEntry.Text,
                                            V2_Gallons = GallonsEntry.Text,
                                            V2_Mileage = MileageEntry.Text,
                                        });
                                        _editServiceId = 0;

                                        CityEntry.Text = null;
                                        TotalEntry.Text = null;
                                        DescriptionEntry.Text = null;
                                        GallonsEntry.Text = null;
                                        MileageEntry.Text = null;
                                        ImageEntry.Text = "FileName";
                                        CityEntry.Focus();
                                        i = 1;
                                    }
                                    else
                                    {
                                        // timeout logic
                                        await DisplayAlert("Server Not Connecting", "The Server is not connecting, Please Reupload once you have a secure wifi connection", "OK");
                                        i = 0;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (UserInfo.language == "Espanol")
                                {
                                    await DisplayAlert("Fallido", "La Solicitud Ha Fallado, Tendrás Que Enviar de Nuevo!" + " : " + ex, "OK");
                                }
                                else
                                {
                                    await DisplayAlert("Failed", "The Request Failed, You Will Need to Reupload!" + " : " + ex, "OK");
                                }
                                i = 0;
                            }

                            BtnSubmit.IsInProgress = false;
                            BtnSubmit.BackgroundColor = Colors.DarkBlue;

                            if (i == 1)
                            {
                                DateEntry.Date = DateTime.Now;

                                if (UserInfo.language == "Espanol")
                                {
                                    await DisplayAlert("Exito!!!", "La Información Ha Sido Publicada", "OK");
                                }
                                else
                                {
                                    await DisplayAlert("Success!!!", "The Information Has Been Posted!", "OK");
                                }
                                listView.ItemsSource = await _dbService.GetExpenseById(UserInfo.id);
                            }
                        }
                        else
                        {
                            if (UserInfo.language == "Espanol")
                            {
                                await DisplayAlert("Kilometraje", "el kilometraje está en blanco", "OK");

                            }
                            else
                            {
                                await DisplayAlert("Mileage", "Mileage is blank", "OK");
                            }
                            i = 0;
                        }
                    }
                    else
                    {
                        if (UserInfo.language == "Espanol")
                        {
                            await DisplayAlert("Galones", "Galones está en blanco", "OK");

                        }
                        else
                        {
                            await DisplayAlert("Gallons", "Gallons is blank", "OK");
                        }
                        i = 0;
                    }

                }

            }
            else
            {
                if (UserInfo.language == "Espanol")
                {
                    await DisplayAlert("Total", "El recibo esta en blanco", "OK");

                }
                else
                {
                    await DisplayAlert("Total", "Total is blank", "OK");
                }

                i = 0;
            }


        }
        else
        {
            if (UserInfo.language == "Espanol")
            {
                await DisplayAlert("Cuidad", "Cuidad esta en blanco", "OK");

            }
            else
            {
                await DisplayAlert("City", "City is blank", "OK");
            }

            i = 0;
        }



        BtnSubmit.IsInProgress = false;
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (UserInfo.language == "Espanol")
        {
            var expense = (ExpenseReport)e.Item;
            var action = await DisplayActionSheet("Acción", "Cancelar", null, "Editar", "Borrar");

            switch (action)
            {
                case "Editar":

                    _editServiceId = expense.V2_Id;
                    DateEntry.Date = expense.V2_Date;
                    CityEntry.Text = expense.V2_City;
                    TotalEntry.Text = expense.V2_Total;
                    ImageEntry.Text = expense.V2_Image_Name;


                    if (expense.V2_Is_Gas == 1)
                    {
                        GasButton.IsChecked = true;
                    }
                    else
                    {
                        GasButton.IsChecked = false;
                    }
                    
                    DescriptionEntry.Text = expense.V2_Description;
                    //need image
                    GallonsEntry.Text = expense.V2_Gallons;
                    MileageEntry.Text = expense.V2_Mileage;
                    CityEntry.Focus();


                    break;
                case "Borrar":
                    await _dbService.DeleteExpense(expense);
                    listView.ItemsSource = await _dbService.GetExpenseById(UserInfo.id);
                    break;
            }
        }
        else
        {
            var expense = (ExpenseReport)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editServiceId = expense.V2_Id;
                    DateEntry.Date = expense.V2_Date;
                    CityEntry.Text = expense.V2_City;
                    TotalEntry.Text = expense.V2_Total;
                    ImageEntry.Text = expense.V2_Image_Name;


                    if (expense.V2_Is_Gas == 1)
                    {
                        GasButton.IsChecked = true;
                    }
                    else
                    {
                        GasButton.IsChecked = false;
                    }

                    DescriptionEntry.Text = expense.V2_Description;
                    
                    //need image
                    GallonsEntry.Text = expense.V2_Gallons;
                    MileageEntry.Text = expense.V2_Mileage;

                    CityEntry.Focus();

                    break;
                case "Delete":
                    await _dbService.DeleteExpense(expense);
                    listView.ItemsSource = await _dbService.GetExpenseById(UserInfo.id);
                    break;
            }
        }
    }

    private async void TapGestureRecognizer_Tapped_For_MissedService(object sender, EventArgs e)
    {
        lblReturn.TextColor = Colors.LightGray;
        await Task.Delay(100);

        _editServiceId = 0;
        DateEntry.Date = DateTime.Now;
        CityEntry.Text = null;
        TotalEntry.Text = null;
        DescriptionEntry.Text = null;
        //need image
        GasButton.IsChecked = false;
        GallonsEntry.Text = null;
        MileageEntry.Text = null;
        ImageEntry.Text = null;
        ImageEntry.Text = "FileName";


        lblReturn.TextColor = Colors.DarkBlue;
        await Task.Delay(100);

        await Shell.Current.GoToAsync("//TechnicianView");
    }

    //private async void TapGestureRecognizer_Tapped_For_Previous(object sender, EventArgs e)
    //{
    //    lblEntries.TextColor = Colors.LightGray;
    //    await Task.Delay(100);
    //    lblReturn.TextColor = Colors.DarkBlue;
    //    await Task.Delay(100);

    //    listView.IsVisible = true;

        
    //}
}