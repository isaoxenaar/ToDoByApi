using System.ComponentModel.DataAnnotations;
namespace todoList;

public class ToDo {
    [Key]
    public int Id {get;set;}
    public string? Title {get;set;}
    public string? Text {get;set;}
    public string? Deadline {get;set;}
    public int Cost {get;set;}
    public bool Done {get;set;}
    public List<SubTd>? SubItems {get;set;}
    public int TdListId {get;set;}
}
