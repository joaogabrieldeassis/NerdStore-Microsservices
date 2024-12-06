using System.ComponentModel.DataAnnotations;

namespace NerdStoreEnterprise.Identity.Api.Models;

public class RegisterUser
{
    [Required(ErrorMessage = "O campo nome é obrigatorio")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "O campo email é obrigatorio")]
    [EmailAddress(ErrorMessage = "O campo email está em formato invalido}")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo senha é obrigatorio")]
    [StringLength(100, ErrorMessage = "O campo senha precisa ter entre {2} e {1} caracters", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "As senhas não conferem")]
    public string ConfirmPassword { get; set; } = string.Empty;
}