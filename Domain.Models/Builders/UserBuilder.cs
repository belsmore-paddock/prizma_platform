namespace Prizma.Domain.Models.Builders
{
    using System;

    using FluentValidation;

    using Prizma.Domain.Models.Validations.Construction;

    /// <summary>
    /// The user builder.
    /// </summary>
    public class UserBuilder : BuilderBase<User>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        protected internal Guid Id { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the validator for the current target domain model.
        /// </summary>
        protected override IValidator<User> Validator { get; } = new UserConstructionValidator();

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        protected internal string UserName { get; protected set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        protected internal string Email { get; protected set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        protected internal string Password { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        protected internal string Name { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        protected internal bool IsActive { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        protected internal bool IsDeleted { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is email confirmed.
        /// </summary>
        protected internal bool IsEmailConfirmed { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether is two factor authentication enabled.
        /// </summary>
        protected internal bool IsTwoFactorAuthenticationEnabled { get; protected set; }

        /// <summary>
        /// Gets or sets the security stamp.
        /// </summary>
        protected internal string SecurityStamp { get; protected set; }

        /// <summary>
        /// Builds new User with a random Id.
        /// </summary>
        /// <returns>
        /// The <see cref="UserBuilder"/>.
        /// </returns>
        public UserBuilder WithId()
        {
            var id = Guid.NewGuid();
            return this.WithId(id);
        }

        /// <summary>
        /// Builds a new User with the provided id.
        /// </summary>
        /// <param name="id">
        /// The id to use as the id of the constructed user.
        /// </param>
        /// <returns>
        /// The <see cref="UserBuilder"/>.
        /// </returns>
        public UserBuilder WithId(Guid id)
        {
            this.Id = id;
            return this;
        }

        /// <summary>
        /// Applies the provided <paramref name="userName"/> for the user being constructed.
        /// </summary>
        /// <param name="userName">
        /// The target user name.
        /// </param>
        /// <returns>
        /// The <see cref="UserBuilder"/>.
        /// </returns>
        public UserBuilder WithUserName(string userName)
        {
            this.UserName = userName;
            return this;
        }

        /// <summary>
        /// Applies the <paramref name="email"/> for the user being constructed.
        /// </summary>
        /// <param name="email">
        /// The target email.
        /// </param>
        /// <returns>
        /// The <see cref="UserBuilder"/>.
        /// </returns>
        public UserBuilder WithEmail(string email)
        {
            this.Email = email;
            return this;
        }

        /// <summary>
        /// Applies the password hash to the object being created.
        /// </summary>
        /// <param name="passwordHash">
        /// The password hash.
        /// </param>
        /// <returns>
        /// The <see cref="UserBuilder"/>.
        /// </returns>
        public UserBuilder WithPasswordHash(string passwordHash)
        {
            this.Password = passwordHash;
            return this;
        }

        /// <summary>
        /// Creates the user as active.
        /// </summary>
        /// <returns>
        /// The <see cref="UserBuilder"/>.
        /// </returns>
        public UserBuilder WithActive()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        /// Performs a creation of the user entity.
        /// </summary>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        protected override User DoBuild()
        {
            return new User(this);
        }

        public UserBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }
    }
}