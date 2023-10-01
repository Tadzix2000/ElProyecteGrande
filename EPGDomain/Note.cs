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
        public DateTime NoteDate { get; set; }
        public bool VerifyNullables() =>
            Work != null &&
            NoteDate != null &&
            NoteNumber <= 10 &&
            NoteNumber > 0;
    }
}
