using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context;

public class ContextDb : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-PMQ8OOE\MSSQLSERVER2019;Database=eReconciliationDb;Integrated Security=true");
    }

    public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }
    public DbSet<AccountReconciliation> AccountReconciliations { get; set; }
    public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }
    public DbSet<BaBsReconciliationsDetail> BaBsReconciliationsDetails { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
    public DbSet<MailParameter> MailParameters { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
}
