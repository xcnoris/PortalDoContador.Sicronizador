using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using Integrador_Com_CRM.Metodos;
using System.IO;
using System;

namespace Integrador_Com_CRM.DataBase
{
    public class ConexaoDB
    {
        private SqlConnection _connection;

        public string Servidor { get; set; }
        public string IpHost { get; set; }
        public string DataBase { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        // Construtor é usado no Metodo CarregarBanco, caso não exista, causa um loop
        public ConexaoDB()
        {

        }

        public ConexaoDB(string teste)
        {
            string conexao = Carregarbanco();
            _connection = new SqlConnection(conexao);
        }


        public void SaveConnectionData(string filePath)
        {
            var jsonData = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, jsonData);
        }

        public static ConexaoDB LoadConnectionData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return null;
                }

                var jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ConexaoDB>(jsonData);
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
                throw;
            }
        }

        internal string Carregarbanco()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = string.Empty;

                filePath = Path.Combine(basePath, "conexao.json");


                if (string.IsNullOrEmpty(filePath))
                {
                    throw new FileNotFoundException("O caminho do arquivo de conexão não foi encontrado.");
                }

                ConexaoDB conexao = ConexaoDB.LoadConnectionData(filePath);
                if (conexao != null)
                {
                    Servidor = conexao.Servidor;
                    IpHost = conexao.IpHost;
                    DataBase = conexao.DataBase;
                    Usuario = conexao.Usuario;
                    Senha = conexao.Senha;
                    return $"Server={conexao.IpHost};Database={conexao.DataBase};User Id={conexao.Usuario};Password={conexao.Senha}; TrustServerCertificate=True";


                }
                else
                {
                    MetodosGerais.RegistrarLog("OS", $"Arquivo de conexão não encontrado");
                    return "";
                }


            }
            catch (Exception ex)
            {
                // Log do erro pode ser adicionado aqui, se necessário
                MetodosGerais.RegistrarLog("OS", $"ERROR: {ex.Message}");
                
                // Re-throw a exceção para que ela possa ser tratada em um nível superior, se necessário
                return "";
            }
        }

        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
