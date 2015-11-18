using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxWeb.Models
{
    public class ArquivoViewModel
    {
        [Required]
        [DisplayName("Selecione um arquivo para fazer upload.")]
        public HttpPostedFileBase File { get; set; } 
    }

    public class ArquivoUploadDBModel
    {
        public int Id { get; set; }
        public string Comprador { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
        public string Endereco{ get; set; }
        public string Fornecedor { get; set; }
    }
}