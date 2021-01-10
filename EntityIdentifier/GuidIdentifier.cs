using System;

namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// Used for describing an Entity ID having a <see cref="Guid"/> type.
    /// </summary>
    public class GuidIdentifier : IdentifierBase<Guid>
    {
        static Guid defaultValue = Guid.Empty;

        public GuidIdentifier() : base(defaultValue)
        { /* No additional construction required. */ }

        public GuidIdentifier(Guid id) : base(id)
        { /* No additional construction required. */ }


        public static IIdentifier<Guid> Parse(string value)
        {
            if (Guid.TryParse(value, out var guidID))
            {
                return new GuidIdentifier(guidID);
            }
            else
            {
                throw new ArgumentException("The given ID could not be parsed into a GUID value.",
                    nameof(value));
            }
        }


        public static bool TryParse(string value, out GuidIdentifier id)
        {
            if (Guid.TryParse(value, out var guidID))
            {
                id = new GuidIdentifier(guidID);
                return true;
            }
            else
            {
                id = new GuidIdentifier(defaultValue);
                return false;
            }
        }


        /// <summary>
        /// Indicates whether or not the Entity ID contains a valid ID.
        /// </summary>
        public override bool IDIsAssigned => !ID.Equals(defaultValue);
    }
}
