using System.ComponentModel.DataAnnotations;

namespace Infrastructure;

public class User
{
    [Key] public string Name { get; set; }
}