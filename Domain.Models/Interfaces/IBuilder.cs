namespace Prizma.Domain.Models.Interfaces
{
    /// <summary>
    /// The Builder interface.
    /// </summary>
    /// <typeparam name="T">
    /// Type parameter of the class being built.
    /// </typeparam>
    public interface IBuilder<out T> where T : class
    {
        /// <summary>
        /// Builds a new class of type {T}.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/> built entity.
        /// </returns>
        T Build();
    }
}
