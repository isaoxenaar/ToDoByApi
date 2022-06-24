using System.ComponentModel.DataAnnotations;
namespace todoList;

public class SubTd {
    [Key]
    public int Id {get;set;}
    public int ToDoId {get;set;}
    public string? Title {get;set;}
    public string? Text {get;set;}
    public string? Deadline {get;set;}
    public int Cost {get;set;}
}
