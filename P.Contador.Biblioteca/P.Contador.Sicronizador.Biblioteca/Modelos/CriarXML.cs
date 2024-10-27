using System;

namespace API.Modelos.RequestAPI
{
    public class CriarXML
    {
        private string _cnpj;

        public string Chave_Nota { get; set; }
        public string Situacao_Nota { get; set; }
        public string Modelo_Nota { get; set; }
        public string Tipo_Nota { get; set; }
        public string Numero_Nota { get; set; }
        public string Xml_Nota { get; set; }
        public DateTime Data_Emissao { get; set; }
    }
}