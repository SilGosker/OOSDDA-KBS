using Kbs.Business.User;

namespace Kbs.Data.User;

public class UserRepository : IUserRepository
{
    // passwords are 'testen'
    // for now use this, later we will use the dapper ORM
    private readonly List<UserEntity> _database = new List<UserEntity>()
    {
        new UserEntity() { UserId = 1, Email = "s1196405@student.windesheim.nl", Name = "Sil Gosker", Password = "$2a$11$JXroVdXQNDCdyqbZvnhPtetIqg606DxW7UWT5Ex43cAJUiSRcw5O6", Role = Role.Member },
        new UserEntity() { UserId = 2, Email = "s1199183@gmail.com", Name = "Colin van Dongen", Password = "$2a$11$JXroVdXQNDCdyqbZvnhPtetIqg606DxW7UWT5Ex43cAJUiSRcw5O6", Role = Role.GameCommissioner }
    };

    public void Create(UserEntity user)
    {
        user.UserId = _database.Count;
        _database.Add(user);
    }

    public void Update(UserEntity user)
    {
        var userToUpdate = _database.FirstOrDefault(x => x.UserId == user.UserId);
        if (userToUpdate == null) return;

        userToUpdate.Email = user.Email;
        userToUpdate.Name = user.Name;
        userToUpdate.Password = user.Password;
        userToUpdate.Role = user.Role;
    }

    public void Delete(UserEntity user)
    {
        var userToDelete = _database.FirstOrDefault(x => x.UserId == user.UserId);
        if (userToDelete == null) return;

        _database.Remove(userToDelete);
    }

    public List<UserEntity> Get()
    {
        return _database;
    }

    public UserEntity GetByCredentials(string email, string password)
    {
        var user = _database.FirstOrDefault(x => x.Email == email);
        if (user == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return user;
    }
}