using Microsoft.AspNetCore.Mvc;
using LAXFilm.Models;
using System.Data.SqlClient;

namespace LAXFilm.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "data source=DESKTOP-GC15CMR; database=Films; integrated security= SSPI; ";
        }
        [HttpPost]
        public IActionResult Verify(AccountModel account)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = ("select * from logindata where username ='" + account.Name + "' and pass ='" + account.Password + "'");
            dr = com.ExecuteReader();
            if (dr.Read())
            {
               
                
                con.Close();
                //videresender siden til den valgte view og controller
                return RedirectToAction("Index", "CRUD");
            }
            else
            {
                con.Close();
                return View("Error");
            }
        }

    }
}