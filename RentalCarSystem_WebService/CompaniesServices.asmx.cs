using RentalCarSystem.BusinessLogic.Abstactions;
using RentalCarSystem.BusinessLogic.Concretes;
using RentalCarSystem.DataAccessLayer;
using RentalCarSystem.DataAccessLayer.Repositories.Abstractions;
using RentalCarSystem.DataAccessLayer.Repositories.Concrates;
using RentalCarSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RentalCarSystem_WebService
{
    /// <summary>
    /// CompaniesServices için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class CompaniesServices : System.Web.Services.WebService
    {

        public CompaniesServices()
        {
            _dbContext = new CustomerContext();

            // EFBlogContext'i kullanıyor olduğumuz için EFUnitOfWork'den türeterek constructor'ına
            // ilgili context'i constructor injection yöntemi ile inject ediyoruz.
            _uow = new UnitOfWork(_dbContext);
            _customersRepo = new Repository<Customers>(_dbContext);
            _carsRepo = new Repository<Cars>(_dbContext);
            _companiesRepo = new Repository<Companies>(_dbContext);
            _transactionRepo = new Repository<Transactions>(_dbContext);
        }

        private CustomerContext _dbContext;
        private IUnitOfWork _uow;
        private IRepository<Cars> _carsRepo;
        private IRepository<Customers> _customersRepo;
        private IRepository<Companies> _companiesRepo;
        private IRepository<Transactions> _transactionRepo;

        [WebMethod]
        public Companies CustomerFind(int ID)
        {
            try
            {
                using (var Fine = _uow)
                {
                    _companiesRepo= Fine.GetRepository<Companies>();
                    return _companiesRepo.GetById(ID);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public bool InsertCompanies(Companies entity)
        {
            try
            {
                using (var Insert = _uow)
                {
                    _companiesRepo = Insert.GetRepository<Companies>();
                    _companiesRepo.Add(entity);
                    Insert.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        [WebMethod]
        public bool DeleteCompanies(int id)
        {
            try
            {
                Companies company = _companiesRepo.GetById(id);

                _companiesRepo.Delete(company);
                _uow.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        [WebMethod]
        public Companies[] GetCompanies()
        {
            try
            {
                using (var List = _uow)
                {
                    _companiesRepo = List.GetRepository<Companies>();
                    return _companiesRepo.GetAll().ToArray();

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
