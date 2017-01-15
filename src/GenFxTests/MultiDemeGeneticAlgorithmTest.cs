﻿using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Algorithms.MultiDemeGeneticAlgorithm and is intended
    /// to contain all GenFx.ComponentLibrary.Algorithms.MultiDemeGeneticAlgorithm Unit Tests
    /// </summary>
    [TestClass()]
    public class MultiDemeGeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the CreateNextGeneration method works correctly.
        /// </summary>
        [TestMethod]
        public async Task MultiDemeGeneticAlgorithm_CreateNextGeneration_Async()
        {
            TestMultiDemeGeneticAlgorithm algorithm = new TestMultiDemeGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                MigrantCount = 1,
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                }
            };
            MockSelectionOperator selectionOp = new MockSelectionOperator();
            algorithm.SelectionOperator = selectionOp;
            await algorithm.InitializeAsync();

            PrivateObject accessor = new PrivateObject(algorithm);

            algorithm.Environment.Populations.Add(GetPopulation(algorithm));
            algorithm.Environment.Populations.Add(GetPopulation(algorithm));
            SimplePopulation population = GetPopulation(algorithm);
            algorithm.Environment.Populations.Add(population);

            int prevPopCount = population.Entities.Count;
            await (Task)accessor.Invoke("CreateNextGenerationAsync", population);

            Assert.AreEqual(4, selectionOp.DoSelectCallCount, "Selection not called correctly.");
            Assert.AreEqual(prevPopCount, population.Entities.Count, "New population not created correctly.");
        }

        /// <summary>
        /// Tests that the Migrate method works correctly.
        /// </summary>
        [TestMethod]
        public async Task MultiDemeGeneticAlgorithm_Migrate()
        {
            MultiDemeGeneticAlgorithm algorithm = new MultiDemeGeneticAlgorithm
            {
                EnvironmentSize = 3,
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation
                {
                    PopulationSize = 4
                },
                FitnessEvaluator = new MockFitnessEvaluator(),
                MigrantCount = 2,
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
            };

            algorithm.SelectionOperator = new MockSelectionOperator();
            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            await algorithm.InitializeAsync();

            SimplePopulation population1 = (SimplePopulation)algorithm.Environment.Populations[0];
            population1.Entities[0].ScaledFitnessValue = 1;
            population1.Entities[1].ScaledFitnessValue = 5;
            population1.Entities[2].ScaledFitnessValue = 2;
            population1.Entities[3].ScaledFitnessValue = 4;

            SimplePopulation population2 = (SimplePopulation)algorithm.Environment.Populations[1];
            population2.Entities[0].ScaledFitnessValue = 6;
            population2.Entities[1].ScaledFitnessValue = 3;
            population2.Entities[2].ScaledFitnessValue = 8;
            population2.Entities[3].ScaledFitnessValue = 7;

            SimplePopulation population3 = (SimplePopulation)algorithm.Environment.Populations[2];
            population3.Entities[0].ScaledFitnessValue = 9;
            population3.Entities[1].ScaledFitnessValue = 13;
            population3.Entities[2].ScaledFitnessValue = 10;
            population3.Entities[3].ScaledFitnessValue = 12;

            algorithm.Migrate();

            Assert.AreEqual((double)1, population1.Entities[0].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)2, population1.Entities[1].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)13, population1.Entities[2].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)12, population1.Entities[3].ScaledFitnessValue, "Incorrect entity.");

            Assert.AreEqual((double)6, population2.Entities[0].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)3, population2.Entities[1].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)5, population2.Entities[2].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)4, population2.Entities[3].ScaledFitnessValue, "Incorrect entity.");

            Assert.AreEqual((double)9, population3.Entities[0].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)10, population3.Entities[1].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)8, population3.Entities[2].ScaledFitnessValue, "Incorrect entity.");
            Assert.AreEqual((double)7, population3.Entities[3].ScaledFitnessValue, "Incorrect entity.");
        }

        private static SimplePopulation GetPopulation(IGeneticAlgorithm algorithm)
        {
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);

            for (int i = 0; i < 3; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            
            return population;
        }

        private class TestMultiDemeGeneticAlgorithm : MultiDemeGeneticAlgorithm
        {
            internal bool OnMigrateCalled;
            
            protected override void OnMigrate()
            {
                this.OnMigrateCalled = true;
            }
        }
    }
}
