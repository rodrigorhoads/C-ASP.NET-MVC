using FilmesCinemasSessoes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FilmesCinemasSessoes.DAL
{
    public class Endereco
    {
        [Key]
        [ForeignKey("Cinema")]
        public int CinemaID { get; set; }

        [Required]
        [StringLength(50)]        
        public string Rua { get; set; }

        [Required]
        public int Numero { get; set; }


        public string Complemento {get;set;}

        [Required(ErrorMessage = "O CEP deve ser informado.!")]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deverá estar no formato 00000000 ou 00000-000")]
        [DisplayName("CEP")]
        public string CEP { get; set; }

        public virtual Cinema Cinema { get; set; }
    }
}