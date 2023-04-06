using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;
using System.Configuration;
using Video_Share_Library_Web_Application.Data;
using Video_Share_Library_Web_Application.Models;
using Microsoft.AspNetCore.Authorization;

namespace Video_Share_Library_Web_Application.Controllers
{
    [Authorize]
    public class AddVideoController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public AddVideoController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" +
                    Path.GetRandomFileName().Substring(0,4) + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var video = new VideoInfo
            {
                VideoName = file.FileName,
                VideoPath = "/videos/" + fileName,
                UploaderName = User.Identity.Name
            };

            _dbcontext.VideoInfo.Add(video);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
