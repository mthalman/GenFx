using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the most basic type of genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <b>SimpleGeneticAlgorithm</b> can operate multiple <see cref="Population"/> objects but
    /// they run isolated from one another.
    /// </remarks>
    public class SimpleGeneticAlgorithm : GeneticAlgorithm
    {
        /// <summary>
        /// Modifies <paramref name="population"/> to become the next generation of <see cref="GeneticEntity"/> objects.
        /// </summary>
        /// <param name="population">The current <see cref="Population"/> to be modified.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override Task CreateNextGenerationAsync(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            IList<GeneticEntity> eliteGeneticEntities = this.ApplyElitism(population);

            int populationCount = population.Entities.Count;

            ObservableCollection<GeneticEntity> nextGeneration = new ObservableCollection<GeneticEntity>();

            foreach (GeneticEntity entity in eliteGeneticEntities)
            {
                nextGeneration.Add(entity);
                population.Entities.Remove(entity);
            }

            while (nextGeneration.Count != populationCount)
            {
                IList<GeneticEntity> childGeneticEntities = this.SelectGeneticEntitiesAndApplyCrossoverAndMutation(population);

                foreach (GeneticEntity entity in childGeneticEntities)
                {
                    if (nextGeneration.Count != populationCount)
                    {
                        nextGeneration.Add(entity);
                    }
                }
            }

            population.Entities.Clear();
            population.Entities.AddRange(nextGeneration);

            return Task.FromResult(true);
        }

        /// <summary>
        /// Returns whether the child <see cref="Population"/> has reached the limit of <see cref="GeneticEntity"/> objects.
        /// </summary>
        /// <param name="nextGeneration"><see cref="Population"/> to test.</param>
        /// <param name="previousGeneration"><see cref="Population"/> that is the previous generation.</param>
        /// <returns>true if the child population is full; otherwise, false.</returns>
        private static bool IsChildPopulationFull(ObservableCollection<GeneticEntity> nextGeneration, ObservableCollection<GeneticEntity> previousGeneration)
        {
            return (nextGeneration.Count >= previousGeneration.Count);
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="SimpleGeneticAlgorithm"/>.
    /// </summary>
    [Component(typeof(SimpleGeneticAlgorithm))]
    public class SimpleGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration
    {
    }
}
