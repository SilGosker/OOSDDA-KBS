namespace Kbs.Business.User;

[Flags]
public enum UserRole
{
    Member = 1,
    GameCommissioner = 2,
    MaterialCommissioner = 4,
    Banned = 11
}