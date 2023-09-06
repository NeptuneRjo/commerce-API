using AutoMapper;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using System.Linq.Expressions;
using CommerceClone.CustomExceptions;
using CommerceApi.DTO;

namespace CommerceApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IMapper _mapper;

        //private Expression<Func<Admin, object>>[] includes = { e => e.Stores };

        public AdminService(IAdminRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public AdminDto CreateAdmin(AdminModel adminModel)
        {
            var (email, password) = adminModel;

            Admin exists = _repository.GetByQuery(e => e.Email == email);

            if (exists != null)
                throw new ObjectExistsException($"An admin with the email {email} already exists");

            Admin admin = _mapper.Map<Admin>(adminModel);

            admin = _repository.GenerateKeys(admin);
            admin.Password = _repository.EncryptPass(admin.Password);

            //if (admin.Stores == null)
            //    admin.Stores = new List<Store>();

            _repository.Add(admin);

            AdminDto dto = _mapper.Map<AdminDto>(admin);

            return dto;
        }

        public bool DeleteAdmin(AdminModel adminModel)
        {
            var (email, password) = adminModel;

            Admin admin = _repository.GetByQuery(e => e.Email == email);

            if (admin == null)
                throw new ObjectNotFoundException($"No admin with the email: {email} found");

            if (!_repository.ValidatePass(admin, password))
                throw new UnauthorizedAccessException("Invalid credentials");

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

        public AdminDto GetAdmin(AdminModel adminModel)
        {
            var (email, password) = adminModel;

            Admin admin = _repository.GetByQuery(e => e.Email == email);

            if (admin == null)
                throw new ObjectNotFoundException($"No admin with the email: {email} found");

            if (!_repository.ValidatePass(admin, password))
                throw new UnauthorizedAccessException("Invalid credentials");

            AdminDto dto = _mapper.Map<AdminDto>(admin);

            return dto;
        }

        public AdminDto UpdateAdmin(UpdateAdmin update)
        {
            var (email, password, updatePassword) = update;

            Admin admin = _repository.GetByQuery(e => e.Email == email);

            if (admin == null)
                throw new ObjectNotFoundException($"No admin with the email: {email} found");

            if (!_repository.ValidatePass(admin, password))
                throw new UnauthorizedAccessException("Invalid credentials");

            admin.Password = _repository.EncryptPass(updatePassword);

            _repository.Update(admin.Id, admin);

            AdminDto dto = _mapper.Map<AdminDto>(admin);

            return dto;
        }
    }
}
