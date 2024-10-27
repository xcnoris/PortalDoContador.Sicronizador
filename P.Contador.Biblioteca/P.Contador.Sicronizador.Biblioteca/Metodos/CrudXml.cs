using API.Modelos.RequestAPI;
using Integrador_Com_CRM.DataBase;
using Integrador_Com_CRM.Metodos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P.Contador.Sicronizador.Biblioteca.Metodos
{
    public class CrudXml
    {
        private ConexaoDB _conexaoDB;
        private ComandosDB _comandosDB;

        public CrudXml()
        {
            string Validacao = "";
            _conexaoDB = new ConexaoDB(Validacao);
            _comandosDB = new ComandosDB(_conexaoDB);
        }


        public List<CriarXML> BuscarBoletosInDB()
        {
            try
            {

                // Buscar XMl no banco de dados a partir de uma data ou parâmetro definido
                string query = @"
                    
                    select 
	                    nx.chave_nfe,
	                    nx.tipo_xml,
	                    nx.modelo_nfe,
	                    nf.Ide_TipoOperacao,
	                    nf.Ide_NumeroNF,
	                    nx.xml_nfe,
	                    nf.Ide_DataHoraEmissao,
	                    nf.Emit_CNPJ,
	                    nf.Lojamix_IdFilial
                    from
	                    nfe_xml nx
                    inner join
	                    nfe nf on nx.chave_nfe = nf.Ide_ChaveAcessoNFe
                    where 
	                    nx.tipo_xml = 2
	                AND Ide_DataHoraEmissao > '06/09/2024'
                ";

                // Converte o resultado do select em DataTable
                DataTable retornoOS = _comandosDB.ExecuteQuery(query);

                List<CriarXML> retornoBoletos = DataTableToList(retornoOS);

                MetodosGerais.RegistrarLog("OS", $"Foram encontradas {retornoBoletos.Count()} ordem de serviço no banco de dados\n");
                return retornoBoletos;
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("OS", $"[ERROR]: {ex.Message} - {_comandosDB.Mensagem}");

                return null;
            }
        }

        private List<CriarXML> DataTableToList(DataTable dt)
        {
            try
            {
                List<CriarXML> listaRetornoOS = new List<CriarXML>();

                foreach (DataRow linha in dt.Rows)
                {
                    CriarXML RBoleto = new CriarXML()
                    {
                        Chave_Nota = linha["chave_nfe"].ToString(),
                        Situacao_Nota = linha["tipo_xml"].ToString(),
                        Modelo_Nota = linha["modelo_nfe"].ToString(),
                        Tipo_Nota = linha["Ide_TipoOperacao"].ToString(),
                        Numero_Nota = linha["Ide_NumeroNF"].ToString(),
                        Xml_Nota = linha["xml_nfe"].ToString(),
                        Data_Emissao = Convert.ToDateTime(linha["Ide_DataHoraEmissao"])
                    };

                    listaRetornoOS.Add(RBoleto);
                }
                return listaRetornoOS;
            }
            catch (Exception ex)
            {
                MetodosGerais.RegistrarLog("OS", ex.Message);

                return null;
            }
        }
    }
}
