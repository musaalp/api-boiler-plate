using MediatR;

namespace Service.Auth.Login
{
    public class LoginServiceRequest : IRequest<LoginServiceResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
