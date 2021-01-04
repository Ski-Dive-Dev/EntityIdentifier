using System.Collections.Generic;
using System.Text;

namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// Used for describing an Entity ID having a <see cref="string"/> type.
    /// </summary>
    public class StringIdentifier : IdentifierBase<string>
    {
        protected StringIdentifier() : base(string.Empty)
        { /* No additional construction required. */ }

        protected StringIdentifier(string id) : base(id)
        { /* No additional construction required. */ }


        public static IIdentifier<string> Parse(string value) => new StringIdentifier(value);

        public static bool TryParse(string value, out IIdentifier<string> id)
        {
            id = new StringIdentifier(value);
            return true;
        }

        /// <summary>
        /// Indicates whether or not the Entity ID contains a valid ID.
        /// </summary>
        public override bool IDIsAssigned => !string.IsNullOrEmpty(ID);

        //public override string ToString() => ID;
    }
}
