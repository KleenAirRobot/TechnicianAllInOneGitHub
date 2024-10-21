using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicianAllInOne.Models;
using Microsoft.Data.Sqlite;
using TechnicianAllInOne.Enc;
using Microsoft.Maui.Graphics;
using ZXing;
using System.Reflection.Metadata;
using System.Net;
using System.Net.Http;
//using Android.Health.Connect.DataTypes.Units;

namespace TechnicianAllInOne.Data
{
    internal class SqlDataSpecialties
    {
        private static string DB_NAME = "demo_local_db,db3";

        public async Task<Stream> ConvertImageSourceToStreamAsync(ImageSource imageSource)
        {
            using var stream = await ((StreamImageSource)imageSource).Stream(CancellationToken.None);
            return stream;
        }

        public static string ImageEntry;

        public static bool UserExists(string user)
        {
            string UserName = "";
            
            using (var connection = new SqliteConnection("Data Source=" + Path.Combine(FileSystem.AppDataDirectory) + "/" + DB_NAME))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT v2_user_name
                FROM users_
                WHERE v2_user_name = $user
                ";
                command.Parameters.AddWithValue("$user", user);
                string man = user;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserName = reader.GetInt32(0).ToString();
                    }
                }
                command.Dispose();
                connection.Close();

                if (UserName == "" || UserName == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool PasswordsMatch(string pass, string user)
        {
            string Password = "";
            
            using (var connection = new SqliteConnection("Data Source=" + Path.Combine(FileSystem.AppDataDirectory) + "/" + DB_NAME))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT v2_password, v2_id
                FROM users_
                WHERE v2_user_name = $user AND v2_password = $password
                ";
                command.Parameters.AddWithValue("$user", user);
                command.Parameters.AddWithValue("$password", Encryption.enterZebes(pass));
                //string man = user;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Password = reader.GetString(0).ToString();
                    }
                }

                command.Dispose();
                connection.Close();

                Password = Encryption.exitZebes(Password);

                if (Password == pass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void SetUserCredentials(string user)
        {
            int ID = 1;
            string role = "";
            string language = "";
            string fName = "";
            string lName = "";
            string FullName = "";

            using (var connection = new SqliteConnection("Data Source=" + Path.Combine(FileSystem.AppDataDirectory) + "/" + DB_NAME))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT v2_id, v2_role, v2_language, v2_first_name, v2_last_name
                FROM users_
                WHERE v2_user_name = $user 
                ";
                command.Parameters.AddWithValue("$user", user);
                //string man = user;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ID = Int32.Parse(reader.GetString(0));
                        role = reader.GetString(1);
                        language = reader.GetString(2);
                        fName = reader.GetString(3);
                        lName = reader.GetString(4);
                    }
                }

                FullName = fName + " " + lName;

                UserInfo.id = ID;
                UserInfo.role = role;
                UserInfo.language = language;
                UserInfo.name = FullName;

                command.Dispose();
                connection.Close();
            }


        }

      
        ///for uploading ImageFiles
        public static async Task UploadImageAPI(string ImageHost, int id, FileResult? uploadFile)
        {
            //var uploadFile = await MediaPicker.CapturePhotoAsync();

            //if (uploadFile is null) return;
            string ids = id.ToString();
            uploadFile.FileName = ids +"_"+ DateTime.Now.ToString("MM-dd-yy-HH-mm-ss") + ".jpeg";
            ImageEntry = uploadFile.FileName;

            var httpContent = new MultipartFormDataContent();
            httpContent.Add(new StreamContent(await uploadFile.OpenReadAsync()),"file", uploadFile.FileName);

            var httpClient = new HttpClient();
            var result = await httpClient.PostAsync(ImageHost, httpContent);
            var response = await result.Content.ReadAsStringAsync();
            await Shell.Current.DisplayAlert("Response From Server", response, "Ok");
        }

        //public static async Task RetrieveImageAPI(string ImageHost, string ImageRef)
        //{
        //    //var httpContent = new MultipartFormDataContent();
        //    //httpContent.Add(new StreamContent("file", ImageRef));

        //    //var httpClient = new HttpClient();
        //    //var result = await httpClient.GetAsync(ImageHost, httpContent);
        //    //var response = await result.Content.ReadAsStringAsync();
        //    //await Shell.Current.DisplayAlert("Response From Server", response, "Ok");
        //}





    }
}
