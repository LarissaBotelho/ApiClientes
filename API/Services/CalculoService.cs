using ApiCobranca.Models;
using ApiCobranca.Services;
using StoneAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoneAPI.Services
{
    public class CalculoService
    {

        private CobrancaService _cobrancaService;
        private ClienteService _clienteService; 

        public CalculoService(CobrancaService cobrancaService, ClienteService clienteService)
        {
            _cobrancaService = cobrancaService;
            _clienteService = clienteService;
        }

        // metodo que realiza o calculo das cobrancas para cada usuario cadastrado.
        public async Task calculaValores()
        {
            List<Cliente> clientes = await _clienteService.Get();

            if (clientes != null)
            {
                foreach (var cliente in clientes)
                {
                    string doisPrimeiros = cliente.Cpf.Substring(0, 2);
                    string doisUltimos = cliente.Cpf.Substring(cliente.Cpf.Length - 2, 2);

                    string valor = doisPrimeiros + doisUltimos;

                    List<Cobranca> cobrancas = await _cobrancaService.GetByCpf(cliente.Cpf);
                    if(cobrancas != null && cobrancas.Count > 0)
                    {
                        foreach (var cobranca in cobrancas)
                        {
                            cobranca.Valor = decimal.Parse(valor);
                            await _cobrancaService.Update(cobranca.Id, cobranca);
                        }
                    }
                    else
                    {
                        Cobranca cobranca = new Cobranca();
                        cobranca.Valor = decimal.Parse(valor);
                        cobranca.Cpf = cliente.Cpf;
                        cobranca.DataVencimento = DateTime.Now.AddDays(15);

                        await _cobrancaService.Create(cobranca);
                    }
                    
                    
               
                }
            }

          
        }

    }
}
