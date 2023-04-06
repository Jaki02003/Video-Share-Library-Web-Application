namespace Video_Share_Library_Web_Application.Models
{
    public class VideoInfo
    {
        public int id { get; set; }

        public string VideoName { get; set; }
        public string VideoPath { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Views { get; set; }
        public string UploaderName { get; set; }
    }
}
