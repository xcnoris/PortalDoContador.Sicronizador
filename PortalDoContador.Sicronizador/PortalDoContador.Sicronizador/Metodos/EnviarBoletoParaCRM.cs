using API.Modelos.RequestAPI;
using Integrador_Com_CRM.Metodos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace P.Contador.Sicronizador.Metodos
{
    internal class EnviarBoletoParaCRM
    {


        public static async Task<CriarXML> CriarOportunidade(CriarXML request, string Token)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://api.leadfinder.com.br/integracao/v2/inserirOportunidade";
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    MetodosGerais.RegistrarLog("XML", "Enviando Xml para API....");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        CriarXML resposta = JsonConvert.DeserializeObject<CriarXML>(responseBody);
                        if (resposta != null)
                        {
                            MetodosGerais.RegistrarLog("XML", "Resposta OK - XML Enviado para API:");
                            MetodosGerais.RegistrarLog("XML", responseBody);

                            return resposta;
                        }
                        else
                        {
                            MetodosGerais.RegistrarLog("XML", "Erro na resposta: resposta desserializada é nula.");
                        }
                    }
                    else
                    {
                        MetodosGerais.RegistrarLog("XMLXML", "Erro na resposta da API:");
                        MetodosGerais.RegistrarLog("XML", $"Status Code: {response.StatusCode}");
                        MetodosGerais.RegistrarLog("XML", responseBody);
                    }
                }
                catch (Exception ex)
                {
                    MetodosGerais.RegistrarLog("XML", "Exceção durante a chamada da API:");
                    MetodosGerais.RegistrarLog("XML", ex.Message);
                    throw;
                }

                return null;
            }
        }

        //public static async Task<CriarXML> AtualizarAcao(CriarXML request, string Token)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        string url = "https://api.leadfinder.com.br/integracao/movimentarOportunidade";
        //        client.DefaultRequestHeaders.Add("Authorization", Token);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        string json = JsonConvert.SerializeObject(request);
        //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //        try
        //        {
        //            HttpResponseMessage response = await client.PostAsync(url, content);
        //            string responseBody = await response.Content.ReadAsStringAsync();

        //            if (response.IsSuccessStatusCode)
        //            {
        //                CriarXML resposta = JsonConvert.DeserializeObject<CriarXML>(responseBody);
        //                if (resposta != null)
        //                {
        //                    MetodosGerais.RegistrarLog("XML", "Resposta OK - XML enviado para API : ");
        //                    MetodosGerais.RegistrarLog("XML", responseBody);

        //                    using (var dalBoletoUsing = new DAL<RelacaoBoletoCRMModel>(new IntegradorDBContext()))
        //                    {
        //                        await dalBoletoUsing.AtualizarAsync(BoletoRElacao);
        //                    }
        //                    if (foiquitado)
        //                    {
        //                        CobrancasNaSegundaModel cobrancas = new CobrancasNaSegundaModel();
        //                        await cobrancas.RemoverRegistro(BoletoRElacao.Id, true);


        //                        MetodosGerais.RegistrarLog("XML", $"Situacao atualizada para {BoletoRElacao.Situacao} na tabela de relação para a o documento a receber {BoletoRElacao.Id_DocumentoReceber}.");

        //                    }
        //                    return resposta;
        //                }
        //                else
        //                {
        //                    MetodosGerais.RegistrarLog("XML", "Erro na resposta: resposta desserializada é nula.");
        //                }
        //            }
        //            else
        //            {
        //                MetodosGerais.RegistrarLog("XML", "Erro na resposta da API:");
        //                MetodosGerais.RegistrarLog("XML", $"Status Code: {response.StatusCode}");
        //                MetodosGerais.RegistrarLog("XML", responseBody);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MetodosGerais.RegistrarLog("XML", "Exceção durante a chamada da API:");
        //            MetodosGerais.RegistrarLog("XML", ex.Message);
        //            throw;
        //        }

        //        return null;
        //    }
        //}
    }
}
