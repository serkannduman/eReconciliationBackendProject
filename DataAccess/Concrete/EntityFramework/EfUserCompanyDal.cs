﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserCompanyDal : EfEntityRepositoryBase<UserCompany, ContextDb>, IUserCompanyDal
{
}
