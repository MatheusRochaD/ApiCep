using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCep.Controllers
{
    [ApiController]
    public class CepController : ControllerBase
    {
        [HttpGet("v1/ConsultarCep")]
        public Contratos.ViaCep.Response ConsultarCep(string cep)
        {
            var retorno = Conexoes.ConsumoApi.Get<Contratos.ViaCep.Response>("https://viacep.com.br/ws/"+ cep + "/json/");
            return retorno;
        }
    }
}
