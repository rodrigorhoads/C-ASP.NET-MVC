using FilmesCinemasSessoes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmesCinemasSessoes.ViewsModels
{
    public class CinemaIndexData
    {
        public IEnumerable<Cinema> Cinemas { get; set; }
        public IEnumerable<Filmes> Filmes { get; set; }
        public IEnumerable<Sessao> Sessoes { get; set; }
    }
}