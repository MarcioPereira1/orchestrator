using api_orchestrator;
using System;
using System.Diagnostics;
using System.Dynamic;

namespace Automacao_Provisao
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Orchestrator orchestrator = new Orchestrator(@"C:\Users\fiska\OneDrive\Área de Trabalho\Arquivo Teste.txt");

            //Console.WriteLine(orchestrator.urlAPI + "\n" + orchestrator.caminhoDiretorio);
            //object body = new { title = "foo", body = "bar", userId = 2};
            //object body = new { /*name = "Daniel Braga",*/ username = "Marcinhoo", password = "123456" };
            object body = new { name = "Harry Potter 6" };

            await orchestrator.Get("", "user/books");


        }
    }
}
