using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace todoList;

public class User {
    [Key]
    public int Id {get;set;}
    public string? Name {get;set;}
    public string? Email {get;set;}
    // [JsonIgnore]
    public string? Password {get;set;}
    public List<TdList>? TdLists {get;set;}
}
