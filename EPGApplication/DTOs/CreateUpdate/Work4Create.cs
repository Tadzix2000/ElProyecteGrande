using EPGDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.DTOs.CreateUpdate
{
    public class Work4Create
    {
        public string Name { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string Language { get; set; }
        [MaxLength(32)]
        public string CoverFile { get; set; }
        [MaxLength(32)]
        public string WorkFile { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public int OriginalWorkId { get; set; }
        public int AuthorId { get; set; }
    }
}
