namespace Prizma.Domain.Models.Attributes
{
    using System;

    /// <summary>
    /// The non updateable attribute flags properties that should not be updateable when performing
    /// an entity update.
    /// </summary>
    internal class NonUpdateableAttribute : Attribute
    {
    }
}