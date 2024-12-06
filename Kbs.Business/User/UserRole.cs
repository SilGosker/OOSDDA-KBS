namespace Kbs.Business.User;

[Flags]
public enum UserRole
{
    Banned = 0,
    Member = 1,
    GameCommissioner = 2,
    MaterialCommissioner = 4,
}