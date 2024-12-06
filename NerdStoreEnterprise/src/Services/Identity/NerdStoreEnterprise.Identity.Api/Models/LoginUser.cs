using System.ComponentModel.DataAnnotations;

namespace NerdStoreEnterprise.Identity.Api.Models;

public class LoginUser
{
    [Required(ErrorMessage = "O campo email é obrigatorio")]
    [EmailAddress(ErrorMessage = "O campo email está em formato invalido}")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo senha é obrigatorio")]
    [StringLength(100, ErrorMessage = "O campo senha precisa ter entre {2} e {1} caracters", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
