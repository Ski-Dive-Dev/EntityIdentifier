using System;

namespace SkiDiveDev.EntityIdentifier
{
    public interface IIdentifier : IComparable
    {
        /// <summary>
        /// Indicates whether or not the Entity ID contains a valid ID.
        /// </summary>
        bool IDIsAssigned { get; }
    }
}
