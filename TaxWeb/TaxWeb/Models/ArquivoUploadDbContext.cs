using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaxWeb.Models
{
    public class ArquivoUploadDbContext:DbContext
    {
        public ArquivoUploadDbContext()
            : base("name=DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<ArquivoUploadDBModel> ArquivoUploadDBModels { get; set; }
    }
}