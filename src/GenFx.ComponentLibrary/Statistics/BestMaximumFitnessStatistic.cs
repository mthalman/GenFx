using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
    /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
    /// </summary>
    public class BestMaximumFitnessStatistic : Statistic
    {
        private Dictionary<int, double> bestMaxValues = new Dictionary<int, double>();
        
        /// <summary>
        /// Calculates to determine the highest <see cref="GeneticEntity.ScaledFitnessValue"/> 
        /// found for a <see cref="Population"/> during the entire run of the genetic algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
        /// <returns>Largest value of <see cref="GeneticEntity.ScaledFitnessValue"/> found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            double bestMax;
            if (!this.bestMaxValues.TryGetValue(population.Index, out bestMax))
            {
                bestMax = Double.MinValue;
                this.bestMaxValues.Add(population.Index, bestMax);
            }

            if (bestMax < population.ScaledMax)
            {
                bestMax = population.ScaledMax;
                this.bestMaxValues[population.Index] = bestMax;
            }

            return bestMax;
        }

        /// <summary>
        /// Sets the statistic's state.
        /// </summary>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(this.bestMaxValues)] = this.bestMaxValues;
        }

        /// <summary>
        /// Restores the statistic's state.
        /// </summary>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.bestMaxValues = (Dictionary<int, double>)state[nameof(this.bestMaxValues)];
        }
    }
}
