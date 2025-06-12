using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ARM.Common.Exceptions;
using ARM.Common.Extensions;
using ARM.Core.Abstractions.Repositories.Main;
using ARM.Core.Abstractions.Services.Main;
using ARM.Core.Abstractions.UOW;
using ARM.Core.Dtos.Create;
using ARM.Core.Dtos.Read;
using ARM.Core.Dtos.Update;
using ARM.Core.Entities.Main;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ARM.Core.Common;

namespace ARM.Application.Services.Main;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<CreateCustomerDto> _createValidator;
    private readonly IValidator<UpdateCustomerDto> _updateValidator;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ICustomerRepository customerRepository,
        IValidator<CreateCustomerDto> createValidator,
        IValidator<UpdateCustomerDto> updateValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _customerRepository = customerRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _unitOfWork = unitOfWork;
    }

    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            throw new AppException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
        var userId = GetUserId();
        await _createValidator.ValidateAndThrowAsync(dto);

        if (await _customerRepository.AnyAsync(c => c.Email == dto.Email))
            throw new AppException(ExceptionType.BadRequest, "EmailAlreadyExists");

        if (await _customerRepository.AnyAsync(c => c.PhoneNumber == dto.PhoneNumber))
            throw new AppException(ExceptionType.BadRequest, "PhoneNumberAlreadyExists");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var customerEntity = _mapper.Map<CustomerEntity>(dto);
            customerEntity.EmailVerified = false;

            await _customerRepository.AddAsync(customerEntity);

            return _mapper.Map<CustomerDto>(customerEntity);
        });
    }

    public async Task<CustomerDto> UpdateAsync(string id, UpdateCustomerDto dto)
    {
        var userId = GetUserId();
        var existingCustomer = (await _customerRepository.FindAsync(
                c => c.Id == id))
            .FirstOrDefault()
            .EnsureFound("CustomerNotFound");

        await _updateValidator.ValidateAndThrowAsync(dto);

        if (await _customerRepository.AnyAsync(c => c.Email == dto.Email && c.Id != id))
            throw new AppException(ExceptionType.BadRequest, "EmailAlreadyExists");

        if (await _customerRepository.AnyAsync(c => c.PhoneNumber == dto.PhoneNumber && c.Id != id))
            throw new AppException(ExceptionType.BadRequest, "PhoneNumberAlreadyExists");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingCustomer);
            await _customerRepository.UpdateAsync(new[] { existingCustomer });

            return _mapper.Map<CustomerDto>(existingCustomer);
        });
    }

    public async Task DeleteAsync(string id)
    {
        var userId = GetUserId();
        var customer = (await _customerRepository.FindAsync(
                c => c.Id == id))
            .FirstOrDefault()
            .EnsureFound("CustomerNotFound");

        await _customerRepository.DeleteAsync(customer.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<CustomerDto> GetByIdAsync(string id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var userId = GetUserId();
        var customers = await _customerRepository.FindAsync(c => c.Id == userId);
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
}