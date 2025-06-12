using System.Security.Claims;
using ARM.Common.Exceptions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Core.Common;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;

namespace ARM.Application.Services.Main;

public class ReviewService : IReviewService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReviewRepository _reviewRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateReviewDto> _createReviewValidator;
    private readonly IValidator<UpdateReviewDto> _updateReviewValidator;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IReviewRepository reviewRepository,
        IBrandRepository brandRepository,
        IUserRepository userRepository,
        IValidator<CreateReviewDto> createReviewValidator,
        IValidator<UpdateReviewDto> updateReviewValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _reviewRepository = reviewRepository;
        _brandRepository = brandRepository;
        _userRepository = userRepository;
        _createReviewValidator = createReviewValidator;
        _updateReviewValidator = updateReviewValidator;
        _unitOfWork = unitOfWork;
    }

    private string GetCustomerId()
    {
        var customerId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(customerId))
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return customerId;
    }

    public async Task<ReviewDto> CreateAsync(CreateReviewDto dto)
    {
        var customerId = GetCustomerId();
        await _createReviewValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var reviewEntity = _mapper.Map<ReviewEntity>(dto);
            reviewEntity.CustomerId = customerId;
            reviewEntity.CreatedAt = DateTime.UtcNow;

            await _reviewRepository.AddAsync(reviewEntity);

            return _mapper.Map<ReviewDto>(reviewEntity);
        });
    }

    public async Task<ReviewDto> UpdateAsync(string id, UpdateReviewDto dto)
    {
        var customerId = GetCustomerId();
        var existingReview = (await _reviewRepository.FindAsync(
                r => r.Id == id && r.CustomerId == customerId))
            .FirstOrDefault()
            .EnsureFound("ReviewNotFound");

        await _updateReviewValidator.ValidateAndThrowAsync(dto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingReview);
            await _reviewRepository.UpdateAsync(new[] { existingReview });

            return _mapper.Map<ReviewDto>(existingReview);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var customerId = GetCustomerId();
        var review = (await _reviewRepository.FindAsync(
                r => r.Id == id && r.CustomerId == customerId))
            .FirstOrDefault()
            .EnsureFound("ReviewNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _reviewRepository.DeleteAsync(review.Id);
            return true;
        });
    }

    public async Task<ReviewDto> GetByIdAsync(string id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        return _mapper.Map<ReviewDto>(review);
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var customerId = GetCustomerId();
        var reviews = await _reviewRepository.FindAsync(r => r.CustomerId == customerId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<PaginatedResponse<ReviewDto>> GetPageAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            throw new AppException(ExceptionType.BadRequest, "PaginationError");

        var totalReviews = await _reviewRepository.GetAllAsync();
        var totalItems = totalReviews.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagedReviews = totalReviews
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var reviewDtos = _mapper.Map<IEnumerable<ReviewDto>>(pagedReviews);

        return new PaginatedResponse<ReviewDto>
        {
            Data = reviewDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<ReviewDto>> GetByAutoServiceIdAsync(string autoServiceId)
    {
        var reviews = await _reviewRepository.FindAsync(r => r.AutoServiceId == autoServiceId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<double> GetAverageRatingByAutoServiceIdAsync(string autoServiceId)
    {
        var reviews = await _reviewRepository.FindAsync(r => r.AutoServiceId == autoServiceId);
        return reviews.Any() ? reviews.Average(r => r.Rating) : 0;
    }

    private async Task UpdateAutoServiceRatingAsync(string autoServiceId)
    {
        var averageRating = await GetAverageRatingByAutoServiceIdAsync(autoServiceId);
        var autoService = await _brandRepository.GetByIdAsync(autoServiceId);

        autoService.Rating = (decimal)averageRating;

        await _brandRepository.UpdateAsync(new[] { autoService });
        await _unitOfWork.SaveChangesAsync();
    }
}