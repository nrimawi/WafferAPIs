using AutoMapper;

using Microsoft.EntityFrameworkCore;
using WafferAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafferAPIs.DAL.Entites;
using WafferAPIs.Dbcontext;
using WafferAPIs.DAL.Entities;

namespace WafferAPIs.DAL.Repositories
{

    public interface ISellerRepository : IDisposable
    {
        Task<SellerData> CreateSeller(SellerData sellerData);
        Task<List<SellerData>> GetSellers();
        Task<SellerData> GetSellerById(Guid id);
        Task DeleteSeller(Guid id);
        Task<SellerData> UpdateSeller(Guid id, SellerData sellerData);
        Task<SellerData> VerifySeller(Guid sellerId, string passowrd);
        Task<SellerData> GetLoggedInSeller(string userId);
        Task<List<SellerData>> GetPendingVerficationSellers();
        Task<List<SellerData>> GetVerifiedSellers();
        Task<SellerData> RejectSellerRequest(Guid sellerId);

    }


    public class SellerRepository : ISellerRepository
    {
        #region Inject Services
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authenticationRepository;
        #endregion

        public SellerRepository(AppDbContext appDbContext, IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _authenticationRepository = authenticationRepository;
        }
        public async Task<SellerData> CreateSeller(SellerData sellerData)
        {
            if (sellerData == null)
                throw new ArgumentNullException(nameof(sellerData));

            try
            {
                Seller seller = _mapper.Map<Seller>(sellerData);
                seller.IsVerified = false;
                seller.Status = true;
                seller.ApplicationUserId = null;

                _appDbContext.Sellers.Add(seller);

                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SellerData>(seller);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<SellerData>> GetSellers()
        {
            try
            {
                var activeSellers = await _appDbContext.Sellers.Where(s => s.Status == true).ToListAsync();
                return _mapper.Map<List<SellerData>>(activeSellers);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SellerData> GetSellerById(Guid id)
        {
            try
            {


                Seller seller = await _appDbContext.Sellers.Where(s => s.Id == id && s.Status == true).FirstOrDefaultAsync();

                if (seller == null)
                {
                    throw new NullReferenceException("SubCategory with id=" + id + " is not found");
                }
                return _mapper.Map<SellerData>(seller);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SellerData> UpdateSeller(Guid id, SellerData sellerData)
        {
            if (sellerData == null || id != sellerData.Id)
                throw new NullReferenceException("Seller is null or id is incorrect");


            try
            {

                Seller seller = await _appDbContext.Sellers.FindAsync(id);

                if (seller == null || seller.Status == false)
                {
                    throw new Exception("Seller with id=" + id + " is not found");
                }
                seller.Address = sellerData.Address;
                seller.CustomerServicePhoneNumber = sellerData.CustomerServicePhoneNumber;
                seller.Name = sellerData.Name;
                seller.Email = sellerData.Email;
                seller.HasStore = sellerData.HasStore;
                seller.ContactPhoneNumber = sellerData.ContactPhoneNumber;

                _appDbContext.Sellers.Update(seller);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SellerData>(seller);
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteSeller(Guid id)
        {

            try
            {

                Seller seller = await _appDbContext.Sellers.FindAsync(id);

                if (seller == null || seller.Status == false)
                {
                    throw new NullReferenceException("Seller with id=" + id + " is not found");
                }
                seller.Status = false;
                _appDbContext.Update(seller);
                await _appDbContext.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }

        public async Task<SellerData> VerifySeller(Guid sellerId, string password)
        {
            #region finding seller
            try
            {
                Seller seller = await _appDbContext.Sellers.FindAsync(sellerId);

                if (seller == null || seller.Status == false)
                {
                    throw new NullReferenceException("Seller with id=" + sellerId + " is not found");

                }
                if (seller.IsVerified)
                    throw new Exception("Seller is already verified try to login");
                #endregion
                #region Generate UserAuthentication(Register new user(seller))
                var user = _authenticationRepository.RegisterUser(seller.Email, password).Result;
                #endregion
                #region updateSeller

                seller.IsVerified = true;
                seller.ApplicationUserId = user.Id;
                _appDbContext.Update(seller);
                await _appDbContext.SaveChangesAsync();
                return _mapper.Map<SellerData>(seller);
                #endregion
            }
            catch { throw; }

        }
        public void Dispose()
        {
            _appDbContext.Dispose();
        }

        public async Task<SellerData> GetLoggedInSeller(string userId)
        {
            try
            {
                Seller seller = await _appDbContext.Sellers.Where(seller => seller.Status == true && seller.ApplicationUserId == userId).FirstOrDefaultAsync();

                if (seller == null)
                    throw new NullReferenceException(nameof(seller));

                return _mapper.Map<SellerData>(seller);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SellerData>> GetPendingVerficationSellers()
        {

            try
            {
                var pendingSellers = await _appDbContext.Sellers.Where(s => s.Status == true && s.IsVerified == false).ToListAsync();
                return _mapper.Map<List<SellerData>>(pendingSellers);
            }
            catch
            {
                throw;
            }

        }

        public async Task<List<SellerData>> GetVerifiedSellers()
        {
            try
            {
                var pendingSellers = await _appDbContext.Sellers.Where(s => s.Status == true && s.IsVerified == true).ToListAsync();
                if (pendingSellers == null)
                {
                    throw new NullReferenceException(nameof(pendingSellers));
                }
                return _mapper.Map<List<SellerData>>(pendingSellers);
            }
            catch
            {
                throw;
            }
        }

        public async Task<SellerData> RejectSellerRequest(Guid sellerId)
        {
            #region finding seller
            try
            {
                Seller seller = await _appDbContext.Sellers.FindAsync(sellerId);

                if (seller == null || seller.Status == false)
                {
                    throw new NullReferenceException("Seller with id=" + sellerId + " is not found");

                }
                if (seller.IsVerified)
                    throw new Exception("Seller is already verified try to login");
                #endregion
                _appDbContext.Sellers.Remove(seller);
                return _mapper.Map<SellerData>(seller);
            }
            catch { throw; }
            {

            }
        }

    }
}
