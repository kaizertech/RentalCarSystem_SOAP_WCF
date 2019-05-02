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

        private CustomerContext _dbContext;

        private IUnitOfWork _uow;
        private IRepository<Cars> _carsRepo;
        private IRepository<Customers> _customersRepo;
        private IRepository<Companies> _companiesRepo;
        private IRepository<Transactions> _transactionRepo;

        

        public class Entitys
        {
            private CustomerContext _dbContext;

            private IUnitOfWork _uow;
            private IRepository<Cars> _carsRepo;
            private IRepository<Customers> _customersRepo;
            private IRepository<Companies> _companiesRepo;
            private IRepository<Transactions> _transactionRepo;

            public void TestInitialize()
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

            public void AddUser(string ad, string soyad)
            {
                Customers user = new Customers
                {
                    Musteri_Ad = ad.ToString(),
                    Musteri_Soyad = soyad.ToString(),
                    Musteri_TC = "584612925768",
                    Musteri_KullaniciAdi = "qyusuf_dede",
                    Musteri_Sifre = "123e"
                };

                _customersRepo.Add(user);
                int process = _uow.SaveChanges();

                //Assert.AreNotEqual(-1, process);
            }

            public void AddUser1()
            {
                Transactions tr = new Transactions
                {
                    Musteri_id =2,
                    Ucret = 123,
                    CreatedDate = DateTime.Now

                };

                _transactionRepo.Add(tr);
                int process = _uow.SaveChanges();

                //Assert.AreNotEqual(-1, process);
            }

            public void TestCleanup()
            {
                _dbContext = null;
                _uow.Dispose();
            }
            public void GetUser()
            {
                Customers user = _customersRepo.GetById(1);

               // Assert.IsNotNull(user);
            }

            public void DeleteUser(int ss)
            {
                Customers user = _customersRepo.GetById(ss);

                _customersRepo.Delete(user);
                int process = _uow.SaveChanges();

                //Assert.AreNotEqual(-1, process);
            }
            public void UpdateUser(int ss)
            {
                Customers user = _customersRepo.GetById(ss);

                user.Musteri_Ad = "Mehmet";

                _customersRepo.Update(user);
                int process = _uow.SaveChanges();

                // Assert.AreNotEqual(-1, process);
            }
        }

        Entitys en = new Entitys();

        [WebMethod]
        public string getAll()
        {
            string temp = "";
            Customers query = _customersRepo.GetAll().FirstOrDefault();

            temp = query.ToString();

            return temp;
        }
        

        
        [WebMethod]
        public string LoadMembership()
        {
            string temp = "";
            
            foreach (Customers item in _customersRepo.GetAll())
            {
                temp += item.Musteri_Ad+" -- "+item.Musteri_TC+"-- ";
            }
            return temp;
        }

        [WebMethod]
        public string GetUser()
        {
            string temp = "";
            Customers user = _customersRepo.GetById(6);

            temp += user.Musteri_ID+"\n"+user.Musteri_KullaniciAdi.ToString();

            return temp;
            // Assert.IsNotNull(user);
        }



        [WebMethod]
         public void AddUser()
            {
                Cars cm = new Cars()
                {   
                    
                    Sirket_id = 1,
                    Araba_Ad ="Mercedes",
                    Araba_Model ="CLK 2018",
                    Araba_GunlukKira = 250,
                    Araba_AirBag = true,
                    Araba_BagajHacmi =31,
                    Araba_GunlukKmSiniri = 24,
                    Araba_KoltukSayisi = 5
                   
                };
                
               _carsRepo.Add(cm);
                int process = _uow.SaveChanges();

                //Assert.AreNotEqual(-1, process);
            }

        [WebMethod]
         public void DeleteUser(int ss)
            {
                Customers user = _customersRepo.GetById(ss);

                _customersRepo.Delete(user);
                int process = _uow.SaveChanges();

                //Assert.AreNotEqual(-1, process);
            }
        
    }
}
