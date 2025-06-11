using ARM.Core.Dtos.Read;
using MediatR;

namespace ARM.RequestPipeline.Queries.User;

public record GetCurrentUserQuery : IRequest<PublicUserDto>;