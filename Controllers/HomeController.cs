using LAXFilm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using System.Web;
using System.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace LAXFilm.Controllers
{
    public class HomeController : Controller
    {

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        List<FilmModel> movies = new List<FilmModel>();
        private readonly ILogger<HomeController> _logger;
        private readonly ReCaptcha _captcha;


        public HomeController(ILogger<HomeController> logger, ReCaptcha captcha)
        {
            _logger = logger;
            con.ConnectionString = "Data Source=DESKTOP-GC15CMR;Initial Catalog=Films;Integrated Security=True";
            _captcha = captcha;

        }

        public IActionResult Index()
        {
            FetchData();
            return View(movies);
        }
       
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]

        public IActionResult ContactUs(SendMailDto sendMailDto)
        {

            if (!ModelState.IsValid) return View();
            {

                try
                {
                    MailMessage mail = new MailMessage();
                    //vi indtaster den email addresse som kontaktformularen skal sendes fra
                    mail.From = new MailAddress("aydan123@outlook.dk");
                    //og den email adresse som skal sendes til
                    mail.To.Add("aydinmehmed23@gmail.com");
                    mail.Subject = sendMailDto.Subject;
                    mail.IsBodyHtml = true;

                    string content = "Name: " + sendMailDto.Name;
                    content += "<br/> Message: " + sendMailDto.Message;

                    mail.Body = content;

                    //Opretter SMTP
                    SmtpClient smtpClient = new SmtpClient("smtp.office365.com");
                    //opret network credential hvor kontaktformularen skal sendes fra
                    NetworkCredential networkCredential = new NetworkCredential("aydan123@outlook.dk", "theAyd123");
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = networkCredential;
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mail);
                    ViewBag.Message = "Mail send";
                    //creating of mail state
                    ModelState.Clear();


                }
                catch (Exception ex)
                {
                    //Hvis der noget fejl så skal den vise fejlen til os
                    ViewBag.Message = ex.Message.ToString();
                }
            }
            return View();
        }


        [HttpPost]


        private void FetchData()
        {
            try
            {
                if (movies.Count > 0)
                {
                    movies.Clear();
                }
                con.Open();
                com.Connection = con;
                //Den vælger første 10 indtastninger fra databasen
                com.CommandText = "SELECT TOP (10) [id],[MovieName],[Year],[Creator],[Rating],[Actors],[Genre] FROM [Films].[dbo].[movie]";

                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    movies.Add(new FilmModel()
                    {//Her sammenligner vi vores model med databasen 
                        id = dr["id"].ToString()
                        ,
                        MovieName = dr["MovieName"].ToString()
                        ,
                        Year = dr["Year"].ToString()
                        ,
                        Creator = dr["Creator"].ToString()
                        ,
                        Rating = dr["Rating"].ToString()
                        ,
                        Actors = dr["Actors"].ToString()
                        ,
                        Genre = dr["Genre"].ToString()
                    });

                }
                con.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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