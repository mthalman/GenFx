using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Provides the base class for elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="ElitismStrategy"/> acts upon a <see cref="Population"/> to
    /// select those <see cref="GeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="ElitismStrategy.ElitistRatio"/> property value.
    /// </remarks>
    [DataContract]
    public abstract class ElitismStrategy : GeneticComponentWithAlgorithm
    {
        private const double DefaultElitistRatio = .1;

        [DataMember]
        private double elitistRatio = DefaultElitistRatio;

        /// <summary>
        /// Gets or sets the ratio of <see cref="GeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [DoubleValidator(MinValue = 0, MaxValue = 1)]
        public double ElitistRatio
        {
            get { return this.elitistRatio; }
            set { this.SetProperty(ref this.elitistRatio, value); }
        }

        /// <summary>
        /// Returns the collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>
        /// The collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> contains no entities.</exception>
        public IList<GeneticEntity> GetEliteEntities(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            if (population.Entities.Count == 0)
            {
                throw new ArgumentException(Resources.ErrorMsg_EntityListEmpty, nameof(population));
            }

            return this.GetEliteGeneticEntitiesCore(population);
        }

        /// <summary>
        /// Returns the collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>The collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.</returns>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to use the <see cref="ElitismStrategy.ElitistRatio"/>
        /// property to determine how many <see cref="GeneticEntity"/> objects are chosen to be elite.  Those <see cref="GeneticEntity"/>
        /// objects with the highest <see cref="GeneticEntity.ScaledFitnessValue"/> are chosen.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected virtual IList<GeneticEntity> GetEliteGeneticEntitiesCore(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            this.AssertIsInitialized();

            int elitistCount = Convert.ToInt32(Math.Round(this.ElitistRatio * population.Entities.Count));

            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            if (elitistCount > 0)
            {
                GeneticEntity[] sorted = population.Entities.GetEntitiesSortedByFitness(
                    this.Algorithm!.SelectionOperator!.SelectionBasedOnFitnessType,
                    this.Algorithm!.FitnessEvaluator!.EvaluationMode).ToArray();

                for (int i = sorted.Length - elitistCount; i < sorted.Length; i++)
                {
                    geneticEntities.Add(sorted[i]);
                }
            }

            return geneticEntities;
        }
    }
}
