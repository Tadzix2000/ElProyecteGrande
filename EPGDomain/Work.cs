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
        public double GetAverageNote(List<Note> notes)
        {
            return notes.Where(n => n.Work == this).Average(x => x.NoteNumber);
        }
        public double GetForTopChart(List<Note> notes, double? popularityWeight)
        {
            if (popularityWeight == null || popularityWeight > 1 || popularityWeight < 0) popularityWeight = 1;
            var score = GetAverageNote(notes) * (1 + ((double)popularityWeight * notes.Count()));
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
