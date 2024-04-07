using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 40 caracteres.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "O slug da categoria é obrigatório.")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O slug deve conter entre 3 e 40 caracteres.")]
    public string? Slug { get; set; }
}