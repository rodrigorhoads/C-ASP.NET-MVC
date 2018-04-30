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
using PagedList;

namespace FilmesCinemasSessoes.Controllers
{
    public class SessoesController : Controller
    {
        private FilmesCinemasSessoesContext db = new FilmesCinemasSessoesContext();

        // GET: Sessoes
        public ActionResult Index(string sortOrder, string currentFilter, string currentFilterF, string searchString, string searchStringF, int? pagina)
        {

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
            if (searchStringF != null)
            {
                pagina = 1;
            }
            else
            {
                searchStringF = currentFilter;
            }

            ViewBag.CurrentFilterF = searchStringF;

            var sessoes = from s in db.Sesseoes
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sessoes = sessoes.Where(s => s.Cinema.Nome.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchStringF))
            {
                sessoes = sessoes.Where(s => s.Filme.Nome.Contains(searchStringF));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    sessoes = sessoes.OrderByDescending(s => s.Cinema.Nome);
                    break;

                default:  // Name ascending 
                    sessoes = sessoes.OrderBy(s => s.Cinema.Nome);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (pagina ?? 1);
            return View(sessoes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sessoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessao sessao = db.Sesseoes.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            return View(sessao);
        }

        // GET: Sessoes/Create
        public ActionResult Create()
        {
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nome");
            ViewBag.FilmeID = new SelectList(db.Filmes, "ID", "Nome");
            return View();
        }

        // POST: Sessoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CinemaID,FilmeID,Horario")] Sessao sessao)
        {
            if (ModelState.IsValid)
            {
                db.Sesseoes.Add(sessao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nome", sessao.CinemaID);
            ViewBag.FilmeID = new SelectList(db.Filmes, "ID", "Nome", sessao.FilmeID);
            return View(sessao);
        }

        // GET: Sessoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessao sessao = db.Sesseoes.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nome", sessao.CinemaID);
            ViewBag.FilmeID = new SelectList(db.Filmes, "ID", "Nome", sessao.FilmeID);
            return View(sessao);
        }

        // POST: Sessoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CinemaID,FilmeID,Horario")] Sessao sessao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sessao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nome", sessao.CinemaID);
            ViewBag.FilmeID = new SelectList(db.Filmes, "ID", "Nome", sessao.FilmeID);
            return View(sessao);
        }

        // GET: Sessoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sessao sessao = db.Sesseoes.Find(id);
            if (sessao == null)
            {
                return HttpNotFound();
            }
            return View(sessao);
        }

        // POST: Sessoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sessao sessao = db.Sesseoes.Find(id);
            db.Sesseoes.Remove(sessao);
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
                IEnumerable<Sessao> sessoes = db.Sesseoes.Where(s => s.FilmeID == ID.Value).ToList().OrderBy(c => c.Cinema.Nome);
                return View(sessoes);
            }
            else
            {
                ID = 2;
                IEnumerable<Sessao> sessoes = db.Sesseoes.Where(s => s.FilmeID == ID.Value).ToList();
                return View(sessoes);
            }

        }


        public ActionResult PaginaCinemas(int? id)
        {
            //var viewModel = new CinemaIndexData();
            //viewModel.Cinemas = db.Cinemas.Include(c => c.Sessoes);

            //if (id != null)
            //{
            //    ViewBag.CinemaID = id.Value;
            //    viewModel.Sessoes = viewModel.Cinemas.Where(
            //        i => i.CinemaID == id.Value).Single().Sessoes;
            //}
            //if (SessaoID != null)
            //{
            //    ViewBag.SessaoID = SessaoID.Value;
            //    viewModel.Sessoes = viewModel.Sessoes.Where(
            //        x => x.ID == SessaoID);
            //}

            //return View(viewModel);
            if (id != null)
            {
                IEnumerable<Sessao> sessoes = db.Sesseoes.Where(c => c.CinemaID == id.Value).ToList().OrderBy(f => f.Filme.Nome);
                return View(sessoes);
            }
            else
            {
                id = 1;
                IEnumerable<Sessao> sessoes = db.Sesseoes.Where(s => s.CinemaID == id.Value).ToList();
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
