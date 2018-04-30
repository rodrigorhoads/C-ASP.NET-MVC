using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmesCinemasSessoes.DAL;
using FilmesCinemasSessoes.Models;
using FilmesCinemasSessoes.ViewsModels;
using PagedList;

namespace FilmesCinemasSessoes.Controllers
{
    public class FilmesController : Controller
    {
        private FilmesCinemasSessoesContext db = new FilmesCinemasSessoesContext();

        // GET: Filmes
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pagina)
        {
            //int tamanhopagina = 3;
            //int numpagina = pagina ?? 1;
            //return View(db.Filmes.ToList().ToPagedList(numpagina, tamanhopagina));

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pagina = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var filmes = from s in db.Filmes
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                filmes = filmes.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    filmes = filmes.OrderByDescending(s => s.Nome);
                    break;

                default:  // Name ascending 
                    filmes = filmes.OrderBy(s => s.Nome);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (pagina ?? 1);
            return View(filmes.ToPagedList(pageNumber, pageSize));
        }



    

        // GET: Filmes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filmes filmes = db.Filmes.Find(id);
            if (filmes == null)
            {
                return HttpNotFound();
            }
            return View(filmes);
        }

        // GET: Filmes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filmes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Genero,ClassificacaoIndicativa,Duracao")] Filmes filmes)
        {
            if (ModelState.IsValid)
            {
                db.Filmes.Add(filmes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(filmes);
        }

        // GET: Filmes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filmes filmes = db.Filmes.Find(id);
            if (filmes == null)
            {
                return HttpNotFound();
            }
            return View(filmes);
        }

        // POST: Filmes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Genero,ClassificacaoIndicativa,Duracao")] Filmes filmes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filmes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filmes);
        }

        // GET: Filmes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filmes filmes = db.Filmes.Find(id);
            if (filmes == null)
            {
                return HttpNotFound();
            }
            return View(filmes);
        }

        // POST: Filmes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filmes filmes = db.Filmes.Find(id);
            db.Filmes.Remove(filmes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PaginaFilmes(int? ID)
        {
            //var viewModel = new CinemaIndexData();

            //viewModel.Sessoes = db.Sesseoes.Include(s => s.Cinema).Include(f =>f.Filme);       



            //if (SessaoID != null)
            //{
            //    ViewBag.SessaoID = SessaoID.Value;
            //    viewModel.Sessoes = viewModel.Sessoes.Where(s => s.FilmeID == SessaoID);

            //}

            if (ID != null)
            {
                IEnumerable<Sessao> sessoes = db.Sesseoes.Where(s => s.FilmeID == ID.Value).ToList().OrderBy(c=>c.Cinema.Nome);
                return View(sessoes);
            }
            else
            {
                ID = 2;
                IEnumerable<Sessao> sessoes = db.Sesseoes.Where(s => s.FilmeID == ID.Value).ToList();
                return View(sessoes);
            }           

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
