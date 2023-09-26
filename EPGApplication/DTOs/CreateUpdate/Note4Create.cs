using EPGDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.DTOs.CreateUpdate
{
    public class Note4Create
    {
        public int NoteNumber { get; set; }
        public int WorkId { get; set; }
        //public int OwnerId { get; set; }
        public DateTime NoteDate { get; set; }

    }
}
