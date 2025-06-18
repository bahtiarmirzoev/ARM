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
using static BCrypt.Net.BCrypt;

namespace ARM.Application.Services.Main;

public class CustomerService : ICustomerService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<CreateCustomerDto> _createValidator;
    private readonly IValidator<UpdateCustomerDto> _updateValidator;
    private readonly IUnitOfWork _unitOfWork;

    private HttpContext context => _httpContextAccessor.HttpContext;

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

    public async Task<PublicCustomerDto> GetCurrentCustomerAsync()
    {
        var customerId = context.GetCustomerId();
        var user = await _customerRepository.GetByIdAsync(customerId);
        return _mapper.Map<PublicCustomerDto>(user);
    }

    public async Task<CustomerDto> ConfirmEmailAsync()
    {
        var customerId = context.GetCustomerId();
        var customer = await _customerRepository.GetByIdAsync(customerId);
        customer.EmailVerified = true;
        await _customerRepository.UpdateAsync([customer]);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
        await _createValidator.ValidateAndThrowAsync(dto);

        if (await _customerRepository.AnyAsync(c => c.Email == dto.Email))
            throw new AppException(ExceptionType.BadRequest, "EmailAlreadyExists");

        if (await _customerRepository.AnyAsync(c => c.PhoneNumber == dto.PhoneNumber))
            throw new AppException(ExceptionType.BadRequest, "PhoneNumberAlreadyExists");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var customerEntity = _mapper.Map<CustomerEntity>(dto);
            customerEntity.EmailVerified = false;
            customerEntity.Password = HashPassword(dto.Password);

            await _customerRepository.AddAsync(customerEntity);

            return _mapper.Map<CustomerDto>(customerEntity);
        });
    }

    public async Task<CustomerDto> UpdateAsync(string id, UpdateCustomerDto dto)
    {

        var existingCustomer = await _customerRepository.FindAsync(
                c => c.Id == id)
            .EnsureFound("CustomerNotFound");

        await _updateValidator.ValidateAndThrowAsync(dto);

        if (await _customerRepository.AnyAsync(c => c.Email == dto.Email && c.Id != id))
            throw new AppException(ExceptionType.BadRequest, "EmailAlreadyExists");

        if (await _customerRepository.AnyAsync(c => c.PhoneNumber == dto.PhoneNumber && c.Id != id))
            throw new AppException(ExceptionType.BadRequest, "PhoneNumberAlreadyExists");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(dto, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);

            return _mapper.Map<CustomerDto>(existingCustomer);
        });
    }

    public async Task<bool> DeleteAsync(string id)
    {

        var customer = await _customerRepository.FindAsync(
                c => c.Id == id)
            .EnsureFound("CustomerNotFound");

        await _customerRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<CustomerDto> GetByIdAsync(string id)
    {

        var customer = (await _customerRepository.FindAsync(c => c.Id == id))
            .FirstOrDefault()
            .EnsureFound("CustomerNotFound");
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto> GetCustomerByEmailAsync(string email)
    {

        var customer = (await _customerRepository.FindAsync
            (c => c.Email == email))
            .FirstOrDefault()
            .EnsureFound("CustomerDoesNotExist");
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerCredentialsDto> GetCustomerCredentialsByIdAsync(string id)
    {
        var customer = (await _customerRepository.FindAsync(c => c.Id == id))
            .FirstOrDefault()
            .EnsureFound("CustomerNotFound");

        return new CustomerCredentialsDto
        {
            Id = customer.Id,
            Password = customer.Password,
        };
    }
}