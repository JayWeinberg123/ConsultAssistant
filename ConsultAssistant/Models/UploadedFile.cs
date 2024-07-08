namespace ConsultAssistant.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public DateTime UploadedAt {  get; set; }

    }
}
