using System;

namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// Used for describing an Entity ID having a <see langref="ulong"/> type.
    /// </summary>
    public class UlongIdentifier : IdentifierBase<ulong>
    {
        const ulong defaultValue = 0;

        public UlongIdentifier() : base(defaultValue)
        { /* No additional construction required. */ }

        public UlongIdentifier(ulong id) : base(id)
        { /* No additional construction required. */ }


        public static IIdentifier<ulong> Parse(string value)
        {
            if (ulong.TryParse(value, out var ulongID))
            {
                return new UlongIdentifier(ulongID);
            }
            else
            {
                throw new ArgumentException("The given ID could not be parsed into an UInt64 value.",
                    nameof(value));
            }
        }


        public static bool TryParse(string value, out IIdentifier<ulong> id)
        {
            if (ulong.TryParse(value, out var ulongID))
            {
                id = new UlongIdentifier(ulongID);
                return true;
            }
            else
            {
                id = new UlongIdentifier(defaultValue);
                return false;
            }
        }


        /// <summary>
        /// Indicates whether or not the Entity ID contains a valid ID.
        /// </summary>
        public override bool IDIsAssigned => !ID.Equals(defaultValue);
    }
}