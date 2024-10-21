using System.Diagnostics;
using Microsoft.Maui.Devices;
using TechnicianAllInOne.Data;
using TechnicianAllInOne.Models;
namespace TechnicianAllInOne;

public partial class ChangedServicePage : ContentPage
{
    private readonly LocalDBService _dbService;
    private int _editServiceId;

    public ChangedServicePage(LocalDBService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        Task.Run(async () => listView.ItemsSource = await _dbService.GetChangeById(UserInfo.id));

        if (UserInfo.language == "Espanol")
        {
            HeaderText.Text = "Informe de Servicio Omitido";
            CustomerEntry.Placeholder = "Nombre del Cliente";
            AddressEntry.Placeholder = "Direccion de Servicio";
            ReasonEntry.Placeholder = "Razón por la Cual";
            BtnSubmit.Text = "Entregar";
            lblReturn.Text = "Volver al menú";
            lblEntries.Text = "V Entradas Anteriores V";
        }
        else
        {
            HeaderText.Text = "Missed Service Report";
            CustomerEntry.Placeholder = "Customer Name";
            AddressEntry.Placeholder = "Address of Service";
            ReasonEntry.Placeholder = "Reason";
            BtnSubmit.Text = "Submit";
            lblReturn.Text = "Return to Menu";
            lblEntries.Text = "V Past Entries V";
        }

        TechName.Text = UserInfo.name;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            Task.Run(async () => listView.ItemsSource = await _dbService.GetChangeById(UserInfo.id));
        }
        catch
        {

        }

