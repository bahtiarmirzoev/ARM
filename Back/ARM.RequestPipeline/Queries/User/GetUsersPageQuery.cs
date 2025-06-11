using ARM.Core.Common;
using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.User;

public record GetUsersPageQuery(int PageNumber, int PageSize) : IRequest<PaginatedResponse<UserDto>>;