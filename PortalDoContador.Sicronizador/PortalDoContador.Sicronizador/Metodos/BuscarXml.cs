using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Modelos.RequestAPI;
using Integrador_Com_CRM.Metodos;
using P.Contador.Sicronizador.Biblioteca.Metodos;
using P.Contador.Sicronizador.Metodos;




namespace PortalDoContador.Sicronizador.Metodos
{
    internal class BuscarXml
    {
        private readonly CrudXml _CrudXml;

        public BuscarXml()
        {
            _CrudXml = new CrudXml();
        }

        public async Task BuscarNovosXml() 
        {
            try
            {
                MetodosGerais.RegistrarInicioLog("XML");

                List<CriarXML> criarXMLs = _CrudXml.BuscarBoletosInDB();
         
                foreach (var xmlExistente in criarXMLs)
                {
                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhdWd1c3RvIiwianRpIjoiYjQ2YWNiYjMtNjYwMC00NTMzLTlmZTYtMjgwYmNhYWExYmEwIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiI1NGMzYzc1Ni1mNTNiLTRlZmMtYmU0ZS0wNmEwYWE0NDA2YjUiLCJDb250YWJpbGlkYWRlSWQiOiI3Iiwicm9sZSI6WyJDbGllbnRlIiwiQWRtaW4iLCJDb250YWJpbGlkYWRlIl0sImV4cCI6MTcyOTYyNjMwMSwiaXNzIjoiY2FzYWRhaW5mb3JtYXRpY2FzY0lzc3VlciIsImF1ZCI6ImNhc2FkYWluZm9ybWF0aWNhc2NBdWRpZW5jZSJ9.HISEkHW-GzcUYmW4HYDpsn-WmC2U6c5-KO-HEJTZPmE";
                    CriarXML Response = await EnviarBoletoParaCRM.CriarOportunidade(xmlExistente, token);
                }


            }
            catch(Exception ex)
            {
                MetodosGerais.RegistrarLog("XML", $"[ERROR]: {ex.Message}");
            }
        }
    }
}
