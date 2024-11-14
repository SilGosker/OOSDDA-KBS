namespace Kbs.Business.User;

[Flags]
public enum Role
{
    Member = 1,
    GameCommissioner = 2,
    MaterialCommissioner = 4
}