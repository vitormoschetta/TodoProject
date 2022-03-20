using System.ComponentModel.DataAnnotations;

namespace Todo.App.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        
        public bool Done { get; set; }

        [Required(ErrorMessage = "Informe o título")]
        public string Title { get; set; }
    }
}