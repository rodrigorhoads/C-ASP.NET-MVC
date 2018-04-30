using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmesCinemasSessoes.Models
{
    public class Filmes
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O nome deve ter no maximo 50 letras.")]
        public string Nome { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O Genero deve ter no maximo 50 letras.")]
        public string Genero { get; set; }

        [Display(Name ="Classificacao Indicativa ")]
        public string ClassificacaoIndicativa { get; set; }

        [Required(ErrorMessage = "A duração deve ser informada!.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:mm}")]
        public string Duracao { get; set; }
    }
}