﻿using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CompanyManager>().As<ICompanyService>();    
        builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

        builder.RegisterType<AccountReconciliationDetailManager>().As<IAccountReconciliationDetailService>();
        builder.RegisterType<EfAccountReconciliationDetailDal>().As<IAccountReconciliationDetailDal>();

        builder.RegisterType<AccountReconciliationManager>().As<IAccountReconciliationService>();
        builder.RegisterType<EfAccountReconciliationDal>().As<IAccountReconciliationDal>();

        builder.RegisterType<BaBsReconciliationsDetailManager>().As<IBaBsReconciliationsDetailService>();
        builder.RegisterType<EfBaBsReconciliationDetailDal>().As<IBaBsReconciliationDetailDal>();

        builder.RegisterType<BaBsReconciliationManager>().As<IBaBsReconciliationService>();
        builder.RegisterType<EfBaBsReconciliationDal>().As<IBaBsReconciliationDal>();

        builder.RegisterType<CurrencyAccountManager>().As<ICurrencyAccountService>();
        builder.RegisterType<EfCurrencyAccountDal>().As<ICurrencyAccountDal>();

        builder.RegisterType<CurrencyManager>().As<ICurrencyService>();
        builder.RegisterType<EfCurrencyDal>().As<ICurrencyDal>();

        builder.RegisterType<MailParameterManager>().As<IMailParameterService>();
        builder.RegisterType<EfMailParameterDal>().As<IMailParameterDal>();


    }
}
