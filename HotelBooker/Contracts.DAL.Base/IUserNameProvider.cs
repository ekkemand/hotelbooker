namespace Contracts.DAL.Base
{
    public interface IUserNameProvider
    {
        string CurrentUserName { get;  }
    }
}