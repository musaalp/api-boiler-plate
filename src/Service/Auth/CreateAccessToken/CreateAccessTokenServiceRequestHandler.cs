using Data.Users.GetUserPermissions;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sdk.Api.Models;
using Sdk.Core.Exceptions;
using Service.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Auth.CreateAccessToken
{
    public class CreateAccessTokenServiceRequestHandler : IRequestHandler<CreateAccessTokenServiceRequest, CreateAccessTokenServiceResponse>
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IMediator _mediator;

        public CreateAccessTokenServiceRequestHandler(IMediator mediator,
            IOptions<JwtSettings> optionsMonitor)
        {
            _mediator = mediator;
            _jwtSettings = optionsMonitor.Value;
        }

        public async Task<CreateAccessTokenServiceResponse> Handle(CreateAccessTokenServiceRequest request, CancellationToken cancellationToken)
        {
            var secretKey = Encoding.ASCII.GetBytes(_jwtSettings.JwtSecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", request.UserName),
                    new Claim("UserId", request.UserId.ToString()),
                    new Claim("FullName", $"{request.FirstName} {request.LastName}")
                }),
                Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            if (request.ExpiryDays.HasValue)
                tokenDescriptor.Expires = DateTime.Now.AddDays((double) request.ExpiryDays);

            if (!string.IsNullOrWhiteSpace(request.Email))
                tokenDescriptor.Subject.AddClaim(new Claim("Email", request.Email));

            var permissionsResponse = await _mediator.Send(new GetUserPermissionsDataRequest
            {
                UserId = request.UserId
            }, cancellationToken);

            if (permissionsResponse.Permissions.Count < 1)
                throw new CustomException(TranslationKeys.User.UserHasNoClaim, HttpStatusCode.NotFound);

            foreach (var permission in permissionsResponse.Permissions)
                tokenDescriptor.Subject.AddClaim(new Claim("Permission", permission.Value));

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(securityToken);

            return new CreateAccessTokenServiceResponse
            {
                AccessToken = accessToken
            };
        }
    }
}
