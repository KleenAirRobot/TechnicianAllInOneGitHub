using SQLite;

namespace TechnicianAllInOne.Models
{

    [Table("users_")]
    public class Users
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("v2_id")]
        public int V2_Id { get; set; }
        ////////////////////////////////
        [Column("v2_first_name")]
        public string V2_FirstName { get; set; }
        ////////////////////////////////       
        [Column("v2_last_name")]
        public string V2_LastName { get; set; }
        ////////////////////////////////
        [Unique]
        [Column("v2_user_name")]
        public string V2_UserName { get; set; }
        ////////////////////////////////
        [Column("v2_question")]
        public string V2_Question { get; set; }
        ////////////////////////////////
        [Column("v2_answer")]
        public string V2_Answer { get; set; }
        ////////////////////////////////
        [Column("v2_password")]
        public string V2_Password { get; set; }
        ////////////////////////////////
        [Column("v2_role")]
        public string V2_Role { get; set; }
        ////////////////////////////////
        [Column("v2_language")]
        public string V2_Language { get; set; }
        ////////////////////////////////
        [Column("v2_is_deleted")]
        public int V2_Is_Deleted { get; set; }
    }
}
