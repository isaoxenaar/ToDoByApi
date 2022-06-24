using System.ComponentModel.DataAnnotations;
namespace todoList;

public class TdList {
    [Key]
    public int Id {get;set;}
    public int UserId {get;set;}
    public string? Title {get;set;}
    public int TotalCost {get;set;}
    public List<ToDo>? ToDoItems {get;set;}
}
