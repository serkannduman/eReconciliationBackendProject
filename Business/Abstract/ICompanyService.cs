using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICompanyService
{
    IResult Add(Company company);
    IDataResult<List<Company>> GetList();
    IResult CompanyExists(Company company);
    IResult UserCompanyAdd(int userId, int companyId);
}
