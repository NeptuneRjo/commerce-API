using AutoMapper;
using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using System.Linq.Expressions;

namespace CommerceClone.Services
{
    using BCrypt.Net;

    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IMapper _mapper;

        private Expression<Func<Admin, object>>[] includes = { e => e.Stores };

        public AdminService(IAdminRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public AdminDto CreateAdmin(AdminModel adminModel)
        {
            Admin exists = _repository.GetByQuery(e => e.Email == adminModel.Email);

            if (exists != null)
                throw new Exception($"An admin with the email {adminModel.Email} already exists");

            Admin admin = _mapper.Map<Admin>(adminModel);

            admin = _repository.GenerateKeys(admin);
            admin.Password = _repository.EncryptPass(admin.Password);

            if (admin.Stores == null)
                admin.Stores = new List<Store>();

            _repository.Add(admin);

            AdminDto dto = _mapper.Map<AdminDto>(admin);

            return dto;
        }

        public bool DeleteAdmin(string email)
        {
            Admin admin = _repository.GetByQuery(e => e.Email == email);

            if (admin == null)
                throw new Exception($"No admin with the email: {email} found");

            try
            {
                _repository.Delete(admin.Id);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public AdminDto GetAdmin(string email)
        {
            Admin admin = _repository.GetByQuery(e => e.Email == email, includes);

            if (admin == null)
                throw new Exception($"No admin with the email: {email} found");

            AdminDto dto = _mapper.Map<AdminDto>(admin);

            return dto;
        }

        public AdminDto UpdateAdmin(string email, string oldPass, string newPass)
        {
            Admin admin = _repository.GetByQuery(e => e.Email == email);

            if (admin == null)
                throw new Exception($"No admin with the email: {email} found");

            bool verified = BCrypt.Verify(oldPass, admin.Password);

            if (verified)
            {
                admin.Password = _repository.EncryptPass(newPass);

                _repository.Update(admin.Id, admin);

                AdminDto dto = _mapper.Map<AdminDto>(admin);

                return dto;
            }

            throw new Exception("Credentials do not match");
        }
    }
}
