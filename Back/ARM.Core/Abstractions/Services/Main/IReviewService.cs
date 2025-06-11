using ARM.Core.Common;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Core.Abstractions.Services.Main;

public interface IReviewService
{
    Task<ReviewDto> CreateAsync(CreateReviewDto dto);
    Task<ReviewDto> UpdateAsync(string id, UpdateReviewDto dto);
    Task<bool> DeleteAsync(string id);
    Task<ReviewDto> GetByIdAsync(string id);
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<PaginatedResponse<ReviewDto>> GetPageAsync(int pageNumber, int pageSize);
    Task<IEnumerable<ReviewDto>> GetByAutoServiceIdAsync(string autoServiceId);
    Task<double> GetAverageRatingByAutoServiceIdAsync(string autoServiceId);
} 