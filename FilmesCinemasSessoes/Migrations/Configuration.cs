namespace FilmesCinemasSessoes.Migrations
{
    using FilmesCinemasSessoes.DAL;
    using FilmesCinemasSessoes.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FilmesCinemasSessoes.DAL.FilmesCinemasSessoesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FilmesCinemasSessoes.DAL.FilmesCinemasSessoesContext";
        }

        protected override void Seed(FilmesCinemasSessoes.DAL.FilmesCinemasSessoesContext context)
        {
            var filmes = new List<Filmes>
            {
                new Filmes{Nome="Os Vingadores: The Avengers",Genero="Fantasia/Filme de ficção científica",ClassificacaoIndicativa="12 anos",Duracao="143 min"},
                new Filmes{Nome="Logan",Genero=" Drama/Filme de ficção ",ClassificacaoIndicativa="16 anos",Duracao="141 min"},
                new Filmes{Nome="Meu Malvado Favorito 3",Genero="Animação, Aventura, Comédia",ClassificacaoIndicativa="3 anos",Duracao="90 min"},
                new Filmes{Nome="Thor: Ragnarok",Genero="Ação, Aventura, Fantasia",ClassificacaoIndicativa="12 Anos",Duracao="130 min"},
                new Filmes{Nome="DEADPOOL",Genero="Comédia , Ação, Fantasia",ClassificacaoIndicativa="16 Anos",Duracao="108 min"}
            };
            filmes.ForEach(s => context.Filmes.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();

            var cinemas = new List<Cinema>
            {
                new Cinema {CinemaID = 1, Nome = "Cinemark Tucuruvi",},
                new Cinema {CinemaID = 2, Nome = "Cinemark Guarulhos",},
                new Cinema {CinemaID = 3, Nome = "Centerplex Lapa", },
                new Cinema {CinemaID = 4, Nome = "Playarte Maraba",}
            };
            cinemas.ForEach(s => context.Cinemas.AddOrUpdate(p => p.Nome, s));
            context.SaveChanges();

            var enderecos = new List<Endereco>
            {
                new Endereco {CinemaID = 1, Rua = "Av. Dr. Antônio Maria Laet,",Numero=566,CEP="02308-130"},
                new Endereco {CinemaID = 2, Rua = "Rodovia Presidente Dutra,",Numero=256,CEP="07034-911"},
                new Endereco {CinemaID = 3, Rua = "R. Catão,",Numero=72,CEP="05049-000" },
                new Endereco {CinemaID = 4, Rua = "Av. Ipiranga,",Numero=757,CEP="01039-000"}
            };
            enderecos.ForEach(s => context.Enderecoes.AddOrUpdate(p => p.Rua, s));
            context.SaveChanges();

        }
    }
}
