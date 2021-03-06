using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// <see cref="GeneticEntity"/> made up of a string of bits.
    /// </summary>
    /// <remarks>This class uses a <see cref="BitArray"/> data structure to represent the list.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    [DataContract]
    public class BinaryStringEntity : ListEntityBase<bool>
    {
        [DataMember]
        private BitArray genes = new BitArray(0);

        [DataMember]
        private bool isFixedSize;

        /// <summary>
        /// Gets or sets a value indicating whether the list is a fixed size.
        /// </summary>
        [ConfigurationProperty]
        public override bool IsFixedSize
        {
            get { return this.isFixedSize; }
            set { this.SetProperty(ref this.isFixedSize, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether each of the element values should be unique for the entity.
        /// </summary>
        /// <remarks>
        /// <see cref="BinaryStringEntity"/> does not support the use of unique element values since there are only
        /// two element values available.
        /// </remarks>
        public override bool RequiresUniqueElementValues
        {
            get { return false; }
            set { throw new NotSupportedException(Resources.ErrorMsg_UseUniqueElementValuesNotSupported); }
        }

        /// <summary>
        /// Gets or sets the length of the binary string.
        /// </summary>
        /// <remarks>
        /// The length of this entity can be changed
        /// from its initial value.  The list will be truncated if the value is less than the current length.
        /// The list will be expanded with zeroes if the value is greater than the current length.
        /// </remarks>
        public override int Length
        {
            get
            {
                return this.genes.Count;
            }
            set
            {
                if (value != this.Length)
                {
                    if (this.IsFixedSize)
                    {
                        throw new ArgumentException(Resources.ErrorMsg_ListEntityLengthCannotBeChanged, nameof(value));
                    }

                    this.genes.Length = value;

                    this.UpdateStringRepresentation();
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="BitArray"/> containing the values of the binary string.
        /// </summary>
        protected BitArray Genes
        {
            get { return this.genes; }
        }

        /// <summary>
        /// Gets or sets the bit at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the bit to get or set.</param>
        public override bool this[int index]
        {
            get
            {
                return this.genes[index];
            }
            set
            {
                this.AssertIsInitialized();
                this.genes[index] = value;
                this.UpdateStringRepresentation();
            }
        }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        public override void Initialize(GeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

            this.genes = new BitArray(this.GetInitialLength());

            for (int i = 0; i < this.Length; i++)
            {
                this[i] = RandomNumberService.Instance.GetRandomValue(2) == 1 ? true : false;
            }

            this.UpdateStringRepresentation();

        }

        /// <summary>
        /// Copies the state from this object to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.AssertIsInitialized();
            base.CopyTo(entity);

            BinaryStringEntity binStrEntity = (BinaryStringEntity)entity;

            binStrEntity.genes = (BitArray)this.genes.Clone();
            binStrEntity.UpdateStringRepresentation();
        }
        
        /// <summary>
        /// Calculates the string representation of the entity.
        /// </summary>
        /// <returns>The string representation.</returns>
        protected override string CalculateStringRepresentation()
        {
            StringBuilder builder = new StringBuilder(this.Length);
            for (int i = 0; i < this.Length; i++)
            {
                builder.Append(this[i] ? "1" : "0");
            }

            return builder.ToString();
        }
    }
}
