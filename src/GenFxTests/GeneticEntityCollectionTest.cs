﻿using GenFx;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.GeneticEntityCollection and is intended
    ///to contain all GenFx.GeneticEntityCollection Unit Tests
    ///</summary>
    [TestClass()]
    public class EntityCollectionTest
    {
        /// <summary>
        /// Tests that the SortByFitness method works correctly.
        /// </summary>
        [TestMethod()]
        public void EntityCollection_SortByFitness()
        {
            ObservableCollection<IGeneticEntity> geneticEntities = new ObservableCollection<IGeneticEntity>();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Population = new MockPopulationConfiguration(),
                Entity = new MockEntityConfiguration()
            });

            for (int i = 9; i >= 0; i--)
            {
                MockEntity entity = new MockEntity(algorithm);
                entity.ScaledFitnessValue = Convert.ToDouble(i);
                geneticEntities.Add(entity);
            }

            IGeneticEntity[] sortedEntities = geneticEntities.GetEntitiesSortedByFitness(FitnessType.Scaled, FitnessEvaluationMode.Maximize).ToArray();
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(Convert.ToDouble(i), sortedEntities[i].ScaledFitnessValue, "Index {0}: Entity is not in correct position of list.", i.ToString());
            }

            sortedEntities = geneticEntities.GetEntitiesSortedByFitness(FitnessType.Scaled, FitnessEvaluationMode.Minimize).ToArray();
            int entityIndex = 0;
            for (int i = 9; i >= 0; i--)
            {
                Assert.AreEqual(Convert.ToDouble(i), sortedEntities[entityIndex].ScaledFitnessValue, "Index {0}: Entity is not in correct position of list.", i.ToString());
                entityIndex++;
            }
        }
    }
}
