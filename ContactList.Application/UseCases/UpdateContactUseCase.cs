using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Domain.Entities;
using ContactList.Application.Interfaces;
using ContactList.Application.Dtos;

namespace ContactList.Application.UseCases
{
    public class UpdateContactUseCase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IViaCepService _viaCepService;

        public UpdateContactUseCase(IContactRepository contactRepository, IViaCepService viaCepService)
        {
            _contactRepository = contactRepository;
            _viaCepService = viaCepService;
        }

        public async Task<(bool Success, string Message)> ExecuteAsync(int id, UpdateContactDto updateContactDto)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id);
            if (existingContact == null)
            {
                return (false, "Contato não encontrado.");
            }

            var viaCepResponse = await _viaCepService.GetAddressByCepAsync(updateContactDto.Cep);

            existingContact.Name = updateContactDto.Name;
            existingContact.Email = updateContactDto.Email;
            existingContact.Phone = updateContactDto.Phone;
            existingContact.Cep = viaCepResponse.Cep;
            existingContact.Street = viaCepResponse.Logradouro;
            existingContact.Neighborhood = viaCepResponse.Bairro;
            existingContact.City = viaCepResponse.Localidade;
            existingContact.State = viaCepResponse.Uf;

            await _contactRepository.UpdateAsync(existingContact);
            await _contactRepository.SaveChangesAsync();

            return (true, "Contato atualizado com sucesso.");
        }
    }
}
