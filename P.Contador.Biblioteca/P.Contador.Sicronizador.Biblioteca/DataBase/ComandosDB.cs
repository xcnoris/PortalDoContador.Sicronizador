using Microsoft.Data.SqlClient;
using System.Data;

namespace Integrador_Com_CRM.DataBase
{
    public class ComandosDB
    {
        private ConexaoDB _conexaoDB;
        public string Mensagem;

        public ComandosDB(ConexaoDB conexao)
        {
            _conexaoDB = conexao;
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parametros = null)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection connection = _conexaoDB.GetConnection();
                _conexaoDB.OpenConnection();
                SqlCommand cmd = new SqlCommand(query, connection);
                if (parametros != null)
                {
                    cmd.Parameters.AddRange(parametros);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SqlException ex)
            {
                Mensagem = "Erro ao executar a consulta: " + ex.Message;
            }
            finally
            {
                _conexaoDB.CloseConnection();
            }
            Mensagem = "Consulta executada com sucesso!";
            return dt;
        }

    }
}
