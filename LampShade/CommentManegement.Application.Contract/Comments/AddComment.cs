namespace CommentManagement.Application.Contract.Comments
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public long ParentId { get; set; }
        public long OwnerRecordId { get; set; }
    }
}
