﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RNDR.DAL;
using RNDR.DAL.Models;
using RNDR.Services.Helpers;

namespace RNDR.Services.UserManagement
{

    /// <summary>
    /// Represent a user service
    /// </summary>
    public class UserService : IUserService
    {
        private DataContext _context;


        /// <summary>
        /// Initialize a new instance of the <see cref="UserService"/> class with specified <see cref="DataContext"/>.
        /// </summary>
        /// <param name="context">A <see cref="DataContext"/>.</param>
        public UserService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            return !VerifyPasswordHash(password, Convert.FromBase64String(user.Password), user.PasswordSalt) ? null : user;

            // authentication successful
        }

        /// <inheritdoc/>
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        /// <inheritdoc/>
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        /// <inheritdoc/>
        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password)) throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username)) throw new AppException("Username \"" + user.Username + "\" is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.Password = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <inheritdoc/>
        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");

                user.Username = userParam.Username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                user.Password = Convert.ToBase64String(passwordHash);
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return;
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        #region private helper methods
        
        /// <summary>
        /// Create password hash and uniq salt for new user.
        /// </summary>
        /// <param name="password">Clear password.</param>
        /// <param name="passwordHash">Encrypted password.</param>
        /// <param name="passwordSalt">Password's salt.</param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Verify is entered password equals hashed ones.
        /// </summary>
        /// <param name="password">Entered password.</param>
        /// <param name="storedHash">Hashed password.</param>
        /// <param name="storedSalt">Uniq salt that were used to encrypt password.</param>
        /// <returns>Is it corrected entered password.</returns>
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}