﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password, Company company);
    IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password);
    IDataResult<User> login(UserForLogin userForLogin);
    IResult UserExists(string email);
    IResult CompanyExists(Company company);
    IDataResult<AccessToken> CreateAccessToken(User user,int companyId);
}
