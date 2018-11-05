namespace Prizma.Domain.Models.Tests
{
    using System;

    using Prizma.Domain.Models;

    /// <summary>
    /// The domain base test class. Not for actual use!
    /// </summary>
    public class DomainBaseTestClass : DomainBase<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBaseTestClass"/> class.
        /// </summary>
        /// <param name="id">
        /// The id to be used for testing this class.
        /// </param>
        public DomainBaseTestClass(Guid id)
        {
            this.Id = id;
        }
    }
}