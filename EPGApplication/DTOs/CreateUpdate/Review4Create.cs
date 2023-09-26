using EPGDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGApplication.DTOs.CreateUpdate
{
    public class Review4Create
    {
        //public int UserId { get; set; }
        public int WorkId { get; set; }
        [MaxLength(32)]
        public string Title { get; set; }
        [MaxLength(8192)]
        public string Body { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
