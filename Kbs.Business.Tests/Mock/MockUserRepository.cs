﻿using Kbs.Business.User;

namespace Kbs.Business.Mock;

public class MockUserRepository : IUserRepository
{
    public List<UserEntity> Users { get; set; } = new();
    public void Create(UserEntity user)
    {
        Users.Add(user);
    }

    public void Update(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public void Delete(UserEntity user)
    {
        if (Users.Contains(user))
        {
            Users.Remove(user);
        }
    }

    public List<UserEntity> Get()
    {
        return Users;
    }

    public UserEntity GetByCredentials(string email, string password)
    {
        return Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}