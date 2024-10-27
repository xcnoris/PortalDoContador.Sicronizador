using Integrador_Com_CRM.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P.Contador.Sicronizador.Biblioteca.Modelos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador_Com_CRM.Data.Map
{
    public class ControleXMLMap : IEntityTypeConfiguration<Control_XML_PC>
    {
        public void Configure(EntityTypeBuilder<Control_XML_PC> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Xml_NFe_Id).IsRequired();

        }
    }
}
