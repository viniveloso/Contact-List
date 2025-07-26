using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactList.Application.Dtos;

namespace ContactList.Application.Interfaces
{
    public interface IViaCepService
    {
        Task<ViaCepResponseDto> GetAddressByCepAsync(string cep);
    }
}
