using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestHub.Shared
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Boolean IsValid { get; set; } = true;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Department? Department { get; set; }//nullable(?)Navigation property to Department
        public int DepartmentId { get; set; } //foreign key used to associate Ticket with Department object
        public string EquipmentName { get; set; } = string.Empty;
        public User? User { get; set; }
        public int UserId { get; set; }
        public SiteLocation? SiteLocation { get; set; }
        public int SiteLocationId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public UploadFile? UploadFile { get; set; }
        public int? UploadFileId { get; set; }

        //Updated property getters below
        public string RequestorName => User?.RequestorName ?? "Error GETting RequestorName";
        public string DepartmentName => Department?.DepartmentName ?? "Error GETting DepartmentName";
        public string Name => SiteLocation?.Name ?? "Error GETting Name";
    }
}
