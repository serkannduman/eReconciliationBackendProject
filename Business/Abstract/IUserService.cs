﻿using Core.Entities.Concrete;

namespace Business.Abstract;

public interface IUserService
{
    List<OperationClaim> GetClaims(User user,int companyId);
    void Add(User user);
    User GetByEmail(string email);
}
