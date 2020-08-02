using System;

namespace DungeonsAndDungeons
{
    /// <summary>The Range class.</summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>Minimum value of the range.</summary>
        public T Minimum { get; set; }

        /// <summary>Maximum value of the range.</summary>
        public T Maximum { get; set; }

        private T _remaining;
        public T Remaining { get => _remaining; set => SetRemaining(value); }

        public Range(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
            Remaining = Maximum;

            if (!IsValid())
            {
                throw new ArgumentException($"Invalid Range. {Minimum} is not greater than {Maximum}");
            }
        }

        public Range(T minimum, T maximum, T remaining) : this(minimum, maximum)
        {
            Remaining = remaining;
        }

        /// <summary>Presents the Range in readable format.</summary>
        /// <returns>String representation of the Range</returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", this.Minimum, this.Maximum);
        }

        /// <summary>Determines if the range is valid.</summary>
        /// <returns>True if range is valid, else false</returns>
        public bool IsValid()
        {
            return this.Minimum.CompareTo(this.Maximum) < 0;
        }

        /// <summary>Determines if the provided value is inside the range.</summary>
        /// <param name="value">The value to test</param>
        /// <returns>True if the value is inside Range, else false</returns>
        public bool ContainsValue(T value)
        {
            return (this.Minimum.CompareTo(value) <= 0) && (value.CompareTo(this.Maximum) <= 0);
        }

        /// <summary>Determines if this Range is inside the bounds of another range.</summary>
        /// <param name="Range">The parent range to test on</param>
        /// <returns>True if range is inclusive, else false</returns>
        public bool IsInsideRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && range.ContainsValue(this.Minimum) && range.ContainsValue(this.Maximum);
        }

        /// <summary>Determines if another range is inside the bounds of this range.</summary>
        /// <param name="Range">The child range to test</param>
        /// <returns>True if range is inside, else false</returns>
        public bool ContainsRange(Range<T> range)
        {
            return this.IsValid() && range.IsValid() && this.ContainsValue(range.Minimum) && this.ContainsValue(range.Maximum);
        }

        public void FillRemaining()
        {
            Remaining = Maximum;
        }

        private void SetRemaining(T value)
        {
            if (value.CompareTo(Minimum) < 0)
            {
                _remaining = Minimum;
            }
            else if (value.CompareTo(Maximum) > 0)
            {
                _remaining = Maximum;
            }
            else
            {
                _remaining = value;
            }
        }

        public bool IsMinimum()
        {
            return Remaining.CompareTo(Minimum) <= 0;
        }

        public static implicit operator T(Range<T> r) => r.Remaining;

    }

}
