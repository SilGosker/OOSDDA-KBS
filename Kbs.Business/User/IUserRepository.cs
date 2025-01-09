namespace Kbs.Business.User;

public interface IUserRepository
{
    public void Create(UserEntity user);
    public void Update(UserEntity user);
    public void Delete(UserEntity user);
    public List<UserEntity> Get();
    public UserEntity GetByCredentials(string email, string password);
    public UserEntity GetByEmail(string email);
    public UserEntity GetById(int id);
    public void Ban(UserEntity user);
    public IEnumerable<UserEntity> GetUsersByName(string name);
    public IEnumerable<UserRole> GetAllRoles();
    public IEnumerable<UserEntity> GetUsersByNameAndRole(string name, UserRole role);    
}