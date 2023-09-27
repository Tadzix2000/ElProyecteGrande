using EPGDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.DTOs.CreateUpdate
{
    public class Comment4Create
    {
        public int ReviewId { get; set; }
        //public int UserId { get; set; }
        public int OriginalCommentId { get; set; }
        public DateTime PublicationDate { get; set; }
        [MaxLength(2048)]
        public string Body { get; set; }

    }
}
