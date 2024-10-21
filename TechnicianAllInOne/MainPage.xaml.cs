using System.Drawing;
using TechnicianAllInOne.Data;

namespace TechnicianAllInOne
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
           
                eye.Source = ImageSource.FromFile("eye.png");
                Password.IsPassword = false;
            
                eye.Source = ImageSource.FromFile("closed.png");
                Password.IsPassword = true;
            
        }

        private async void TapGestureRecognizer_Tapped_For_SignUp(object sender, EventArgs e)
        {
            

            //await DisplayAlert("Tessssssst", Path.Combine(FileSystem.AppDataDirectory) + "/demo_local_db,db3", "OK");
            //await DisplayAlert("test", DateTime.Now.ToString("mm:ss"), "OK");

            LblSignUp.TextColor = Colors.Blue;
            await Task.Delay(100);
            LblSignUp.TextColor = Colors.DarkBlue;
            await Task.Delay(100);

            await Shell.Current.GoToAsync("//SignUp");
            //Application.Current.MainPage = App.Services.GetService<SignUp>();

            Username.Text = "";
            Password.Text = "";
        }
        private async void BtnSubmit_Tapped(object sender, EventArgs e)
        {

            //await Shell.Current.GoToAsync("//TechnicianView");

            if (Username.Text != "" && Username.Text != null && Password.Text != "" && Password.Text != null)
            {



                BtnSubmit.IsInProgress = true;
                //BtnSubmit.BackgroundColor = Colors.Blue;
                await Task.Delay(1000);
                //HapticFeedback.Default.Perform(HapticFeedbackType.Click);
                //haptic is not supported 
                if (SqlDataSpecialties.PasswordsMatch(Password.Text, Username.Text) == true)
                {
                    SqlDataSpecialties.SetUserCredentials(Username.Text);
                    //GetLogin Settings
                    if (UserInfo.role == "Technician")
                    {
                        //Application.Current.MainPage = App.Services.GetService<TechnicianView>();
                        await Shell.Current.GoToAsync("//TechnicianView");
                        Username.Text = "";
                        Password.Text = "";
                    }
                    else if (UserInfo.role == "Admin")
                    {
                        //await Shell.Current.GoToAsync("//AdminView");
                        Username.Text = "";
                        Password.Text = "";
                    }
                    else if (UserInfo.role == "Maintenance")
                    {
                        //await Shell.Current.GoToAsync("//MaintenanceView");
                        Username.Text = "";
                        Password.Text = "";
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Login", "The information entered doesn't match our records", "OK");
                }
            }
            else
            {
                await DisplayAlert("Missing Information", "One or more fields are left blank, please complete them and resubmit", "OK");
            }
            BtnSubmit.IsInProgress = false;
        }

        private void EyeClicked(object sender, EventArgs e)
        {
            if (eye.Source.ToString() == "File: closed.png")
            {
                eye.Source = ImageSource.FromFile("eye.png");
                Password.IsPassword = false;
            }
            else
            {
                eye.Source = ImageSource.FromFile("closed.png");
                Password.IsPassword = true;
            }
        }
    }

}
