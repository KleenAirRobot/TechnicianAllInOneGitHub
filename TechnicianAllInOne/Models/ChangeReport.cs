using SQLite;

namespace TechnicianAllInOne.Models
{
    [Table("change_report_")]
    public class ChangeReport
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("v2_id")]
        public int V2_Id { get; set; }
        ///////////////////////////////////
        [Column("v2_user_id")]
        public int V2_UserId { get; set; }
        ///////////////////////////////////
        [Column("v2_date")]
        public DateTime V2_Date { get; set; }
        ///////////////////////////////////
        [Column("v2_customer_name")]
        public string V2_CustomerName { get; set; }
        ///////////////////////////////////
        [Column("v2_address")]
        public string V2_Address { get; set; }
        ///////////////////////////////////
        [Column("v2_reason")]
        public string V2_Reason { get; set; }
        ///////////////////////////////////Additions
        [Column("v2_addition_size")]
        public string V2_Addition_Size { get; set; }
        ///////////////////////////////////
        [Column("v2_addition_location")]
        public string V2_Addition_Location { get; set; }
        ///////////////////////////////////Subtractions
        [Column("v2_subtraction_size")]
        public string V2_Subtraction_Size { get; set; }
        ///////////////////////////////////
        [Column("v2_subtraction_location")]
        public string V2_Subtraction_Location { get; set; }
        ///////////////////////////////////Frames Needed
        [Column("v2_frames")]
        public string V2_Frames { get; set; }
        ///////////////////////////////////Wires Needed
        [Column("v2_wires")]
        public string V2_Wires { get; set; }
        ///////////////////////////////////Frequency From
        [Column("v2_from_frequency")]
        public string V2_From_Fequency { get; set; }
        ///////////////////////////////////Frequency To
        [Column("v2_to_frequency")]
        public string V2_To_Fequency { get; set; }
        ////////////////////////////////
        [Column("v2_is_deleted")]
        public string V2_Is_Deleted { get; set; }
    }
}

