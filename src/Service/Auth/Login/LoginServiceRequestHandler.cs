using Data.Users.GetUser;
using MediatR;
using Sdk.Core.Exceptions;
using Sdk.Helpers.CryptoHelper;
using Service.Utils;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Auth.Login
{
    public class LoginServiceRequestHandler : IRequestHandler<LoginServiceRequest, LoginServiceResponse>
    {
        private readonly IMediator _mediator;
        private readonly ICryptoHelper _cryptoHelper;

        public LoginServiceRequestHandler(IMediator mediator, ICryptoHelper cryptoHelper)
        {
            _mediator = mediator;
            _cryptoHelper = cryptoHelper;
        }

        public async Task<LoginServiceResponse> Handle(LoginServiceRequest request, CancellationToken cancellationToken)
        {
            string passwordHash = _cryptoHelper.HashWithSha256(request.Password);

            var user = await _mediator.Send(new GetUserDataRequest
            {
                UserName = request.UserName,
                PasswordHash = passwordHash
            }, cancellationToken);

            if (user == null)
                throw new CustomException(TranslationKeys.User.NotFound, HttpStatusCode.NotFound);

            var createAccessTokenServiceRequest = new CreateAccessTokenServiceRequest
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
                UserName = user.UserName
            };

            return await _mediator.Send(createAccessTokenServiceRequest, cancellationToken);
        }
    }
}
