using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Entity made up of a variable-length string of bits.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class VariableLengthBinaryStringEntity : BinaryStringEntity
    {
        private const int DefaultMaxLength = 20;
        private const int DefaultMinLength = 5;

        private int minimumStartingLength = DefaultMinLength;
        private int maximumStartingLength = DefaultMaxLength;

        /// <summary>
        /// Gets or sets the maximum starting length a new entity can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MaximumStartingLength
        {
            get { return this.maximumStartingLength; }
            set { this.SetProperty(ref this.maximumStartingLength, value); }
        }

        /// <summary>
        /// Gets or sets the minimum starting length a new entity can have.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MinimumStartingLength
        {
            get { return this.minimumStartingLength; }
            set { this.SetProperty(ref this.minimumStartingLength, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the list is a fixed size.
        /// </summary>
        public override bool IsFixedSize { get { return false; } }

        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// The length of an entity can be changed
        /// from its initial value.  The string will be truncated if the value is less than the current length.
        /// The string will be expanded with zeroes if the value is greater than the current length.
        /// </remarks>
        public override int Length
        {
            get { return base.Length; }
            set
            {
                if (value != this.Length)
                {
                    this.Genes.Length = value;
                    this.UpdateStringRepresentation();
                }
            }
        }

        /// <summary>
        /// Returns the initial length to use for the list.
        /// </summary>
        /// <returns>The initial length to use for the list.</returns>
        protected override int GetInitialLength()
        {
            if (this.MinimumStartingLength > this.MaximumStartingLength)
            {
                throw new ValidationException(
                    StringUtil.GetFormattedString(
                    Resources.ErrorMsg_MismatchedMinMaxValues,
                    nameof(this.MinimumStartingLength),
                    nameof(this.MaximumStartingLength)));
            }

            return RandomNumberService.Instance.GetRandomValue(this.MinimumStartingLength, this.MaximumStartingLength + 1);
        }
    }
}
