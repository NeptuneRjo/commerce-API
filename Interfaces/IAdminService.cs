using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// Queries the database to find any duplicate emails
        /// <br />
        /// If there are none, a new admin with the credentials is created
        /// </summary>
        /// <param name="adminModel"></param>
        /// <returns>The created <see cref="AdminDto"/> object</returns>
        AdminDto CreateAdmin(AdminModel adminModel);
        /// <summary>
        /// Queries the database to find the admin and validates that the password matches
        /// </summary>
        /// <param name="adminModel"></param>
        /// <returns>The retrieved <see cref="AdminDto"/> object</returns>
        AdminDto GetAdmin(AdminModel adminModel);
        /// <summary>
        /// Queries the database for the admin that matches the email
        /// <br />
        /// Verifies that the old password matches and updates it
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPass"></param>
        /// <param name="newPass"></param>
        /// <returns>The updated <see cref="AdminDto"/> object</returns>
        AdminDto UpdateAdmin(UpdateAdmin update);
        /// <summary>
        /// Queries the database for the admin that matches the email and deletes it
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true if successfully deleted</returns>
        bool DeleteAdmin(AdminModel adminModel);
    }
}