        if (UserInfo.language == "Espanol")
        {
            HeaderText.Text = "Informe de Servicio Omitido";
            CustomerEntry.Placeholder = "Nombre del Cliente";
            AddressEntry.Placeholder = "Direccion de Servicio";
            ReasonEntry.Placeholder = "Razón por la Cual";
            BtnSubmit.Text = "Entregar";
            lblReturn.Text = "Volver al menú";
            lblEntries.Text = "Entradas Anteriores";
            FilterAddition.Text = "(Tamaño y Estilo del Filtro)";
            FilterALocation.Text = "(Ubicación del filtro)";
            FilterSubtraction.Text = "(Tamaño y Estilo del Filtro)";
            FilterSLocation.Text = "(Ubicación del filtro)";
            Frames.Text = "Marcos";
            Wire.Text = "Cable Metalicos";
            From.Text = "(De Que?)";
            To.Text = "(a Que?)";

            lblAdd.Text = "Filtros para Agregaron";
            lblSub.Text = "Filtros para Restar";
            lblFreq.Text = "Cambio de Frecuencia";

        }
        else
        {
            HeaderText.Text = "Missed Service Report";
            CustomerEntry.Placeholder = "Customer Name";
            AddressEntry.Placeholder = "Address of Service";
            ReasonEntry.Placeholder = "Reason";
            BtnSubmit.Text = "Submit";
            lblReturn.Text = "Return to Menu";
            lblEntries.Text = "Your Past Entries";
            FilterAddition.Text = "(Filter Size and Style)";
            FilterALocation.Text = "(Location on Worksheets)";
            FilterSubtraction.Text = "(Filter Size and Style)";
            FilterSLocation.Text = "(Location on Worksheets)";
            Frames.Text = "Frames";
            Wire.Text = "Wire";
            From.Text = "(From?)";
            To.Text = "(To?)";

            lblAdd.Text = "Filters to be Added";
            lblSub.Text = "Filters to be Subtracted";
            lblFreq.Text = "Change in Frequency";

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
                if (ReasonEntry.Text != null)
                {
                    try
                    {
                        if (_editServiceId == 0)
                        {
                            await _dbService.CreateChange(new ChangeReport
                            {
                                //Ideally!!! the user id will be set by a variable that is 
                                //contained in a role class for each user, to determine who is logged in.
                                V2_UserId = UserInfo.id,
                                V2_Date = DateEntry.Date,
                                V2_CustomerName = CustomerEntry.Text,
                                V2_Address = AddressEntry.Text,
                                V2_Reason = ReasonEntry.Text,
                                V2_Addition_Size = FilterAdditionEntry.Text,
                                V2_Addition_Location = FilterALocationEntry.Text,
                                V2_Subtraction_Size = FilterSubtractionEntry.Text,
                                V2_Subtraction_Location = FilterSLocationEntry.Text,
                                V2_Frames = FramesEntry.Text,
                                V2_Wires = WireEntry.Text,
                                V2_From_Fequency = FromEntry.Text,
                                V2_To_Fequency = ToEntry.Text,

                            });
                            i = 1;
                        }
                        else
                        {
                            await _dbService.UpdateChange(new ChangeReport
                            {
                                V2_Id = _editServiceId,
                                V2_UserId = UserInfo.id,
                                V2_Date = DateEntry.Date,
                                V2_CustomerName = CustomerEntry.Text,
                                V2_Address = AddressEntry.Text,
                                V2_Reason = ReasonEntry.Text,
                                V2_Addition_Size = FilterAdditionEntry.Text,
                                V2_Addition_Location = FilterALocationEntry.Text,
                                V2_Subtraction_Size = FilterSubtractionEntry.Text,
                                V2_Subtraction_Location = FilterSLocationEntry.Text,
                                V2_Frames = FramesEntry.Text,
                                V2_Wires = WireEntry.Text,
                                V2_From_Fequency = FromEntry.Text,
                                V2_To_Fequency = ToEntry.Text,
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
            ReasonEntry.Text = null;
            FromEntry.Text = null;
            ToEntry.Text = null;
            FilterAdditionEntry.Text = null;
            FilterALocation.Text = null;
            FilterSubtractionEntry.Text = null;
            FilterSLocation.Text = null;
            FramesEntry.Text = null;
            WireEntry.Text = null;

            if (UserInfo.language == "Espanol")
            {
                await DisplayAlert("Exito!!!", "La Información Ha Sido Publicada", "OK");
            }
            else
            {
                await DisplayAlert("Success!!!", "The Information Has Been Posted!", "OK");
            }
            listView.ItemsSource = await _dbService.GetChangeById(UserInfo.id);
            //await _dbService.DeleteService(MissedService);
        }

    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (UserInfo.language == "Espanol")
        {
            var change = (ChangeReport)e.Item;
            var action = await DisplayActionSheet("Acción", "Cancelar", null, "Editar", "Borrar");

            switch (action)
            {
                case "Editar":

                    _editServiceId = change.V2_UserId;
                    DateEntry.Date = change.V2_Date;
                    CustomerEntry.Text = change.V2_CustomerName;
                    AddressEntry.Text = change.V2_Address;
                    ReasonEntry.Text = change.V2_Reason;
                    FilterAdditionEntry.Text = change.V2_Addition_Size;
                    FilterALocationEntry.Text = change.V2_Addition_Location;
                    FilterSubtractionEntry.Text = change.V2_Subtraction_Size;
                    FilterSLocationEntry.Text = change.V2_Subtraction_Location;
                    FramesEntry.Text = change.V2_Frames;
                    WireEntry.Text = change.V2_Wires;
                    FromEntry.Text = change.V2_From_Fequency;
                    ToEntry.Text = change.V2_To_Fequency;

                    CustomerEntry.Focus();

                    break;
                case "Borrar":
                    await _dbService.DeleteChange(change);
                    listView.ItemsSource = await _dbService.GetChangeById(UserInfo.id);
                    break;
            }
        }
        else
        {
            var change = (ChangeReport)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editServiceId = change.V2_UserId;
                    DateEntry.Date = change.V2_Date;
                    CustomerEntry.Text = change.V2_CustomerName;
                    AddressEntry.Text = change.V2_Address;
                    ReasonEntry.Text = change.V2_Reason;
                    FilterAdditionEntry.Text = change.V2_Addition_Size;
                    FilterALocationEntry.Text = change.V2_Addition_Location;
                    FilterSubtractionEntry.Text = change.V2_Subtraction_Size;
                    FilterSLocationEntry.Text = change.V2_Subtraction_Location;
                    FramesEntry.Text = change.V2_Frames;
                    WireEntry.Text = change.V2_Wires;
                    FromEntry.Text = change.V2_From_Fequency;
                    ToEntry.Text = change.V2_To_Fequency;
                    CustomerEntry.Focus();

                    break;
                case "Delete":
                    await _dbService.DeleteChange(change);
                    listView.ItemsSource = await _dbService.GetChangeById(UserInfo.id);
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
        ReasonEntry.Text = null;

        FilterAdditionEntry.Text = null;
        FilterALocationEntry.Text = null;
        FilterSubtractionEntry.Text = null;
        FilterSLocationEntry.Text = null;
        FramesEntry.Text = null;
        WireEntry.Text = null;
        FromEntry.Text = null;
        ToEntry.Text = null;

        lblReturn.TextColor = Colors.DarkBlue;
        await Task.Delay(100);

        await Shell.Current.GoToAsync("//TechnicianView");
    }
}