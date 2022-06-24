using System.ComponentModel.DataAnnotations;
namespace todoList;

public class User {
    [Key]
    public int Id {get;set;}
    public string? Name {get;set;}
    public List<TdList>? TdLists {get;set;}
}
