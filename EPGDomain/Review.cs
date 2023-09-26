using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDomain
{
    public class Review : IVerifyNullables
    {
        public int Id { get; set; }
        //public User? User { get; set; }
        public Work? Work { get; set; }
        [MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(8192)]
        public string Body { get; set; }
        public DateTime ReviewDate { get; set; }
        //public ServiceUser ServiceUser { get; set; }
        //[ForeignKey(nameof(ServiceUser))]
        //public string ServiceUserId { get; set; }
        public bool VerifyNullables() =>
            Work != null &&
            Title != null &&
            Body != null &&
            ReviewDate != null;
    }
}
