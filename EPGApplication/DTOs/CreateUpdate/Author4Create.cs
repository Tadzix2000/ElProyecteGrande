using EPGDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.DTOs.CreateUpdate
{
    public class Author4Create
    {
        [MaxLength(32)]
        public string Name { get; set; }
        //public ServiceUser Owner { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string ProfileImageFile { get; set; }
        [MaxLength(32)]
        public string Country { get; set; }
        [MaxLength(1024)]
        public string FurtherLinks { get; set; }
        public DateTime CreationDate { get; set; }
    }

}
