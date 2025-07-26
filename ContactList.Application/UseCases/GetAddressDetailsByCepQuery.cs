using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Application.Dtos;
using ContactList.Application.Interfaces;

namespace ContactList.Application.UseCases
{
    public class GetAddressDetailsByCepQuery
    {
        private readonly IViaCepService _viaCepService;

        public GetAddressDetailsByCepQuery (IViaCepService viaCepService)
        {
            _viaCepService = viaCepService;
        }

        public async Task<AddressDetailsDto?> ExecuteAsync(string cep)
        {
            var cleanCep = cep.Replace("-", "").Replace(".", "").Trim();

            if (string.IsNullOrWhiteSpace(cleanCep) || cleanCep.Length != 8)
            {
                return null;
            }

            var viaCepResponse = await _viaCepService.GetAddressByCepAsync(cleanCep);

            if (viaCepResponse == null || viaCepResponse.Erro)
            {
                return null;
            }

            var addressDetails = new AddressDetailsDto
            {
                Cep = viaCepResponse.Cep,
                Street = viaCepResponse.Logradouro,
                Neighborhood = viaCepResponse.Bairro,
                City = viaCepResponse.Localidade,
                State = viaCepResponse.Uf
            };

            return addressDetails;
        }
    }
}
