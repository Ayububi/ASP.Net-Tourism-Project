using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Tour_FP.Models.Domain;
using Tour_FP.Repositories.Abstract;

namespace Tour_FP.Controllers
{
    //Admin can only access these functions
    [Authorize(Roles = "User")]
    public class FourmController : Controller
    {
        private readonly DatabaseContext _dbcontext;
        private readonly IFourmService _fourmService;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;
        public FourmController(DatabaseContext dbcontext, IFourmService fourmService, IFileService fileService, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbcontext;
            _fourmService = fourmService;
            _fileService = fileService;
            _userManager = userManager;
        }
        public IActionResult Posts()
        {
            var data = this._fourmService.ListWithComments();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> AddPost()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(CommunityPostTable model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Get the current logged-in user
            var user = await _userManager.GetUserAsync(User);

            // Set the UserId for the review
            model.UserId = user.Id;
            model.UserName = user.UserName;
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.Images = imageName;
            }
            var result = _fourmService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Posts));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult EditPost(int id)
        {
            var model = _fourmService.GetById(id);

            // Check if the current user is the author of the post
            if (model.UserId != _userManager.GetUserId(User))
            {
                TempData["msg"] = "You are not authorized to edit this post.";
                return RedirectToAction(nameof(Posts));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditPost(CommunityPostTable model, int id)
        {
            model.PostId = id;

            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.Images = imageName;
            }
            var result = _fourmService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Posts));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }



        public IActionResult DeletePost(int id)
        {
            var existingData = _fourmService.GetById(id);
            if (existingData == null)
            {
                return NotFound();
            }
            if (existingData.UserId != _userManager.GetUserId(User))
            {
                TempData["msg"] = "You are not authorized to delete this post.";
                return RedirectToAction(nameof(Posts));
            }
            _fileService.DeleteImage(existingData.Images);
            var result = _fourmService.Delete(id);
            return RedirectToAction(nameof(Posts));
        }

        //Comment function

        [HttpPost]
        public IActionResult AddComment(int postId, string commentContent)
        {
            var post = _fourmService.GetById(postId);

            if (post == null)
            {
                return NotFound();
            }

            var comment = new CommentTable
            {
                PostId = postId,
                Content = commentContent,
                UserId = _userManager.GetUserId(User),
                UserName = _userManager.GetUserName(User),
                CreatedAt = DateTime.Now
            };

            _dbcontext.commentTable.Add(comment);
            _dbcontext.SaveChanges();

            TempData["msg"] = "Comment added successfully.";
            return RedirectToAction("Posts");
        }


    }
}
