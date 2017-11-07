using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using EMDB.Models;
using EMDB;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
          return View();
        }

        [HttpPost("/result")]
        public ActionResult Result()
        {
          Dictionary<string, List<Movie>> model = new Dictionary<string, List<Movie>>();
          List<Movie> resultMovie = Movie.FindTitle(Request.Form["inputTitle"]);
          List<Movie> resultGenre = Movie.FindGenre(Request.Form["inputGenre"]);
          model.Add("Title",resultMovie);
          model.Add("Genre",resultGenre);
          return View(model);
        }

        [HttpGet("/Login")]
        public ActionResult LoginPage()
        {
          return View();
        }

        [HttpGet("/Create")]
        public ActionResult CreateAccount()
        {
          return View();
        }

        [HttpPost("/Access")]
        public ActionResult AccessPage()
        {
          Users newUser = new Users(Request.Form["inputName"],Request.Form["inputUser"],Request.Form["inputPass"]);
          if(newUser.IsNewUsers())
          {
            newUser.Save();
          }

          return View("index");
        }

        [HttpPost("/Access2")]
        public ActionResult LoginResult()
        {
          Users foundUser = Users.FindUser(Request.Form["inputUser"], Request.Form["inputPass"]);
          if(foundUser.GetUsername() == "" )
          {
            return View("Wrong");
          }
          else
          {
            return View("Correct", foundUser);
          }
        }

    }

}
