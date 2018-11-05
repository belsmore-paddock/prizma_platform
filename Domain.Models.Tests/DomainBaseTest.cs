namespace Prizma.Domain.Models.Tests
{
    using System;

    using Xunit;

    /// <summary>
    /// The domain base test.
    /// </summary>
    public partial class DomainBaseTest
    {
        /// <summary>
        /// The randomly generated id used to validate base domain model behavior.
        /// </summary>
        private readonly Guid id = Guid.NewGuid();

        /// <summary>
        /// The domain base class being tested (SUT).
        /// </summary>
        private readonly DomainBaseTestClass domainBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBaseTest"/> class.
        /// </summary>
        public DomainBaseTest()
        {
            this.domainBase = new DomainBaseTestClass(this.id);
        }

        [Fact(DisplayName = "Domain base class validate Id property returns expected value")]
        public void IdPropertyReturnsProvidedIdValue()
        {
            var idProperty = this.domainBase.Id;
            Assert.Equal(this.id, idProperty);
        }

        [Fact(DisplayName = "Domain base class UpdateTimeStamps when not previously set updates Created and Updated at values")]
        public void UpdateTimeStampsWhenNotPreviouslySetUpdatesCreatedAtAndUpdatedAt()
        {
            var initialCreatedAt = this.domainBase.CreatedAt;
            var initialUpdatedAt = this.domainBase.UpdatedAt;

            this.domainBase.UpdateTimeStamps();

            var updatedCreatedAt = this.domainBase.CreatedAt;
            var updatedUpdatedAt = this.domainBase.UpdatedAt;

            Assert.NotNull(updatedCreatedAt);
            Assert.NotNull(updatedUpdatedAt);
            Assert.NotEqual(initialCreatedAt, updatedCreatedAt);
            Assert.NotEqual(initialUpdatedAt, updatedUpdatedAt);
        }

        [Fact(DisplayName = "Domain base class UpdateTimeStamps when previously set updates Created and Updated at values")]
        public void UpdateTimeStampsWhenPreviouslySetUpdatesCreatedAtAndUpdatedAt()
        {
            this.domainBase.UpdateTimeStamps(); // initial set
            var initialCreatedAt = this.domainBase.CreatedAt;
            var initialUpdatedAt = this.domainBase.UpdatedAt;

            this.domainBase.UpdateTimeStamps();

            var updatedCreatedAt = this.domainBase.CreatedAt;
            var updatedUpdatedAt = this.domainBase.UpdatedAt;

            Assert.NotNull(updatedCreatedAt);
            Assert.NotNull(updatedUpdatedAt);
            Assert.Equal(initialCreatedAt, updatedCreatedAt);
            Assert.NotEqual(initialUpdatedAt, updatedUpdatedAt);
        }
    }
}