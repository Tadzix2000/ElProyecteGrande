using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPGDomain
{
    public class Work : IVerifyNullables
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength (32)]
        public string Language { get; set; }
        [MaxLength(32)]
        public string CoverFile { get; set; }
        [MaxLength(32)]
        public string WorkFile { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PublicationDate { get; set; }
        public Work? OriginalWork { get; set; }
        public Author Author { get; set; }
        public double GetAverageNote(List<int> noteValue)
        {
            return noteValue.Average(x => x);
        }
        public double GetForTopChart(List<int> notes, bool best)
        {
            var score = best? GetAverageNote(notes) * notes.Count() : (11 * GetAverageNote(notes)) * notes.Count;
            return score;
        }
        public bool VerifyNullables() =>
            Name != null &&
            Description != null &&
            Language != null &&
            CoverFile != null &&
            WorkFile != null &&
            ReleaseDate != null &&
            PublicationDate != null &&
            Author != null;
    }
}
