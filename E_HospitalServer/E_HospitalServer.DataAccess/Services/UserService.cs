using AutoMapper;
using E_HospitalServer.Business.Services;
using E_HospitalServer.DataAccess.Extensions;
using E_HospitalServer.Entities.DTOs;
using E_HospitalServer.Entities.Enums;
using E_HospitalServer.Entities.Models;
using E_HostpitalServer.Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_HospitalServer.DataAccess.Services;

internal sealed class UserService(
    UserManager<User> userManager,
    IMapper mapper) : IUserService
{
    public async Task<Result<string>> CreateUserAsync(CreateUserDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            var isEmailExist = await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
            if (isEmailExist)
            {
                return Result<string>.Failure(409, "Email is already taken" );
            }
        }
        

        if(request.IdentityNumber != "11111111111")
        {
            var isIdentityNumberExists = await userManager.Users.AnyAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken: cancellationToken);
            if (isIdentityNumberExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Identity number is already exist");
            }
        }

        var user = mapper.Map<User>(request);

        var isUserNameExists = await userManager.Users.AnyAsync(p => p.UserName == request.Username, cancellationToken: cancellationToken);
        if (isUserNameExists)
        {
            return Result<string>.Failure(StatusCodes.Status409Conflict, "User name is already taken");
        }
    
        Random random = new();

        bool isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(100000, 999999);
            if(!userManager.Users.Any(p=> p.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }
        }

        
        if (request.Specialty is not null)
        {
            //user.UserType = UserType.Doctor;
            user.DoctorDetail = new DoctorDetail
            {
                Specialty = (Specialty)request.Specialty,
                WorkingDays = request.WorkingDays ?? []
            };
        }

        IdentityResult result;
        if (request.Password is not null)
            result = await userManager.CreateAsync(user, request.Password);
        else
            result = await userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            return Result<string>.Succeed("User Create is successful");
        }
        return Result<string>.Failure(500, result.Errors.Select(x => x.Description).ToList());
    }

    public async Task<Result<User>> FindPatientWithIdentityNumberAsync(string identityNumber, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByIdentityNumber(identityNumber);

        if (user is null)
        {
            return Result<User>.Failure(500,"User not found");
        }

        return user;
    }

    public async Task<Result<string>> CreatePatientAsync(CreatePatientDto request, CancellationToken cancellationToken)
    {
        if (request.Email is not null)
        {
            bool isEmailExists = await userManager.Users.AnyAsync(p => p.Email == request.Email, cancellationToken: cancellationToken);
            if (isEmailExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Email already has taken");
            }
        }        

        if (request.IdentityNumber != "11111111111")
        {
            bool isIdentityNumberExists = await userManager.Users.AnyAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken: cancellationToken);
            if (isIdentityNumberExists)
            {
                return Result<string>.Failure(StatusCodes.Status409Conflict, "Identity number already exists");
            }
        }

        User user = mapper.Map<User>(request);        
        user.UserType = UserType.Patient;

        var number = 0;
        while (await userManager.Users.AnyAsync(p => p.UserName == user.UserName, cancellationToken: cancellationToken))
        {
            number++;
            user.UserName += number;
        }

        Random random = new();

        bool isEmailConfirmCodeExists = true;
        while (isEmailConfirmCodeExists)
        {
            user.EmailConfirmCode = random.Next(100000, 999999);
            if (!userManager.Users.Any(p => p.EmailConfirmCode == user.EmailConfirmCode))
            {
                isEmailConfirmCodeExists = false;
            }
        }

        user.EmailConfirmCodeSendDate = DateTime.UtcNow;
        

        IdentityResult result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            return Result<string>.Failure(500, result.Errors.Select(s => s.Description).ToList());            
        }

        return Result<string>.Succeed("User create is successful");
    }

    public async Task<Result<List<User>>> GetAllDoctorsAsync(CancellationToken cancellationToken)
    {
        var doctors = 
            await userManager
                .Users
                .Where(p => p.UserType == UserType.Doctor)
                .Include(p => p.DoctorDetail)
                .OrderBy(p => p.FirstName)
                .ToListAsync(cancellationToken);

        return Result<List<User>>.Succeed(doctors);
    }
}