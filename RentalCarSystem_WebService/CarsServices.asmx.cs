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
    /// CarsServices için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class CarsServices : System.Web.Services.WebService
    {
        public CarsServices()
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
        public Cars CarsFind(int ID)
        {
            try
            {
                using (var Fine = _uow)
                {
                   _carsRepo= Fine.GetRepository<Cars>();
                    return _carsRepo.GetById(ID);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public bool InsertCars(Cars entity)
        {
            try
            {
                using (var Insert = _uow)
                {
                    _carsRepo = Insert.GetRepository<Cars>();
                    _carsRepo.Add(entity);
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
        public bool DeleteCars(int id)
        {
            try
            {
                Cars car = _carsRepo.GetById(id);

                _carsRepo.Delete(car);
                _uow.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        [WebMethod]
        public Cars[] GetCars()
        {
            try
            {
                using (var List = _uow)
                {
                    _carsRepo = List.GetRepository<Cars>();
                    return _carsRepo.GetAll().ToArray();

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
