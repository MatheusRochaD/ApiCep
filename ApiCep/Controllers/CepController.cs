using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiCep.Controllers
{
    [ApiController]
    public class CepController : ControllerBase
    {
        [HttpGet("v1/ConsultarCep")]
        public IActionResult ConsultarCep(string cep)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
                {
                    throw new InvalidOperationException("Cep inválido.");
                }

                var retorno = Conexoes.ConsumoApi.Get<Contratos.ViaCep.Response>("https://viacep.com.br/ws/" + cep + "/json/");
                return StatusCode(200, retorno);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                var log = new Contratos.LogAplicacao.Request();
                log.NomeAplicacao = "ApiCep";
                log.NomeMaquina = Environment.MachineName;
                log.DataHora = DateTime.Now;
                log.MensagemErro = ex.Message;
                log.RastreioErro = ex.StackTrace;
                log.Usuario = Environment.UserName;

                Conexoes.ConsumoApi.Post<string>("https://logaplicacao.aiur.com.br/v1/Logs", log);
                return StatusCode(500, "Serviço indisponível no momento.");
            }
        }
    }
}
