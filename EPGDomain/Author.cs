using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDomain
{
    public class Author : IVerifyNullables
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(32)]
        public string ProfileImageFile { get; set; }
        [MaxLength(32)]
        public string Country { get; set; }
        [MaxLength(1024)]
        public string FurtherLinks { get; set; }
        public DateTime CreationDate { get; set; }
        //public ServiceUser ServiceUser { get; set; }
        //[ForeignKey(nameof(ServiceUser))]
        //public string ServiceUserId { get; set; }

        public bool VerifyNullables() =>
            Name != null &&
            Description != null &&
            FurtherLinks != null &&
            Country != null &&
            CreationDate != null &&
            ProfileImageFile != null;
        
    }
}
