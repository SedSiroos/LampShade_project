namespace _01_LampShadeQuery.Contracts.CommentQueryModels
{
    public class CommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public long ParentId { get; set; }
        public string ParentName { get; set; }
        public string CreationDate { get; set; }
    }
}
