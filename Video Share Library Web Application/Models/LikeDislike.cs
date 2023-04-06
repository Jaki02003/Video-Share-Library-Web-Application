namespace Video_Share_Library_Web_Application.Models
{
    public class LikeDislike
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }

        public virtual VideoInfo Video { get; set; }
    }

}
