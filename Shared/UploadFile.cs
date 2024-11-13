using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestHub.Shared
{
    public class UploadFile
    {
        public int Id { get; set; }
        public Ticket? Ticket { get; set; }//allows full ticket object from the UploadFile instance
        public int? TicketId { get; set; }
        public string FileName { get; set; }
        public string TrustedFileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }
    }
}
