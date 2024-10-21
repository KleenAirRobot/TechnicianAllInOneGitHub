using System.Diagnostics;
using Microsoft.Maui.Devices;
using TechnicianAllInOne.Data;
using TechnicianAllInOne.Models;
using System.Collections.Generic;
using TechnicianAllInOne.Enc;
namespace TechnicianAllInOne;

public partial class SignUp : ContentPage
{
    private readonly LocalDBService _dbService;

    public SignUp(LocalDBService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
    }

    private async void TapGestureRecognizer_Tapped_For_MainPage(object sender, EventArgs e)
    {
        BtnSignIn.TextColor = Colors.Red;
        await Task.Delay(100);
        BtnSignIn.TextColor = Colors.DarkBlue;
        await Task.Delay(100);

        //Application.Current.MainPage = App.Services.GetService<MainPage>();
        await Shell.Current.GoToAsync("//MainPage");

        FirstName.Text = "";
        LastName.Text = "";
        UserName.Text = "";
        //Question.SelectedIndex = -1;
        //Answer.Text = "";
        Password.Text = "";
        Language.SelectedIndex = -1;
    }

    private async void SignUpClicked(object sender, EventArgs e)
    {
        ListView listView = new ListView();
        BtnSignUp.BackgroundColor = Colors.Blue;

        if (UserName.Text != null)
        {   
            if (SqlDataSpecialties.UserExists(UserName.Text) != true)
            {
                if (FirstName.Text != null)
                {
                    if (LastName.Text != null)
                    {
                        //if (Question.SelectedIndex != -1)
                        //{
                        //    if (Answer.Text != null)
                        //    {      
                                if (Password.Text != null)
                                {
                                    if (Language.SelectedIndex != -1)
                                    {
                                        //string AnswerEnc = "";
                                        string PassEnc = "";
                                        //AnswerEnc = Encryption.enterZebes(Answer.Text);
                                        PassEnc = Encryption.enterZebes(Password.Text);

                                        await _dbService.CreateUsers(new Users
                                        {
                                            V2_UserName = UserName.Text,
                                            V2_FirstName = FirstName.Text,
                                            V2_LastName = LastName.Text,
                                            //V2_Question = Question.Items[Question.SelectedIndex],
                                            //V2_Answer = AnswerEnc,
                                            V2_Password = PassEnc,
                                            V2_Role = "Technician",
                                            V2_Language = Language.Items[Language.SelectedIndex],
                                        });

                                        await DisplayAlert("Success", "Your Information Has Been Successfully Added", "OK");

                                        BtnSignUp.BackgroundColor = Colors.DarkBlue;

                                        //Application.Current.MainPage = App.Services.GetService<MainPage>();
                                        await Shell.Current.GoToAsync("//MainPage");
                                        FirstName.Text = "";
                                        LastName.Text = "";
                                        UserName.Text = "";
                                        //Question.SelectedIndex = -1;
                                        //Answer.Text = "";
                                        Password.Text = "";
                                        Language.SelectedIndex = -1;
                                    }
                                    else
                                    {
                                        await DisplayAlert("No Language Selected", "Please Set Your Language", "OK");
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Password Blank", "Please Ensure All Fields Are Completed", "OK");
                                }
                        //    }
                        //    else
                        //    {
                        //        await DisplayAlert("Security Answer Blank", "Please Ensure All Fields Are Completed", "OK");
                        //    }
                        //}
                        //else
                        //{
                        //    await DisplayAlert("Security Question Blank", "Please Ensure All Fields Are Completed", "OK");
                        //}
                    }
                    else
                    {
                        await DisplayAlert("LastName Blank", "Please Ensure All Fields Are Completed", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("FirstName Blank", "Please Ensure All Fields Are Completed", "OK");
                }
            }
            else
            {
                await DisplayAlert("UserName Taken", "This Username is already taken, please select another", "OK");
            }
        }
        else
        {
            await DisplayAlert("UserName Blank", "Please Ensure All Fields Are Completed", "OK");
        }

    }



    private async void EyeClicked(object sender, EventArgs e)
    {
        if (ImgBtnEye.Source.ToString() == "File: closed.png")
        {
            ImgBtnEye.Source = ImageSource.FromFile("eye.png");
            Password.IsPassword = false;
        }
        else
        {
            ImgBtnEye.Source = ImageSource.FromFile("closed.png");
            Password.IsPassword = true;
        }
    }

}