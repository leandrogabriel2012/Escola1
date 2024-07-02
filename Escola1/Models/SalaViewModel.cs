using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Escola1.Models;

public class SalaViewModel
{
    [Display(Name = "ID")]
    public int SalaId { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão de número da sala")]
    [StringLength(20, ErrorMessage = "O número da sala deve ter no máximo {1} caracteres")]
    [Display(Name = "Número")]
    public string? Numero { get; set; }

    public ICollection<TurmaViewModel>? Turmas { get; set; }
}