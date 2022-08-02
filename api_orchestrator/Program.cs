using api_orchestrator;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Dynamic;

namespace Automacao_Provisao
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Orchestrator orchestrator = new Orchestrator();

            object body = new { username = "Marcinho", password = "123456" };
            //object body = new { name = "Harry Potter 6" };

            await orchestrator.Put("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6Ik1hcmNpbmhvIiwiaWF0IjoxNjU5NDY5OTgzLCJleHAiOjE2NTk1NTYzODMsInN1YiI6IjMxOGYzMTkwLTY1NzEtNDQ1Mi05NmQ2LTdhN2RkYzI0NDA0YSJ9.MrdIS_hQh1XjxS2XzEfov_xc1zPUDo2xJeiVRcFICAw", body, "user/update");
        }

    }
}
