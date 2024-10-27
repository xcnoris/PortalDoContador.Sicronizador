

using System;
using System.IO;

namespace Integrador_Com_CRM.Metodos
{
    public class MetodosGerais
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        static MetodosGerais()
        {
            // Garantir que o diretório de logs exista
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        private static string GetLogFilePath(string logType)
        {
            return Path.Combine(LogDirectory, $"log-{logType}-{DateTime.Now:dd-MM-yyyy}.txt");
        }

        public static void RegistrarInicioLog(string logType)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetLogFilePath(logType), true))
                {
                    string logEntry = $"======================================> Inicio do Log <======================================";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void RegistrarLog(string logType, string mensagem)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetLogFilePath(logType), true))
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções relacionadas ao log
            }
        }

        public static void RegistrarFinalLog(string logType)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(GetLogFilePath(logType), true))
                {
                    string logEntry = $"\n======================================>   Fim do Log  <======================================\n";
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções relacionadas ao log
                
            }
        }
    }
}
