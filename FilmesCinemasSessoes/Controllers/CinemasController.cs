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
using System.Data.Entity.Infrastructure;
using PagedList;

namespace FilmesCinemasSessoes.Controllers
{
    public class CinemasController : Controller
    {
        private FilmesCinemasSessoesContext db = new FilmesCinemasSessoesContext();

        // GET: Cinemas
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pagina)
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

            var cinemas = from s in db.Cinemas
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cinemas = cinemas.Where(s => s.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    cinemas = cinemas.OrderByDescending(s => s.Nome);
                    break;

                default:  // Name ascending 
                    cinemas = cinemas.OrderBy(s => s.Nome);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (pagina ?? 1);
            return View(cinemas.ToPagedList(pageNumber, pageSize));
        }


        // GET: Cinemas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        // GET: Cinemas/Create
        public ActionResult Create()
        {
            Cinema cinema = new Cinema();
            cinema.Sessoes = new List<Sessao>();
            return View(cinema);
        }

        // POST: Cinemas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CinemaID,SessaoID,Nome,Endereco")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                db.Cinemas.Add(cinema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cinema);
        }

        // GET: Cinemas/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Cinema cinema = db.Cinemas.Find(id);
            //if (cinema == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.CinemaID = new SelectList(db.Enderecoes, "CinemaID", "Endereco", "Nome", cinema.CinemaID);
            //return View(cinema);


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = db.Cinemas
                .Include(i => i.Endereco)
                .Where(i => i.CinemaID == id)
                .Single();
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);


        }

        // POST: Cinemas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(cinema).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.CinemaID = new SelectList(db.Enderecoes, "CinemaID", "Nome", "Endereco", cinema.CinemaID);
            //return View(cinema);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cinemaParaAtualizar = db.Cinemas
               .Include(i => i.Endereco)
               .Where(i => i.CinemaID == id)
               .Single();

            if (TryUpdateModel(cinemaParaAtualizar, "",
               new string[] { "CinemaID", "SessaoID", "Nome", "Endereco" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(cinemaParaAtualizar.Endereco.Rua))
                    {
                        cinemaParaAtualizar.Endereco = null;
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Não é possível salvar as alterações.Tente novamente e, se o problema persistir, consulte o administrador do sistema.");
                }
            }
            return View(cinemaParaAtualizar);
        }
    

        // GET: Cinemas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            Cinema cinema = db.Cinemas
              .Include(i => i.Endereco)
              .Where(i => i.CinemaID == id)
              .Single();

            db.Cinemas.Remove(cinema);

            var endereco = db.Enderecoes
                .Where(d => d.CinemaID == id)
                .SingleOrDefault();
            if (endereco != null)
            {
                cinema.Endereco = null;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
