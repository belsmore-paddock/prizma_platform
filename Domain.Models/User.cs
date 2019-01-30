namespace Prizma.Domain.Models
{
    using System;
    using System.Collections.Generic;

    using Prizma.Domain.Models.Builders;

    /// <summary>
    /// The user class represents the domain-specific representation of a single user.
    /// </summary>
    public class User : DomainBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userBuilder">
        /// The user builder.
        /// </param>
        public User(UserBuilder userBuilder)
        {
            this.Id = userBuilder.Id;
            this.UserName = userBuilder.UserName;
            this.Email = userBuilder.Email;
            this.Password = userBuilder.Password;
            this.Name = userBuilder.Name;
            this.IsActive = userBuilder.IsActive;
            this.IsDeleted = userBuilder.IsDeleted;
            this.IsEmailConfirmed = userBuilder.IsEmailConfirmed;
            this.IsTwoFactorAuthenticationEnabled = userBuilder.IsTwoFactorAuthenticationEnabled;
            this.SecurityStamp = userBuilder.SecurityStamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        protected User()
        {
            // This is required for Entity Framework.
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public virtual string UserName { get; protected set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public virtual string Email { get; protected set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public virtual string Password { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public virtual bool IsActive { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public virtual bool IsDeleted { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is email confirmed.
        /// </summary>
        public virtual bool IsEmailConfirmed { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is two factor authentication enabled.
        /// </summary>
        public virtual bool IsTwoFactorAuthenticationEnabled { get; protected set; }

        /// <summary>
        /// Gets or sets the security stamp.
        /// </summary>
        public virtual string SecurityStamp { get; protected set; }

        /// <summary>
        /// Gets or sets the user claims.
        /// </summary>
        public virtual ISet<UserClaim> Claims { get; protected set; } = new HashSet<UserClaim>();

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public virtual ISet<UserRole> Roles { get; protected set; } = new HashSet<UserRole>();

        /// <summary>
        /// Gets or sets the logins.
        /// </summary>
        public virtual ISet<UserLogin> Logins { get; protected set; } = new HashSet<UserLogin>();

        /// <summary>
        /// Marks the user's email address as having been confirmed.
        /// </summary>
        public void ConfirmEmail()
        {
            this.IsEmailConfirmed = true;
        }

        /// <summary>
        /// Marks the current user as not active.
        /// </summary>
        public void DeactivateUser()
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Marks the current user as active.
        /// </summary>
        public void ActivateUser()
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Adds a new user <paramref name="claim"/> to the current user object.
        /// </summary>
        /// <param name="claim">
        /// The user claim being added.
        /// </param>
        public void AddClaim(UserClaim claim)
        {
            this.Claims.Add(claim);
        }

        /// <summary>
        /// Adds a new user <paramref name="role"/> to the current user object.
        /// </summary>
        /// <param name="role">
        /// The user role being added.
        /// </param>
        public void AddRole(UserRole role)
        {
            this.Roles.Add(role);
        }

        /// <summary>
        /// Adds a new <paramref name="login"/> entry to the users history.
        /// </summary>
        /// <param name="login">
        /// The login instance being added.
        /// </param>
        public void AddLogin(UserLogin login)
        {
            this.Logins.Add(login);
        }
    }
}