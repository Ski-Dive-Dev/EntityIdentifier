using System;

namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// An interface which describes an Entity ID whose type is provided by the implementing class.
    /// </summary>
    /// <typeparam name="T">The underlying type of the ID.</typeparam>
    public interface IIdentifier<T> : IIdentifier where T : IEquatable<T>
    {
        T ID { get; }

        //IIdentifier<T> Parse(string value);

        //bool TryParse(string value, out IIdentifier<T> id);
    }
}
