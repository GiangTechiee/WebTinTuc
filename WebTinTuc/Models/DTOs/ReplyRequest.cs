namespace WebTinTuc.Models.DTOs
{
    public class ReplyRequest
    {
        public int NewId { get; set; }
        public int ParentId { get; set; }
        public string Content { get; set; }
    }
}
