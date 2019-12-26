using System.Collections.Generic;
using RNDR.DAL.Models;

namespace RNDR.Services.UserManagement
{
    /// <summary>
    /// Represent user management service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticate user on server and create JWT-token.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>A <see cref="User"/></returns>
        User Authenticate(string username, string password);

        /// <summary>
        /// Get all users from database.
        /// </summary>
        /// <returns>List of <see cref="User"/>s.</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Get 1 user from database by id.
        /// </summary>
        /// <param name="id">A user identifier.</param>
        /// <returns></returns>
        User GetById(int id);

        /// <summary>
        /// Create new user and return it back
        /// </summary>
        /// <param name="user">A <see cref="User"/>.</param>
        /// <param name="password">Password</param>
        /// <returns>A <see cref="User"/></returns>
        User Create(User user, string password);

        /// <summary>
        /// Update existed user.
        /// </summary>
        /// <param name="user">A <see cref="User"/>.</param>
        /// <param name="password">Passwords</param>
        void Update(User user, string password = null);

        /// <summary>
        /// Delete existed user by id.
        /// </summary>
        /// <param name="id">A user identifier.</param>
        void Delete(int id);
    }
}
