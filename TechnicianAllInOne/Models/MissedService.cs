using SQLite;

namespace TechnicianAllInOne.Models
{
    [Table("missed_service_")]
    public class MissedService
    {
        private int Sync_Status;

        [PrimaryKey]
        [AutoIncrement]
        [Column("v2_id")]
        public int V2_Id { get; set; }
        ///////////////////////////////////
        [Column("v2_user_id")]
        public int V2_UserId { get; set; }
        ///////////////////////////////////
        [Column("v2_date")]
        public DateTime V2_Date { get; set;}
        ///////////////////////////////////
        [Column("v2_customer_name")]
        public string V2_CustomerName { get; set;}
        ///////////////////////////////////
        [Column("v2_address")]
        public string V2_Address { get; set;}
        ///////////////////////////////////
        [Column("v2_invoice")]
        public string V2_Invoice { get; set;}
        ///////////////////////////////////
        [Column("v2_reason")]
        public string V2_Reason { get; set;}
        ////////////////////////////////
        [Column("v2_is_deleted")]
        public int V2_Is_Deleted { get; set; }    
    }
}
