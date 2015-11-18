using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TaxWeb.Models;

namespace TaxWeb.Controllers
{
    public class ImportaArquivoController : Controller
    {
        private ArquivoUploadDbContext db = new ArquivoUploadDbContext();

        // GET: ImportaArquivo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportaArquivo()
        {
            var model = new ArquivoViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ImportaArquivo(ArquivoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StreamReader reader = new StreamReader(model.File.InputStream);
                                    
            string arquivo = reader.ReadToEnd();

            decimal resultado = 0;
            bool validaCabeçalho = true;
            string[] stringSeparadorLinha = new string[] { "\r\n" };
            string[] linhas = arquivo.Split(stringSeparadorLinha, StringSplitOptions.None);

            string[] stringSeparadorColuna = new string[] { "\t" };
            for (int i = 0; i < linhas.Length; i++)
            {
                string[] colunas = linhas[i].Split(stringSeparadorColuna, StringSplitOptions.None);

                
                if (i==0)
                {
                    //Valida Cabeçalho
                    if (!linhas[i].ToString().Equals("Comprador\tdescrição\tPreço Uniário\tQuantidade\tEndereço\tFornecedor"))
                    {
                        validaCabeçalho = false;
                    }
                }
                else
                {
                    //processa linha no banco
                    //Conn

                    ArquivoUploadDBModel arquivoUploadModel = new ArquivoUploadDBModel();
                    arquivoUploadModel.Comprador = colunas[0].ToString();
                    arquivoUploadModel.Descricao = colunas[1].ToString();
                    arquivoUploadModel.Endereco = colunas[4].ToString();
                    arquivoUploadModel.Fornecedor = colunas[5].ToString();
                    arquivoUploadModel.PrecoUnitario = Convert.ToDecimal(colunas[2].ToString().Replace('.',','));
                    arquivoUploadModel.Quantidade = Convert.ToInt32(colunas[3].ToString());

                    resultado = resultado + arquivoUploadModel.PrecoUnitario * arquivoUploadModel.Quantidade;
                    db.ArquivoUploadDBModels.Add(arquivoUploadModel);
                    db.SaveChanges();
                }
                
            }
                        
            return Content("Obrigado, você fez o upload de "+linhas.Length.ToString()+" registros, com total bruto de "+resultado.ToString());
        }

    }
}
