using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EPGDomain
{
    public class Comment : IVerifyNullables
    {
        public int Id { get; set; }
        //public User? User { get; set; }
        public Review? Review { get; set; }
        public Comment? OriginalComment { get; set; }
        public DateTime PublicationDate { get; set; }
        [MaxLength(2048)]
        public string Body { get; set; }
        //public ServiceUser ServiceUser { get; set; }
        //[ForeignKey(nameof(ServiceUser))]
        //public string ServiceUserId { get; set; }
        public bool VerifyNullables() =>
            Review != null &&
            PublicationDate != null &&
            Body != null;
    }
}
