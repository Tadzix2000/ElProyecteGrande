using EPGDomain;

namespace EPGApplication.DTOs.Read
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public int NoteNumber { get; set; }
        public Work Work { get; set; }
        //public ServiceUser Owner { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
