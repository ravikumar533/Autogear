namespace AutogearWeb.Models
{
    public class TblPostCodeModel
    {
        public int PostCodeId { get; set; }
        public int SuburbId { get; set; }
    }

    public class TblSuburbModel
    {
        public int SuburbId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
    }

    public class TblStateModel
    {
        public int StateId { get; set; }
        public string Name { get; set; }
    }

    public class TblPostCodeSuburbModel
    {
        public int PostCodeId { get; set; }
        public string SuburbName { get; set; }
    }

    public class TblAddress
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int PostalCode { get; set; }
        public int SuburbId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }

    public class TblInstructorArea
    {
        public int Id { get; set; }
        public string InstructorId { get; set; }
        public int AreaId { get; set; }
     }
}