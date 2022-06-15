using System.ComponentModel.DataAnnotations;

namespace Todo.UI.Blazor.Models;
public class TodoItem
{
    public Guid Id { get; set; }

    public bool Done { get; set; }

    [Required(ErrorMessage = "Informe o título")]
    public string Title { get; set; }
}
