using EPGDataAccess;
using EPGDomain;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
DataInstance Instance = new DataInstance();
var newBook = new Work()
{
    Name = "Biblia 2: Zemsta Jezusa",
    CoverFile = "bible2cover.png",
    WorkFile = "bible.pdf",
    Description = "Lepiej tego nie czytaj",
    Language = "english",
    OriginalWork = null,
    ReleaseDate = DateTime.Now,
    PublicationDate = DateTime.Now
};
Instance.Add(newBook);
Instance.SaveChanges();
