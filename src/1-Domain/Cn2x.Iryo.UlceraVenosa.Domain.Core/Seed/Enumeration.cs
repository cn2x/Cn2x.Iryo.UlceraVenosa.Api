using System;
using System.Reflection;

namespace Cn2x.Iryo.UlceraVenosa.Domain.Core
{
    public abstract class Enumeration<TYPE> : IComparable
    {
        public string Name { get; private set; }

        public TYPE Id { get; private set; }

        protected Enumeration(TYPE id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TYPE> =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration<TYPE> otherValue))
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration<TYPE> firstValue, Enumeration<TYPE> secondValue)
        {
            if (firstValue == null)
                throw new ArgumentNullException(nameof(firstValue));

            if (secondValue == null)
                throw new ArgumentNullException(nameof(secondValue));

            return Math.Abs(Comparer<TYPE>.Default.Compare(firstValue.Id, secondValue.Id));
        }

        public static T FromValue<T>(TYPE value) where T : Enumeration<TYPE>
        {
            var matchingItem = Parse<T, TYPE>(value, "value", item => item.Id.Equals(value));
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration<TYPE>
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration<TYPE>
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            var otherEnum = obj as Enumeration<TYPE>;
            if (otherEnum == null)
                throw new ArgumentException("Objeto não é uma Enumeration<TYPE>");

            return Comparer<TYPE>.Default.Compare(Id, otherEnum.Id);
        }

        public static implicit operator Enumeration<TYPE>(TYPE  t) {
            return Enumeration<TYPE>.FromValue<Enumeration<TYPE>>(t);
        }

        public static implicit operator TYPE(Enumeration<TYPE> t) {
            return t.Id;
        }
    }
}