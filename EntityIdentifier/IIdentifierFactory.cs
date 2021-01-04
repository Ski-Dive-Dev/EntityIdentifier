namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// An interface to describe a means to create an <see cref="IIdentifier"/> object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Useful for creating an Entity ID, whose type is inferred, when the underlying data store does not provide
    /// an ID for a new entity -- such as some mocks.
    /// </para><para>
    /// An <see cref="IIdentifierFactory"/> can be injected into a constructor or method when it is required to
    /// create an Entity ID whose type is not known until runtime.
    /// </para>
    /// </remarks>
    public interface IIdentifierFactory
    {
        IIdentifier CreateID();
    }
}
