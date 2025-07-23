using System.ComponentModel.DataAnnotations;

namespace ContactList.Application.Dtos
{
    public class CreateContactDto
    {
        [Required(ErrorMessage = "Adicione um nome ao contato!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 a 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Informe um e-mail.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe um telefone.")]
        [Phone(ErrorMessage = "O número de telefone não é válido.")]
        public required string Phone { get; set; }

        public required string Address { get; set; }

    }
}
