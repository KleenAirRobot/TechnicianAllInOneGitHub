using SQLite;
using System.Reflection.Metadata;

namespace TechnicianAllInOne.Models
{
    [Table("expense_report_")]
    public class ExpenseReport
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("v2_id")]
        public int V2_Id { get; set; }
        ///////////////////////////////////
        [Column("v2_user_id")]
        public int V2_UserId { get; set; }
        ///////////////////////////////////
        //[Column("v2_image_id")]
        //public int V2_ImageId { get; set; }
        ///////////////////////////////////
        //[Column("v2_receipt_id")]
        //public string V2_Receipt { get; set; }
        ///////////////////////////////////
        [Column("v2_date")]
        public DateTime V2_Date { get; set; }
        ///////////////////////////////////
        [Column("v2_mileage")]
        public string V2_Mileage { get; set; }
        ///////////////////////////////////
        [Column("v2_gallons")]
        public string V2_Gallons { get; set; }
        ///////////////////////////////////
        [Column("v2_total")]
        public string V2_Total { get; set; }
        ///////////////////////////////////
        [Column("v2_city")]
        public string V2_City { get; set; }
        ///////////////////////////////////
        [Column("v2_description")]
        public string V2_Description { get; set; }
        ///////////////////////////////////
        [Column("v2_image_name")]
        public string V2_Image_Name { get; set; }
        ///////////////////////////////////
        [Column("v2_is_gas")]
        public int V2_Is_Gas { get; set; }
        /// <summary>
        /// ///////////////////////////////
        /// </summary>
        [Column("v2_is_verified")]
        public int V2_Is_Verified { get; set; }
        ////////////////////////////////
        [Column("v2_is_deleted")]
        public int V2_Is_Deleted { get; set; }
    }
}
