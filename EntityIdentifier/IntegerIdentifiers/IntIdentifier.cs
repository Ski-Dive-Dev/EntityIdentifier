using System;

namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// A Class to represent Entity IDs that are based on the 32-bit signed <see langref="int"/> type.
    /// </summary>
    public class IntIdentifier : IdentifierBase<int>
    {
        const int defaultValue = 0;

        public IntIdentifier() : base(defaultValue)
        { /* No additional construction required. */ }

        public IntIdentifier(int id) : base(id)
        { /* No additional construction required. */ }


        public static IIdentifier<int> Parse(string value)
        {
            if (int.TryParse(value, out var intID))
            {
                return new IntIdentifier(intID);
            }
            else
            {
                throw new ArgumentException("The given ID could not be parsed into an Int32 value.",
                    nameof(value));
            }
        }


        public static bool TryParse(string value, out IIdentifier<int> id)
        {
            if (int.TryParse(value, out var intID))
            {
                id = new IntIdentifier(intID);
                return true;
            }
            else
            {
                id = new IntIdentifier(defaultValue);
                return false;
            }
        }


        /// <summary>
        /// Indicates whether or not the Entity ID contains a valid ID.
        /// </summary>
        public override bool IDIsAssigned => !ID.Equals(defaultValue);
    }
}