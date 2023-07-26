using Tour_FP.Models.DTO;

namespace Tour_FP.Repositories.Abstract
{
    public interface IUserAuthenticationServices
    {

        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
