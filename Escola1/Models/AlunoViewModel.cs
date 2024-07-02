using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Escola1.Models;

public class AlunoViewModel
{
    [Display(Name = "ID")]
    public int AlunoId { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão de nome do aluno")]
    [StringLength(50, ErrorMessage = "O nome do aluno deve ter no máximo {1} caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão de identidade do aluno")]
    [StringLength(12, ErrorMessage = "A identidade do aluno deve ter no máximo {1} caracteres")]
    public string? Identidade { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão de nascimento do aluno")]
    [DataType(DataType.Date)]
    public DateTime? Nascimento { get; set; }

    public int? TurmaId { get; set; }

    public TurmaViewModel? Turma { get; set; }
}
