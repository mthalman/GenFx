using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby the <see cref="IGeneticEntity"/> objects in a <see cref="IPopulation"/>
    /// are selected according to their fitness rank in comparison to the result of the <see cref="IPopulation"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Selection is then based on this ranking rather than on absolute fitness.  This technique avoids 
    /// selecting only a few of highly fit <see cref="IGeneticEntity"/> objects and thus can prevent premature 
    /// convergence.  But it also loses the perhaps important distinguishment of absolute fitness values 
    /// later in a run.  Use of a <see cref="IFitnessScalingStrategy"/> object does not have an impact 
    /// when <b>RankSelectionOperator</b> is being used since absolute differences in fitness are ignored.
    /// </para>
    /// </remarks>
    public class RankSelectionOperator : SelectionOperatorBase
    {
        /// <summary>
        /// Selects a <see cref="IGeneticEntity"/> from <paramref name="population"/> according to its
        /// fitness rank within the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            IGeneticEntity[] sortedEntities = population.Entities.GetEntitiesSortedByFitness(
                this.SelectionBasedOnFitnessType,
                this.Algorithm.FitnessEvaluator.EvaluationMode).ToArray();

            List<WheelSlice> wheelSlices = new List<WheelSlice>(sortedEntities.Length);
            for (int i = 0; i < sortedEntities.Length; i++)
            {
                wheelSlices.Add(new WheelSlice(sortedEntities[i], i + 1));
            }

            return RouletteWheelSampler.GetEntity(wheelSlices);
        }
    }
}
