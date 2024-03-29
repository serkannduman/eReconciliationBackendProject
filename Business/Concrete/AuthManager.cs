﻿using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;
    private readonly ICompanyService _companyService;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _companyService = companyService;
    }

    public IResult CompanyExists(Company company)
    {
        var result = _companyService.CompanyExists(company);
        if (result.Success == false)
            return new ErrorResult(Messages.CompanyAlreadyExists);

        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
    {
        var claims = _userService.GetClaims(user, companyId);
        var accessToken = _tokenHelper.CreateToken(user, claims, companyId);
        return new SuccessDataResult<AccessToken>(accessToken);
    }

    public IDataResult<User> login(UserForLogin userForLogin)
    {
        var userToCheck = _userService.GetByEmail(userForLogin.Email);
        if (userToCheck is null)
            return new ErrorDataResult<User>(Messages.UserNotFound);

        if (!HashingHelper.VeriyPasswordHash(userForLogin.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            return new ErrorDataResult<User>(Messages.PasswordError);

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

    public IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password,Company company)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
        var user = new User()
        {
            Email = userForRegister.Email,
            AddedAt = DateTime.Now,
            IsActive = true,
            MailConfirm = false,
            MailConfirmDate = DateTime.Now,
            MailConfirmValue = Guid.NewGuid().ToString(),
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Name = userForRegister.Name
        };

        _userService.Add(user);
        _companyService.Add(company);

        _companyService.UserCompanyAdd(user.Id, company.Id);

        UserCompanyDto userCompanyDto = new UserCompanyDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            AddedAt = user.AddedAt,
            CompanyId = company.Id,
            IsActive = true,
            MailConfirm = user.MailConfirm,
            MailConfirmDate = user.MailConfirmDate,
            MailConfirmValue = user.MailConfirmValue,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
        };

        return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
    }

    public IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password)
    {
        throw new NotImplementedException();
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByEmail(email)!= null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }
        return new SuccessResult();
    }
}
