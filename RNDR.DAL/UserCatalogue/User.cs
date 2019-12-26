namespace RNDR.DAL.Models
{
    /// <summary>
    /// Represent user in database.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets a product category ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a user's firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a user's lastname.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a user's phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets a user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a user's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a user's password salt.
        /// </summary>
        public byte[] PasswordSalt { get; set; }
    }
}