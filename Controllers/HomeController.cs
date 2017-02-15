using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
// using dojodachi.Models;

namespace dojodachi.Controllers
 {
    //this goes here so we can serialize our objects and store them as a json string 
//         public static class SessionExtensions
//     {
//         public static void SetObjectAsJson(this ISession session, string key, object value)
//         {
//             session.SetString(key, JsonConvert.SerializeObject(value));
//         }
//         public static T GetObjectFromJson<T>(this ISession session, string key)
//         {
//             var value = session.GetString(key);
//             return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
//         }
   
    }
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            
            // List<string> NewList = new List<string>();
            // HttpContext.SetObjectAsJson("TheList", NewList);
            // List<string> Retrieve = HttpContext.GetObjectFromJson<List<string>>("TheList");
           if(HttpContext.Session.GetInt32("Meals") == null){
               HttpContext.Session.SetInt32("Meals", 3);
            }
            if(HttpContext.Session.GetInt32("Happiness") == null){
                HttpContext.Session.SetInt32("Happiness", 20);
            }
            if(HttpContext.Session.GetInt32("Fullness") == null){
                HttpContext.Session.SetInt32("Fullness", 20);
            }
            if(HttpContext.Session.GetInt32("Energy") == null){
                HttpContext.Session.SetInt32("Energy", 50);
            }
            if(HttpContext.Session.GetString("message") == null){
                HttpContext.Session.SetString("message", "Hi I'm your monster, I love you");
            }
            if(HttpContext.Session.GetInt32("Happiness") <= 0 || HttpContext.Session.GetInt32("Fullness") <= 0 ){
                ViewBag.Show = "false";
                HttpContext.Session.SetString("message", "You killed your monster! How Could you! Play this game multiple times until you really learn to take care of your Dojodachi before getting yourself a real monster or pet.");
                ViewBag.image = "blueFuzzDeath.jpg";
                ViewBag.Happy = HttpContext.Session.GetInt32("Happiness");
                ViewBag.Full = HttpContext.Session.GetInt32("Fullness");
                ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
                ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
                ViewBag.message = HttpContext.Session.GetString("message");
                return View();
            }
            if(HttpContext.Session.GetInt32("Happiness") >= 100 && HttpContext.Session.GetInt32("Fullness") >= 100 && HttpContext.Session.GetInt32("Energy") >= 100){
                ViewBag.Show = "false";
                ViewBag.image = "blueFuzzWin.jpg";
                HttpContext.Session.SetString("message", "You win! Thank you for taking such great care of me!");
                ViewBag.message = HttpContext.Session.GetString("message");
                ViewBag.Happy = HttpContext.Session.GetInt32("Happiness");
                ViewBag.Full = HttpContext.Session.GetInt32("Fullness");
                ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
                ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
                return View();
            }
             if(HttpContext.Session.GetInt32("Happiness") <= 10){
                ViewBag.image = "blueFuzzPlay.jpg";
                HttpContext.Session.SetString("message", "please play with me!");
                
            }else{
               ViewBag.image = "blueFuzz1.jpg"; 
            }
           
            ViewBag.message = HttpContext.Session.GetString("message");
            ViewBag.Happy = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Full = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Show = "true";
            
           

            return View();
        }
        [HttpGet]
        [Route("process/{Name}")]
        public IActionResult Process(string Name)
        {
            Random rnd = new Random();
            int full = rnd.Next(5 , 11);
            int hap = rnd.Next(5, 11);
            int food = rnd.Next(1, 4);
            int likeFood = rnd.Next(1, 5);
            int likePlay = rnd.Next(1, 5);
            System.Console.WriteLine("full: " + full + " hap: " + hap + " food: " + food + " likeFood: " + likeFood + " likePlay: " + likePlay);
           if(Name == "Feed" && HttpContext.Session.GetInt32("Meals") > 0){
           
            HttpContext.Session.SetInt32("Fullness", (int) HttpContext.Session.GetInt32("Fullness")+full); 
            HttpContext.Session.SetString("message", "You fed your monster. Fullness + " + full + ". Thank you for feeding me.");
                if(likeFood == 2){
                    HttpContext.Session.SetString("message", "You fed your monster. Fullness + " + full + ". Thank you for feeding me. I didn't like it much.");
            }
            HttpContext.Session.SetInt32("Meals", (int) HttpContext.Session.GetInt32("Meals")-1);
            }
            
            if(Name == "Play"){
               HttpContext.Session.SetInt32("Happiness", (int) HttpContext.Session.GetInt32("Happiness")+hap);
               HttpContext.Session.SetString("message", "You played with your monster. Happiness + " + hap + " and energy -5." + " I like to play!");
                if(likePlay == 1){
                 HttpContext.Session.SetString("message", "You played with your monster. Happiness + " + hap + " and energy -5." + " I like to play, but I didn't like that game.");
                }
                HttpContext.Session.SetInt32("Energy", (int) HttpContext.Session.GetInt32("Energy")-5); 
            }
            if(Name == "Work"){
                HttpContext.Session.SetInt32("Energy", (int) HttpContext.Session.GetInt32("Energy")-5);
                HttpContext.Session.SetInt32("Meals", (int) HttpContext.Session.GetInt32("Meals")+food);
                HttpContext.Session.SetString("message", "You worked. Meals + " + food + " and energy -5");
            }
            if(Name == "Sleep"){
                HttpContext.Session.SetInt32("Energy", (int) HttpContext.Session.GetInt32("Energy")+15);
                HttpContext.Session.SetInt32("Fullness", (int) HttpContext.Session.GetInt32("Fullness")-5);
                HttpContext.Session.SetInt32("Happiness", (int) HttpContext.Session.GetInt32("Happiness")-5);
                HttpContext.Session.SetString("message", "Sleep time. Fullness - " + food + ", happiness - " + hap + ", and energy +15");
            }
            
        
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        [Route("Reset")]
        public IActionResult Reset(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    
}
