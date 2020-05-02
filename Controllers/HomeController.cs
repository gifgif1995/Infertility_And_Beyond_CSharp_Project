using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using infertilityAndBeyondProject.Models;
using Microsoft.AspNetCore.Http;

namespace infertilityAndBeyondProject.Controllers
{
    public class HomeController : Controller
    {

        private myWebsiteContext dbContext;

        public HomeController(myWebsiteContext context)
        {
            dbContext = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index");
        }


        [HttpGet("Dashboard")]

        public IActionResult Dashboard()
        {
            int? userID = HttpContext.Session.GetInt32("userID");
            if(userID == null)
            {
                TempData["error"] = "You must be logged in to view this page";
                return RedirectToAction("Index");
            }

            User pulledUser = dbContext.Users.FirstOrDefault(u => u.UserID == userID);
            ViewBag.User = pulledUser;

            return View("dashboard");
        }

        [HttpPost("login")]
        public IActionResult LoginValidate(LoginRegWrapper LoginRegWrapper)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == LoginRegWrapper.Login.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(LoginRegWrapper.Login, userInDb.Password, LoginRegWrapper.Login.Password);
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View("Index");
                }   
                HttpContext.Session.SetInt32("logged", userInDb.UserID);
                return RedirectToAction("Blog");
            }
            return View("Index");
        }

        [HttpPost("create")]
        public IActionResult CreateNewUser(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email has already taken, Please try another");
                    return View("Registration");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                HttpContext.Session.SetInt32("userID", user.UserID);
                return RedirectToAction("dashboard");
            }
            else
                return View("Registration");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        
        [HttpGet("AboutUs")]
        public ViewResult AboutUs()
        {
            return View("AboutUs");
        }

        [HttpGet("Doctors")]
        public ViewResult Doctors()
        {
            return View("Doctors");
        }
        [HttpGet("Products")]
        public ViewResult Products()
        {
            return View("Products");
        }
        [HttpGet("Discounts")]
        public ViewResult Discounts()
        {
            return View("Discounts");
        }


        [HttpGet]
        [Route ("Blog")]
        public IActionResult Blog () {
            if (HttpContext.Session.GetInt32 ("logged") == null) {
                return View ("Index");
            }
            UserBag ();
            MessageBag();
            CommentBag();
            return View ("Blog");
        }
        public void UserBag () {
            User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserID == HttpContext.Session.GetInt32 ("logged"));
            ViewBag.LoggedUser = loggedUser;
            ViewBag.LoggedUserName = $"{loggedUser.FirstName} {loggedUser.LastName}";
        }

        public void MessageBag() {
            List<Message> allMessages = dbContext.Messages
                .Include(m => m.User)
                .OrderBy(m => m.created_at)
                .ToList();
            allMessages.Reverse();
            ViewBag.allMessages = allMessages;
        }

        public void CommentBag() {
            List<Comment> allComments = dbContext.Comments
                .Include(c => c.User)
                .Include(m => m.Message)
                .ToList();
            ViewBag.AllComments = allComments;
        }

        [HttpPost]
        [Route ("postmessage")]
        public IActionResult PostMessage (Message postedMessage) {
            if (ModelState.IsValid) {
                User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserID == HttpContext.Session.GetInt32 ("logged"));
                postedMessage.UserId = loggedUser.UserID;
                postedMessage.User = loggedUser;
                dbContext.Messages.Add (postedMessage);
                dbContext.SaveChanges ();
                return RedirectToAction ("Blog");
            }
            UserBag ();
            MessageBag();
            CommentBag();
            return View ("Blog");
        }

        [HttpPost]
        [Route ("postcomment/{MessageId}")]
        public IActionResult PostComment (Comment postedComment, int messageID) {
            if (ModelState.IsValid) {
                User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserID == HttpContext.Session.GetInt32 ("logged"));
                Message retrievedMessage = dbContext.Messages.FirstOrDefault(m => m.MessageId == messageID);
                postedComment.UserId = loggedUser.UserID;
                postedComment.User = loggedUser;
                postedComment.MessageId = retrievedMessage.MessageId;
                postedComment.Message = retrievedMessage;
                dbContext.Comments.Add (postedComment);
                dbContext.SaveChanges ();
                return RedirectToAction ("Blog");
            }
            UserBag ();
            MessageBag();
            CommentBag();
            return View ("Blog");
        }

        [HttpGet("Registration")]
        public ViewResult Registration()
        {
            return View("Registration");
        }
    }
}