using System.ComponentModel.DataAnnotations;
namespace todoList;

public class User {
    [Key]
    public int Id {get;set;}
    public string? Name {get;set;}
    public string? Email {get;set;}
    public string? Password {get;set;}
    public List<TdList>? TdLists {get;set;}
}
