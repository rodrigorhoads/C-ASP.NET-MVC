using FilmesCinemasSessoes.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilmesCinemasSessoes.Models
{
    public class Cinema
    {
        [Key]
        public int CinemaID { get; set; }
  
        public int SessaoID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O nome deve ter no maximo 50 letras.")]
        public string Nome { get; set; }

        public virtual Endereco Endereco { get; set; }

        public IList<Sessao> Sessoes { get; set;}

    }
}