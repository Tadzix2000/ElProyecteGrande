using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDomain
{
    public class Note : IVerifyNullables
    {
        public int Id { get; set; }
        public int NoteNumber { get; set; }
        public Work? Work { get; set; }
        //public User? Owner { get; set; }
        public DateTime NoteDate { get; set; }
        //public ServiceUser ServiceUser { get; set; }
        //[ForeignKey(nameof(ServiceUser))]
        //public string ServiceUserId { get; set; }
        public bool VerifyNullables() =>
            Work != null &&
            NoteDate != null &&
            NoteNumber <= 10 &&
            NoteNumber > 0;
    }
}
