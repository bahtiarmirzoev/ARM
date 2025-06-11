using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Common;
using ARM.Core.Dtos.Read;
using ARM.RequestPipeline.Queries.User;
using MediatR;

namespace ARM.RequestPipeline.Handlers.User.Queries;

public class GetUsersPageQueryHandler(IUserService userService)
    : IRequestHandler<GetUsersPageQuery, PaginatedResponse<UserDto>>
{
    public async Task<PaginatedResponse<UserDto>> Handle(GetUsersPageQuery request, CancellationToken cancellationToken)
    {
        return await userService.GetUsersPageAsync(request.PageNumber, request.PageSize);
    }
}