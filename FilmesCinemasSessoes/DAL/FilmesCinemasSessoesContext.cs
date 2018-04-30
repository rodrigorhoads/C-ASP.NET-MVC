using FilmesCinemasSessoes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FilmesCinemasSessoes.DAL
{
    public class FilmesCinemasSessoesContext:DbContext
    {
        public FilmesCinemasSessoesContext() : base("FilmesCinemasSessoesContext"){ }

        public DbSet<Filmes> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Sessao> Sesseoes { get; set; }
        
       

        public DbSet<Endereco> Enderecoes { get; set; }
       
    }
}