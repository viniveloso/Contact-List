using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Domain.Entities;
using ContactList.Application.Interfaces;
using ContactList.Application.Dtos;
using System.IO;

namespace ContactList.Application.UseCases
{
    public class CreateContactUseCase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IViaCepService _viaCepService;

        public CreateContactUseCase(IContactRepository contactRepository, IViaCepService viaCepService)
        {
            _contactRepository = contactRepository;
            _viaCepService = viaCepService;
        }   

        public async Task<(bool Success, string Message, Contact? Contact)> ExecuteAsync(CreateContactDto createContactDto)
        {
            var viaCepResponse = await _viaCepService.GetAddressByCepAsync(createContactDto.Cep);

            if (viaCepResponse == null || viaCepResponse.Erro)
            {
                return (false, "CEP não encontrado ou invalido.", null);
            }

            var contact = new Contact(
                createContactDto.Name,
                createContactDto.Email,
                createContactDto.Phone,
                viaCepResponse.Cep,
                viaCepResponse.Logradouro,
                viaCepResponse.Localidade,
                viaCepResponse.Bairro,
                viaCepResponse.Uf
                );

            await _contactRepository.AddAsync( contact );
            await _contactRepository.SaveChangesAsync();

            return (true, "Contato criado com sucesso", contact);
        }
    }
}
