using MediatR;

namespace Service.Auth.CreateAccessToken
{
    public class CreateAccessTokenServiceRequest : IRequest<CreateAccessTokenServiceResponse>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? ExpiryDays { get; set; }
    }
}
