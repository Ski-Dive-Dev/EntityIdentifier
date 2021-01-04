using System;
using System.Collections;
using System.Collections.Generic;

namespace SkiDiveDev.EntityIdentifier
{
    /// <summary>
    /// An abstract class to describe an Entity Identifier for loose coupling to its underlying type.
    /// </summary>
    /// <remarks>
    /// When an Entity ID inherits from this class with an underlying type, <typeparamref name="T"/>, code written
    /// to use that Entity ID is loosely coupled to <typeparamref name="T"/> -- the underlying type can be easily
    /// re-specified without requiring changes to code that rely on the Entity ID.
    /// </remarks>
    /// <typeparam name="T">The underlying type of the ID, which must implement <see cref="IEquatable{T}"/> so that
    /// two IDs can be compared to one another for equality, and must also implement <see cref="IComparable{T}"/>
    /// so that IDs can be sorted (which is only useful in user interfaces, but preferred by end-users.)
    /// </typeparam>
    public abstract class IdentifierBase<T> : IIdentifier<T>, IComparer where T : IEquatable<T>, IComparable<T>
    {
        protected IdentifierBase(T id)
        {
            ID = id;
        }

        public T ID { get; }


        /// <summary>
        /// Indicates whether or not the Entity ID contains a valid ID.
        /// </summary>
        public abstract bool IDIsAssigned { get; }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((T)ID).Equals(((IdentifierBase<T>)obj).ID);
        }


        public override int GetHashCode() => ID.GetHashCode();


        public static bool operator ==(IdentifierBase<T> a, IdentifierBase<T> b)
        {
            if (a is null)
            {
                var trueIfBothOperandsAreNull = (b is null);
                return trueIfBothOperandsAreNull;
            }
            else if (b is null)
            {
                return false;
            }

            return a.ID.Equals(b.ID);
        }

        public static bool operator !=(IdentifierBase<T> a, IdentifierBase<T> b) => !(a == b);


        [Obsolete("This operator encourages use of static typing, which is undesired.")]
        public static bool operator ==(IdentifierBase<T> a, T b)
        {
            if (a is null)
            {
                // https://stackoverflow.com/questions/65351/null-or-default-comparison-of-generic-argument-in-c-sharp
                return (EqualityComparer<T>.Default.Equals(b, default(T)));
            }
            else if (EqualityComparer<T>.Default.Equals(b, default(T)))
            {
                return false;
            }

            return a.ID.Equals(b);
        }

        [Obsolete("This operator encourages use of static typing, which is undesired.")]
        public static bool operator ==(T b, IdentifierBase<T> a)
        {
            if (a is null)
            {
                // https://stackoverflow.com/questions/65351/null-or-default-comparison-of-generic-argument-in-c-sharp
                return (EqualityComparer<T>.Default.Equals(b, default(T)));
            }
            else if (EqualityComparer<T>.Default.Equals(b, default(T)))
            {
                return false;
            }

            return a.ID.Equals(b);
        }

        [Obsolete("This operator encourages use of static typing, which is undesired.")]
        public static bool operator !=(IdentifierBase<T> a, T b) => !(a == b);

        [Obsolete("This operator encourages use of static typing, which is undesired.")]
        public static bool operator !=(T a, IdentifierBase<T> b) => !(a == b);


        public int Compare(object x, object y)
        {
            var a = (IdentifierBase<T>)x;
            var b = (IdentifierBase<T>)y;

            if (a == b)
            {
                return 0;
            }
            else if (a < b)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public int CompareTo(object other)
        {
            return Compare(this, (IdentifierBase<T>)other);
        }

 
        public static bool operator <(IdentifierBase<T> a, IdentifierBase<T> b)
        {
            if (a is null)
            {
                return (b is null);
            }
            else if (b is null)
            {
                return false;
            }

            return a.ID.CompareTo(b.ID) == -1;
        }

        public static bool operator >(IdentifierBase<T> a, IdentifierBase<T> b)
        {
            if (a is null)
            {
                return (b is null);
            }
            else if (b is null)
            {
                return false;
            }

            return a.ID.CompareTo(b.ID) == 1;
        }

        public static bool operator <(IdentifierBase<T> a, T b)
        {
            if (a is null)
            {
                return (EqualityComparer<T>.Default.Equals(b, default(T)));
            }
            else if ((EqualityComparer<T>.Default.Equals(b, default(T))))
            {
                return false;
            }

            return a.ID.CompareTo(b) == -1;
        }

        public static bool operator >(IdentifierBase<T> a, T b)
        {
            if (a is null)
            {
                return (EqualityComparer<T>.Default.Equals(b, default(T)));
            }
            else if ((EqualityComparer<T>.Default.Equals(b, default(T))))
            {
                return false;
            }

            return a.ID.CompareTo(b) == 1;
        }


        public override string ToString() => ID.ToString();
    }
}
