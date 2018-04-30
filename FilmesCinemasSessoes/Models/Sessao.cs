using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmesCinemasSessoes.Models
{
    public class Sessao
    {   
        public int ID { get; set; }

        public int CinemaID { get; set; }

        public int FilmeID { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Horario da Sessao")]
        public DateTime Horario { get; set; }

        public virtual Filmes Filme { set; get; }
        public virtual Cinema Cinema { get; set; }
        
    }
}