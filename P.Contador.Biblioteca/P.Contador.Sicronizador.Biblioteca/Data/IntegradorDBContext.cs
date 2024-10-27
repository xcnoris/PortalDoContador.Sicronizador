

using Integrador_Com_CRM.Data.Map;
using Integrador_Com_CRM.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Integrador_Com_CRM.Data
{
    public class IntegradorDBContext : DbContext
    {
        private readonly string _connectionString;

        // Construtor que aceita DbContextOptions
        public IntegradorDBContext(DbContextOptions<IntegradorDBContext> options)
            : base(options)
        {
        }

        public IntegradorDBContext()
        {
            string teste = "";
            var conexao = new ConexaoDB(teste);
            _connectionString = conexao.Carregarbanco();
        }

      
        public DbSet<ControleXMLMap> Controle_XML_PC { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfiguration(new ControleXMLMap());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Aqui é usado a string de conexão carregada da classe ConexaoDB
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
