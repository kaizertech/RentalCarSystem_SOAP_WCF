using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using RentalCarSystem.BusinessLogic.Abstactions;
using RentalCarSystem.BusinessLogic.Concretes;
using RentalCarSystem.DataAccessLayer;
using RentalCarSystem.DataAccessLayer.Repositories.Abstractions;
using RentalCarSystem.DataAccessLayer.Repositories.Concrates;
using RentalCarSystem.Models;

namespace RentalCarSystem_WebService
{
    /// <summary>
    /// Convert için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class Convert : System.Web.Services.WebService
    {
        public Convert()
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

        public bool result = false;


        private CustomerContext _dbContext;
        private IUnitOfWork _uow;
        private IRepository<Cars> _carsRepo;
        private IRepository<Customers> _customersRepo;
        private IRepository<Companies> _companiesRepo;
        private IRepository<Transactions> _transactionRepo;


        [WebMethod]
        public Customers CustomerFind(int ID)
        {
            try
            {
                using(var Fine = _uow)
                {
                    _customersRepo = Fine.GetRepository<Customers>();
                    return _customersRepo.GetById(ID);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public bool InsertCustomer(Customers entity)
        {
            try
            {
                using(var Insert = _uow )
                {
                    _customersRepo = Insert.GetRepository<Customers>();
                    _customersRepo.Add(entity);
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
        public bool DeleteCustomer(int id)
        {
            try
            {
                Customers user = _customersRepo.GetById(id);

                _customersRepo.Delete(user);
                _uow.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        
        [WebMethod]
        public Customers[] GetCustomer()
        {
            try
            {
                using (var List = _uow)
                {
                    _customersRepo = List.GetRepository<Customers>();
                    return _customersRepo.GetAll().ToArray();
                    
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public bool Updates(int id)
        {
            try
            {
                using(var update = _uow)
                {
                    Customers user = _customersRepo.GetById(id);

                    _customersRepo.Update(user);
                    
                    
                }
                return true;
            }
            catch (Exception)
            {

                return false;
                
            }
        }
        [WebMethod]
        public bool LoginCheck(string userName, string userPass)
        {

            string un, up;
            
            try
            {
                List<Customers> custom = new List<Customers>();
                foreach (var item in GetCustomer().OrderBy(x => x.Musteri_ID).ToList())
                {
                    Customers castedCustomer = new Customers()
                    {
                        Musteri_KullaniciAdi = item.Musteri_KullaniciAdi,
                        Musteri_Sifre = item.Musteri_Sifre

                    };
                    custom.Add(castedCustomer);
                }
                foreach (var items in custom)
                {
                    un = items.Musteri_KullaniciAdi;
                    up = items.Musteri_Sifre;


                    if (un == userName && up == userPass)
                    {
                        result = true;
                        break;

                    }

                }


                return result;

            }
            catch (Exception)
            {

                return false;
            }
        }
  
    }
}
