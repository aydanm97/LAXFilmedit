using LAXFilm.Data;
using LAXFilm.Models;
using Microsoft.AspNetCore.Mvc;
 
namespace LAXFilm.Controllers
{
    public class CRUDController : Controller
    {
        public IActionResult Index()
        {
            List<FilmModel> film = new List<FilmModel>();
            FilmDAO filmDAO = new FilmDAO();
            film = filmDAO.FetchAll();
            return View("Index",film);
        }
        public IActionResult Details(int id)
        {
            FilmDAO filmDAO = new FilmDAO();
            FilmModel film = filmDAO.FetchOne(id);
            return View("Details",film);
        }     
        public IActionResult Create()
        {
            return View("FilmForm");
        }
       
        public IActionResult ProcessCreate(FilmModel filmModel)
        {
            FilmDAO filmDAO = new FilmDAO();
            filmDAO.Create(filmModel);
            return View("Details", filmModel);
        }
        public IActionResult Delete(int id)
        {
            FilmDAO filmDAO=new FilmDAO();
            filmDAO.Delete(id);
            List<FilmModel>filmModels = filmDAO.FetchAll();
            return View("Index",filmModels);
        }
    }
}
