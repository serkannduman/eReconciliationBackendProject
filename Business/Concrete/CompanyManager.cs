using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete;

public class CompanyManager : ICompanyService
{
    private readonly ICompanyDal _companyDal;

    public CompanyManager(ICompanyDal companyDal)
    {
        _companyDal = companyDal;
    }

    public List<Company> GetList()
    {
        return _companyDal.GetList();
    }
}
