using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Video_Share_Library_Web_Application.Data;
using Video_Share_Library_Web_Application.Models;

namespace Video_Share_Library_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger , 
                             ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<VideoInfo> videos = await _context.VideoInfo.ToListAsync();
            return View(videos);
        }

        public async Task<IActionResult> Watch(int id)
        {
            VideoInfo video = await _context.VideoInfo.FirstOrDefaultAsync(v => v.id == id);
            if (video == null)
            {
                return NotFound();
            }

            video.Views++;
            await _context.SaveChangesAsync();
            ViewData["UploaderName"] = video.UploaderName.Substring(0, video.UploaderName.IndexOf("@"));

            return View(video);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(int videoId, string action)
        {
            VideoInfo video = await _context.VideoInfo.FirstOrDefaultAsync(v => v.id == videoId);
            if (video == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            LikeDislike likeDislike = await _context.LikeDislike
                .FirstOrDefaultAsync(ld => ld.VideoId == videoId && ld.UserId == userId);
            bool objectCreatedOrNot = false;
            string userName = User.Identity.Name;

            if (likeDislike == null)
            { 
                objectCreatedOrNot = true;
                likeDislike = new LikeDislike
                {
                    VideoId = videoId,
                    UserId = userId,
                    UserName = userName,
                    Like = false,
                    Dislike = false
                };
            }

            if (action == "like")
            {
                if (!likeDislike.Like)
                {
                    video.Likes++;
                    likeDislike.Like = true;

                    if (likeDislike.Dislike)
                    {
                        video.Dislikes--;
                        likeDislike.Dislike = false;
                    }
                }
            }
            else if (action == "dislike")
            {
                if (!likeDislike.Dislike)
                {
                    video.Dislikes++;
                    likeDislike.Dislike = true;

                    if (likeDislike.Like)
                    {
                        video.Likes--;
                        likeDislike.Like = false;
                    }
                }
            }
            if (objectCreatedOrNot)
            {
                _context.LikeDislike.Add(likeDislike);
            }

            await _context.SaveChangesAsync();

            return Json(new
            {
                videoId = video.id,
                likes = video.Likes,
                dislikes = video.Dislikes
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            VideoInfo video = await _context.VideoInfo.FirstOrDefaultAsync(v => v.id == id);
            if (video == null)
            {
                return NotFound();
            }

            string uploaderName = video.UploaderName;
            uploaderName = uploaderName.Substring(0, uploaderName.IndexOf("@"));

            List<LikeDislike> likeDislikes = await _context.LikeDislike
                .Where(ld => ld.VideoId == id && (ld.Like || ld.Dislike))
                .Select(ld => new LikeDislike
                {
                    UserId = ld.UserId,
                    UserName = ld.UserName.Substring(0, ld.UserName.IndexOf("@")),
                    Like = ld.Like,
                    Dislike = ld.Dislike
                })
                .ToListAsync();

            ViewData["UploaderName"] = uploaderName;
            ViewData["LikeDislikes"] = likeDislikes;

            return View(video);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}