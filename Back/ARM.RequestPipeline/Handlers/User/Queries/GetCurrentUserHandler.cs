using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Queries;
 
public class GetCurrentUserHandler(IUserService userService) : IRequestHandler<GetCurrentUserQuery, PublicUserDto>
{
    public async Task<PublicUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return await userService.GetCurrentUserAsync();
    }
}