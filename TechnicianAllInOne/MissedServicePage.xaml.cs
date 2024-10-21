using System.Diagnostics;
using Microsoft.Maui.Devices;
using TechnicianAllInOne.Data;
using TechnicianAllInOne.Models;
namespace TechnicianAllInOne;

public partial class MissedServicePage : ContentPage
{
    private readonly LocalDBService _dbService;
    private int _editServiceId;

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            Task.Run(async () => listView.ItemsSource = await _dbService.GetServiceById(UserInfo.id));
        }
        catch
        {

        }

        if (UserInfo.language == "Espanol")
        {
            HeaderText.Text = "Informe de Servicio Omitido";
            CustomerEntry.Placeholder = "Nombre del Cliente";
            AddressEntry.Placeholder = "Direccion de Servicio";
            InvoiceEntry.Placeholder = "número de servicio único";
            ReasonEntry.Placeholder = "Razón por la Cual";
            BtnSubmit.Text = "Entregar";
            lblReturn.Text = "Volver al menú";
            lblEntries.Text = "Entradas Anteriores";
        }
        else
        {
            HeaderText.Text = "Missed Service Report";
            CustomerEntry.Placeholder = "Customer Name";
            AddressEntry.Placeholder = "Address of Service";
            InvoiceEntry.Placeholder = "Invoice Number";
            ReasonEntry.Placeholder = "Reason";
            BtnSubmit.Text = "Submit";
            lblReturn.Text = "Return to Menu";
            lblEntries.Text = "Your Past Entries";
        }

        TechName.Text = UserInfo.name;

    }

    public MissedServicePage(LocalDBService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => listView.ItemsSource = await _dbService.GetServiceById(UserInfo.id));

        if (UserInfo.language == "Espanol")
        {
            HeaderText.Text = "Informe de Servicio Omitido";
            CustomerEntry.Placeholder = "Nombre del Cliente";
            AddressEntry.Placeholder = "Direccion de Servicio";
            InvoiceEntry.Placeholder = "número de servicio único";
            ReasonEntry.Placeholder = "Razón por la Cual";
            BtnSubmit.Text = "Entregar";
            lblReturn.Text = "Volver al menú";
            lblEntries.Text = "Entradas Anteriores";
        }
        else
        {
            HeaderText.Text = "Missed Service Report";
            CustomerEntry.Placeholder = "Customer Name";
            AddressEntry.Placeholder = "Address of Service";
            InvoiceEntry.Placeholder = "Invoice Number";
            ReasonEntry.Placeholder = "Reason";
            BtnSubmit.Text = "Submit";
            lblReturn.Text = "Return to Menu";
            lblEntries.Text = "Your Past Entries";
        }

        TechName.Text = UserInfo.name;

    }

    private async void BtnSubmit_Tapped(object sender, EventArgs e)
    {
        int i = 0;
        BtnSubmit.IsInProgress = true;

        if (CustomerEntry.Text != null)
        {
            if (AddressEntry.Text != null)
            {
                if (InvoiceEntry.Text != null)
                {
                    if (ReasonEntry.Text != null)
                    {
                        try
                        {
                            if (_editServiceId == 0)
                            {
                                await _dbService.CreateService(new MissedService
                                {
                                    //Ideally!!! the user id will be set by a variable that is 
                                    //contained in a role class for each user, to determine who is logged in.
                                    V2_UserId = UserInfo.id,
                                    //^this should not stay as 1
                                    V2_Date = DateEntry.Date,
                                    V2_CustomerName = CustomerEntry.Text,
                                    V2_Address = AddressEntry.Text,
                                    V2_Invoice = InvoiceEntry.Text,
                                    V2_Reason = ReasonEntry.Text,

                                });
                                i = 1;
                            }
                            else
                            {
                                await _dbService.UpdateService(new MissedService
                                {
                                    V2_Id = _editServiceId,
                                    V2_UserId = UserInfo.id,
                                    //^this should not stay as 1
                                    V2_Date = DateEntry.Date,
                                    V2_CustomerName = CustomerEntry.Text,
                                    V2_Address = AddressEntry.Text,
                                    V2_Invoice = InvoiceEntry.Text,
                                    V2_Reason = ReasonEntry.Text,
                                });
                                i = 1;
                            }
                            _editServiceId = 0;
                        }
                        catch
                        {
                            if (UserInfo.language == "Espanol")
                            {
                                await DisplayAlert("Fallido", "La Solicitud Ha Fallado, Tendrás Que Enviar de Nuevo!", "OK");

                            }
                            else
                            {
                                await DisplayAlert("Failed", "The Request Failed, You Will Need to Reupload!", "OK");
                            }
                            i = 0;
                        }
                    }
                    else
                    {
                        if (UserInfo.language == "Espanol")
                        {
                            await DisplayAlert("Necesita Una Razón", "El campo de motivo está en blanco", "OK");

                        }
                        else
                        {
                            await DisplayAlert("Reason", "The Reason Must Be Completed", "OK");
                        }
                        i = 0;
                    }
                }
                else
                {
                    if (UserInfo.language == "Espanol")
                    {
                        await DisplayAlert("Necesita Un número de servicio único", "El Campo Del número de servicio único Está En Blanco", "OK");

                    }
                    else
                    {
                        await DisplayAlert("Invoice", "The Invoice Must Be Completed", "OK");
                    }
                    i = 0;
                }

            }
            else
            {
                if (UserInfo.language == "Espanol")
                {
                    await DisplayAlert("Necesita Una Dirección", "El Campo De Dirección Está En Blanco", "OK");

                }
                else
                {
                    await DisplayAlert("Address", "Address Must be Completed", "OK");
                }
                i = 0;
            }
        }
        else
        {
            if (UserInfo.language == "Espanol")
            {
                await DisplayAlert("Necesita Una Nombre De Cliente", "El Campo Nombre Del Cliente Está En Blanco", "OK");

            }
            else
            {
                await DisplayAlert("Customer Name", "Customer Name Must be Completed", "OK");
            }
            i = 0;
        }

        BtnSubmit.IsInProgress = false;
        BtnSubmit.BackgroundColor = Colors.DarkBlue;

        if (i == 1)
        {
            DateEntry.Date = DateTime.Now;
            CustomerEntry.Text = null;
            AddressEntry.Text = null;
            InvoiceEntry.Text = null;
            ReasonEntry.Text = null;
            if (UserInfo.language == "Espanol")
            {
                await DisplayAlert("Exito!!!", "La Información Ha Sido Publicada", "OK");
            }
            else
            {
                await DisplayAlert("Success!!!", "The Information Has Been Posted!", "OK");
            }
            listView.ItemsSource = await _dbService.GetServiceById(UserInfo.id);
            //await _dbService.DeleteService(MissedService);
        }

    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (UserInfo.language == "Espanol")
        {
            var service = (MissedService)e.Item;
            var action = await DisplayActionSheet("Acción", "Cancelar", null, "Editar", "Borrar");

            switch (action)
            {
                case "Editar":
                    
                    _editServiceId = service.V2_Id; 
                    CustomerEntry.Text = service.V2_CustomerName;
                    DateEntry.Date = service.V2_Date;
                    AddressEntry.Text = service.V2_Address;
                    InvoiceEntry.Text = service.V2_Invoice;
                    ReasonEntry.Text = service.V2_Reason;
                    CustomerEntry.Focus();

                    break;
                case "Borrar":
                    await _dbService.DeleteService(service);
                    listView.ItemsSource = await _dbService.GetServiceById(UserInfo.id);
                    break;
            }
        }
        else
        {
            var service = (MissedService)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit": 
                    _editServiceId = service.V2_Id;
                    CustomerEntry.Text = service.V2_CustomerName;
                    DateEntry.Date = service.V2_Date;
                    AddressEntry.Text = service.V2_Address;
                    InvoiceEntry.Text = service.V2_Invoice;
                    ReasonEntry.Text = service.V2_Reason;
                    CustomerEntry.Focus();

                    break;
                case "Delete":
                    await _dbService.DeleteService(service);
                    listView.ItemsSource = await _dbService.GetServiceById(UserInfo.id);
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
        CustomerEntry.Text = null;
        AddressEntry.Text = null;
        InvoiceEntry.Text = null;
        ReasonEntry.Text = null;

        lblReturn.TextColor = Colors.DarkBlue;
        await Task.Delay(100);


        //var page = Navigation.NavigationStack.LastOrDefault();
        //await Shell.Current.GoToAsync(nameof(TechnicianView), false);
        //Navigation.RemovePage(page);

        //Application.Current.MainPage = App.Services.GetService<TechnicianView>();
        await Shell.Current.GoToAsync("//TechnicianView");
    }

}