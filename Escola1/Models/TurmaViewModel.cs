using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escola1.Models;

public class TurmaViewModel
{
    [Display(Name = "Id")]
    public int TurmaId { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão do nome da turma")]
    [StringLength(12, ErrorMessage = "Nome da turma deve ter no máximo {1} caracteres")]
    public string? Ano { get; set; }

    [Required(ErrorMessage = "É obrigatório inclusão de letra sequência da turma")]
    [StringLength(1, ErrorMessage = "Sequência da turma deve ter no máximo {1} caractere")]
    public string? Sequencia { get; set; }

    [Display(Name = "Sala")]
    public int? SalaId { get; set; }

    //public SalaViewModel? Sala { get; set; }

    public string? Nome {
        get
        {
            return $"{Ano} {Sequencia}";
        }
    }

    public ICollection<AlunoViewModel>? Alunos { get; set; }
}
