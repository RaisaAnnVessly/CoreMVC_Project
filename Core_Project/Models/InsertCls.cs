using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core_Project.Models
{
    public class InsertCls
    {
        public int uid { set; get; }
        public string? uname { set; get; }
        public string? uemail { set; get; }
        public string? uphone { set; get; }
        public string? uaddr { set; get; }
        public string? username { set; get; }
        public string? password { set; get; }
    }
    public class AdminCls
    {
        public int id { set; get; }
        public string? name { set; get; }
        public string? email { set; get; }
        public string? phone { set; get; }
        public string? username { set; get; }
        public string? password { set; get; }
    }
    public class MechanicCls
    {
        public int id { set; get; }
        public string? name { set; get; }
        public string? phone { set; get; }
        public string? spec { set; get; }
        public string? exp { set; get; }
    }

    public class ServiceCls
    {
        public int bid { set; get; }
        public int uid { set; get; }
        public string? uname { set; get; }
        public int mid { set; get; }   
        public System.DateOnly bookingdate { set; get; }
        public string? vehicleno { set; get; }
        public string? vehiclemodel { set; get; }
        public string? servicetype {  set; get; }
        [DataType(DataType.Date)]
        public DateTime preferredDate { set; get; }
        public string? documents {  set; get; }
        public string? status {  set; get; }
        public string? mechanicname { get; set; }

    }

}
