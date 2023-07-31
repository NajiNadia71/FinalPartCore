namespace ViewModelAnd.ViewModel;
using Model.ModelClass;
using System.Text.Json.Serialization;

public class UserInApplication
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public List<Role> Role { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}