#nullable disable
using System.ComponentModel.DataAnnotations;
namespace todoList;

public class ToDo {
    [Key]
    public int Id {get;set;}
    public string Title {get;set;}
}
