﻿using GenFx;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// This is a test class for GenFx.GeneticAlgorithm and is intended
    /// to contain all GenFx.GeneticAlgorithm Unit Tests
    /// </summary>
    [TestClass()]
    public class GeneticAlgorithmTest
    {
        /// <summary>
        /// Tests that the InitializeAsync initializes the state correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_InitializeAsync()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            await algorithm.InitializeAsync();

            Assert.IsNotNull(algorithm.Environment, "Environment not initialized.");
            PrivateObject accessor = new PrivateObject(algorithm.Environment);
            Assert.AreSame(algorithm, accessor.GetField("algorithm"), "Environment should be initialized with the algorithm.");

            Assert.IsInstanceOfType(algorithm.Terminator, typeof(DefaultTerminator));
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoCrossover_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                //CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoElitism_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                //ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessEvaluator_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                //FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoFitnessScalingStrategy_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                //FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoEntityType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                //GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoMutationOperatorType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                //MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoPopulationType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                //PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());
            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoSelectionOperatorType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                //SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            algorithm.Metrics.Add(new MockMetric());

            await TestInitializeAsync(algorithm, true);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoMetricType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };
            //algorithm.Metrics.Add(new MockMetric());

            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Initialize method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Initialize_NoTerminatorType_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
            };
            algorithm.Metrics.Add(new MockMetric());

            await TestInitializeAsync(algorithm);
        }

        /// <summary>
        /// Tests that the Run method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Async()
        {
            int eventHandlerCallCount = 0;
            TestGeneticAlgorithm algorithm = GetAlgorithm();
            await algorithm.InitializeAsync();
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate (object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventHandlerCallCount++;
            });
            await algorithm.RunAsync();

            Assert.AreEqual(3, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(3, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(800, ((MockFitnessEvaluator)algorithm.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(8, ((MockFitnessScalingStrategy)algorithm.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run twice without initializing in between.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_Twice_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = GetAlgorithm();

            await algorithm.InitializeAsync();
            await algorithm.RunAsync();
            await AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.RunAsync());
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run without first calling Initialize.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Run_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };

            await AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.RunAsync());
        }

        /// <summary>
        /// Tests that the Step method works correctly.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Async()
        {
            TestGeneticAlgorithm algorithm = GetAlgorithm();

            int eventHandlerCallCount = 0;
            await algorithm.InitializeAsync();
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate (object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventHandlerCallCount++;
            });
            bool stepResult = await algorithm.StepAsync();

            Assert.IsFalse(stepResult, "Algorithm should not be complete yet.");

            Assert.AreEqual(1, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(1, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(400, ((MockFitnessEvaluator)algorithm.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(4, ((MockFitnessScalingStrategy)algorithm.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");

            stepResult = await algorithm.StepAsync();
            Assert.IsFalse(stepResult, "Algorithm should not be complete yet.");

            stepResult = await algorithm.StepAsync();
            Assert.IsTrue(stepResult, "Algorithm should be complete.");

            Assert.AreEqual(3, eventHandlerCallCount, "GenerationCreated event not called enough times.");
            Assert.AreEqual(3, algorithm.CurrentGeneration, "Algorithm did not run for enough generations.");
            Assert.AreEqual(0, ((MockCrossoverOperator)algorithm.CrossoverOperator).DoCrossoverCallCount, "Crossover call count not correct.");
            Assert.AreEqual(0, ((MockElitismStrategy)algorithm.ElitismStrategy).GetElitistGeneticEntitiesCallCount, "Elitism call count not correct.");
            Assert.AreEqual(800, ((MockFitnessEvaluator)algorithm.FitnessEvaluator).DoEvaluateFitnessCallCount, "FitnessEvaluator call count not correct.");
            Assert.AreEqual(8, ((MockFitnessScalingStrategy)algorithm.FitnessScalingStrategy).OnScaleCallCount, "FitnessScaling call count not correct.");
            Assert.AreEqual(0, ((MockMutationOperator)algorithm.MutationOperator).DoMutateCallCount, "Mutation call count not correct.");
            Assert.AreEqual(0, ((MockSelectionOperator)algorithm.SelectionOperator).DoSelectCallCount, "Selection call count not correct.");
        }

        /// <summary>
        /// Tests that an exception is thrown when Step is called too many times with no initialize in between.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_Overbounds_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = GetAlgorithm();

            await algorithm.InitializeAsync();
            bool result = await algorithm.StepAsync();
            Assert.IsFalse(result, "Algorithm should not be complete.");

            result = await algorithm.StepAsync();
            Assert.IsFalse(result, "Algorithm should not be complete.");

            result = await algorithm.StepAsync();
            Assert.IsTrue(result, "Algorithm should be complete.");

            await AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.StepAsync());
        }

        /// <summary>
        /// Tests that an exception is thrown when calling Run without first calling Initialize.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_Step_NoInitialize_Async()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };

            await AssertEx.ThrowsAsync<InvalidOperationException>(async () => await algorithm.StepAsync());
        }

        /// <summary>
        /// Tests that the ApplyElitism method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyElitism()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator
                {
                    EvaluationMode = FitnessEvaluationMode.Maximize
                },
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation()
            };

            TestApplyElitism(algorithm, 0);

            algorithm.ElitismStrategy = new MockElitismStrategy
            {
                ElitistRatio = .1
            };
            algorithm.ElitismStrategy.Initialize(algorithm);
            TestApplyElitism(algorithm, 1);
        }

        private void TestApplyElitism(TestGeneticAlgorithm algorithm, int expectedEliteCount)
        {
            PrivateObject accessor = new PrivateObject(algorithm);
            MockPopulation population = new MockPopulation();
            population.Initialize(algorithm);
            for (int i = 0; i < 10; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                population.Entities.Add(entity);
            }
            IList<GeneticEntity> entities = (IList<GeneticEntity>)accessor.Invoke("ApplyElitism", population);
            Assert.AreEqual(expectedEliteCount, entities.Count);
        }

        /// <summary>
        /// Tests that the ApplyCrossover method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyCrossover()
        {
            GeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };

            MockCrossoverOperator crossoverOp = (MockCrossoverOperator)algorithm.CrossoverOperator;
            crossoverOp.Initialize(algorithm);

            algorithm.CrossoverOperator = crossoverOp;

            PrivateObject algAccessor = new PrivateObject(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            IList<GeneticEntity> geneticEntities = (IList<GeneticEntity>)algAccessor.Invoke("ApplyCrossover", algorithm.PopulationSeed, new GeneticEntity[] { entity1, entity2 });

            Assert.AreEqual(2, geneticEntities.Count, "Incorrect number of genetic entities returned.");
            Assert.AreEqual(1, crossoverOp.DoCrossoverCallCount, "Crossover not called correctly.");
        }

        /// <summary>
        /// Tests that the ApplyMutation method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyMutation()
        {
            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity()
            };

            TestApplyMutation(algorithm, 0);

            algorithm.MutationOperator = new MockMutationOperator
            {
                MutationRate = .01
            };
            TestApplyMutation(algorithm, 3);
        }

        private void TestApplyMutation(TestGeneticAlgorithm algorithm, int expectedMutationCount)
        {
            PrivateObject accessor = new PrivateObject(algorithm);
            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();

            for (int i = 0; i < 3; i++)
            {
                MockEntity entity = new MockEntity();
                entity.Initialize(algorithm);
                geneticEntities.Add(entity);
            }

            IList<GeneticEntity> mutants = (IList<GeneticEntity>)accessor.Invoke("ApplyMutation", geneticEntities);

            Assert.AreEqual(geneticEntities.Count, mutants.Count, "Incorrect number of genetic entities returned.");
            if (algorithm.MutationOperator != null)
            {
                MockMutationOperator mutationOp = (MockMutationOperator)algorithm.MutationOperator;
                Assert.AreEqual(expectedMutationCount, mutationOp.DoMutateCallCount, "Mutation not called correctly.");
            }
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new CrossoverDependentEntity(),
                PopulationSeed = new MockPopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };

            this.ValidateComponent(algorithm.GeneticEntitySeed, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidCrossover()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new CrossoverDependentEntity2(),
                PopulationSeed = new MockPopulation(),
                CrossoverOperator = new MockCrossoverOperator
                {
                    CrossoverRate = 1
                }
            };

            this.ValidateComponent(algorithm.GeneticEntitySeed, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidElitism()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                ElitismStrategy = new MockElitismStrategy
                {
                    ElitistRatio = 1
                },
                CrossoverOperator = new ElitismDependentCrossover()
            };

            this.ValidateComponent(algorithm.CrossoverOperator, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidElitism()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                ElitismStrategy = new MockElitismStrategy
                {
                    ElitistRatio = 1
                },
                CrossoverOperator = new ElitismDependentCrossover2()
            };

            this.ValidateComponent(algorithm.CrossoverOperator, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new FitnessEvaluatorDependentSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator()
            };

            this.ValidateComponent(algorithm.SelectionOperator, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidFitnessEvaluator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new FitnessEvaluatorDependentSelectionOperator2(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
            };

            this.ValidateComponent(algorithm.SelectionOperator, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                MutationOperator = new FitnessScalingStrategyDependentMutationOperator()
            };

            this.ValidateComponent(algorithm.MutationOperator, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidFitnessScalingStrategy()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                MutationOperator = new FitnessScalingStrategyDependentMutationOperator2()
            };

            this.ValidateComponent(algorithm.MutationOperator, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new EntityDependentPopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidEntity()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new EntityDependentPopulation2()
            };

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MutationOperatorDependentFitnessEvaluation(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            this.ValidateComponent(algorithm.FitnessEvaluator, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidMutationOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MutationOperatorDependentFitnessEvaluator2(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                MutationOperator = new MockMutationOperator
                {
                    MutationRate = 1
                }
            };

            this.ValidateComponent(algorithm.FitnessEvaluator, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new PopulationDependentFitnessScaling()
            };

            this.ValidateComponent(algorithm.FitnessScalingStrategy, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessScalingStrategy = new PopulationDependentFitnessScaling2()
            };

            this.ValidateComponent(algorithm.FitnessScalingStrategy, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                Terminator = new SelectionOperatorDependentTerminator()
            };

            this.ValidateComponent(algorithm.Terminator, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidSelectionOperator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Scaled
                },
                Terminator = new SelectionOperatorDependentTerminator2()
            };

            this.ValidateComponent(algorithm.Terminator, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidMetricType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MetricDependentPopulation(),
            };
            algorithm.Metrics.Add(new MockMetric());
            algorithm.Metrics.Add(new MockMetric2());

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidMetricType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MetricDependentPopulation2(),
            };
            algorithm.Metrics.Add(new MockMetric());

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidPluginType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new PluginDependentPopulation(),
            };
            algorithm.Plugins.Add(new MockPlugin());
            algorithm.Plugins.Add(new MockPlugin2());

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidPluginType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new PluginDependentPopulation2(),
            };
            algorithm.Plugins.Add(new MockPlugin());

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_ValidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentPopulation(),
                Terminator = new MockTerminator()
            };

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, false);
        }

        /// <summary>
        /// Tests that an exception is throw when an required configurable type has not been set on the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_InvalidTerminator()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentPopulation2(),
                Terminator = new MockTerminator()
            };

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, true);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly when overriding a required type of a base class.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_OverrideRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentDerivedPopulation(),
                Terminator = new MockTerminator2()
            };

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, false);
        }

        /// <summary>
        /// Tests that the ValidateRequiredComponents method works correctly.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ValidateConfiguration_UsingBaseTypeAsRequiredType()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new TerminatorDependentPopulation3(),
                Terminator = new MockTerminator3() // uses derived type of the required type
            };

            this.ValidateComponent(algorithm.PopulationSeed, algorithm, false);
        }

        /// <summary>
        /// Tests that GeneticAlgorithmConfiguration.EnvironmentSize can be set to valid value.
        ///</summary>
        [TestMethod()]
        public void EnvironmentSizeTest_Valid()
        {
            MockGeneticAlgorithm target = new MockGeneticAlgorithm();
            int val = 2;
            target.MinimumEnvironmentSize = val;

            Assert.AreEqual(val, target.MinimumEnvironmentSize, "EnvironmentSize was not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when GeneticAlgorithmConfiguration.EnvironmentSize is set to 
        /// an invalid value.
        ///</summary>
        [TestMethod()]
        public void EnvironmentSizeTest_Invalid()
        {
            MockGeneticAlgorithm target = new MockGeneticAlgorithm();
            int val = 0;
            AssertEx.Throws<ValidationException>(() => target.MinimumEnvironmentSize = val);
        }

        /// <summary>
        /// Tests that the <see cref="GeneticAlgorithm"/> can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_Serialization()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                CrossoverOperator = new MockCrossoverOperator(),
                ElitismStrategy = new MockElitismStrategy(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                FitnessScalingStrategy = new MockFitnessScalingStrategy(),
                GeneticEntitySeed = new MockEntity(),
                MinimumEnvironmentSize = 10,
                MutationOperator = new MockMutationOperator(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                Terminator = new MockTerminator()
            };

            algorithm.Metrics.Add(new MockMetric());
            algorithm.Metrics.Add(new MockMetric2());

            algorithm.Plugins.Add(new MockPlugin());
            algorithm.Plugins.Add(new MockPlugin2());

            MockGeneticAlgorithm result = (MockGeneticAlgorithm)SerializationHelper.TestSerialization(
                algorithm, algorithm.GetAllComponents().Select(c => c.GetType()));

            Assert.AreEqual(10, result.MinimumEnvironmentSize);
            Assert.IsInstanceOfType(result.CrossoverOperator, typeof(MockCrossoverOperator));
            Assert.IsInstanceOfType(result.ElitismStrategy, typeof(MockElitismStrategy));
            Assert.IsInstanceOfType(result.FitnessEvaluator, typeof(MockFitnessEvaluator));
            Assert.IsInstanceOfType(result.FitnessScalingStrategy, typeof(MockFitnessScalingStrategy));
            Assert.IsInstanceOfType(result.GeneticEntitySeed, typeof(MockEntity));
            Assert.IsInstanceOfType(result.MutationOperator, typeof(MockMutationOperator));
            Assert.IsInstanceOfType(result.PopulationSeed, typeof(MockPopulation));
            Assert.IsInstanceOfType(result.SelectionOperator, typeof(MockSelectionOperator));
            Assert.IsInstanceOfType(result.Terminator, typeof(MockTerminator));

            Assert.IsInstanceOfType(result.Metrics[0], typeof(MockMetric));
            Assert.IsInstanceOfType(result.Metrics[1], typeof(MockMetric2));

            Assert.IsInstanceOfType(result.Plugins[0], typeof(MockPlugin));
            Assert.IsInstanceOfType(result.Plugins[1], typeof(MockPlugin2));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed to <see cref="GeneticAlgorithm.ApplyCrossover"/>.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyCrossover_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            AssertEx.Throws<ArgumentNullException>(() => algorithm.TestApplyCrossover(null, new List<GeneticEntity>()));
        }

        /// <summary>
        /// Tests that an exception is thrown when null parents are passed to <see cref="GeneticAlgorithm.ApplyCrossover"/>.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyCrossover_NullParents()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            AssertEx.Throws<ArgumentNullException>(() => algorithm.TestApplyCrossover(new MockPopulation(), null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed to <see cref="GeneticAlgorithm.ApplyElitism"/>.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyElitism_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            AssertEx.Throws<ArgumentNullException>(() => algorithm.TestApplyElitism(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when null parents are passed to <see cref="GeneticAlgorithm.ApplyMutation"/>.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_ApplyMutation_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();

            AssertEx.Throws<ArgumentNullException>(() => algorithm.TestApplyMutation(null));
        }

        /// <summary>
        /// Tests that external configuration properties are validated when the algorithm is initialized.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_ExternalConfigurationPropertyValidation()
        {
            CustomGeneticAlgorithm algorithm = new CustomGeneticAlgorithm
            {
                CrossoverOperator = new CustomCrossoverOperator(),
                ElitismStrategy = new CustomElitismStrategy(),
                FitnessEvaluator = new CustomFitnessEvaluator(),
                FitnessScalingStrategy = new CustomFitnessScalingStrategy(),
                GeneticEntitySeed = new CustomGeneticEntity(),
                MutationOperator = new CustomMutationOperator(),
                PopulationSeed = new CustomPopulation(),
                SelectionOperator = new CustomSelectionOperator(),
                Terminator = new CustomTerminator()
            };

            algorithm.Plugins.Add(new CustomPlugin());
            algorithm.Metrics.Add(new CustomMetric());

            // Ensure the algorithm can be initialized without error
            await algorithm.InitializeAsync();

            foreach (ICustomComponent component in algorithm.GetAllComponents())
            {
                await TestExternalConfigurationPropertyValidation(algorithm, component);
            }

            await TestExternalConfigurationPropertyValidation(algorithm, algorithm);
        }

        /// <summary>
        /// Tests that the <see cref="GeneticAlgorithm.AlgorithmCompleted"/> event is raised correctly
        /// when the user manually "completes" the algorithm.
        /// </summary>
        [TestMethod]
        public void GeneticAlgorithm_AlgorithmCompletedEvent_Manual()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            AlgorithmCompletedPlugin plugin = new AlgorithmCompletedPlugin();
            algorithm.Plugins.Add(plugin);
            plugin.Initialize(algorithm);

            bool eventRaised = false;
            algorithm.AlgorithmCompleted += (sender, eventObj) =>
            {
                eventRaised = true;
            };

            algorithm.Complete();

            Assert.IsTrue(eventRaised);
            Assert.IsTrue(plugin.OnAlgorithmCompletedWasCalled);
        }

        /// <summary>
        /// Tests that the <see cref="GeneticAlgorithm.AlgorithmCompleted"/> event is raised correctly
        /// when the algorithm naturally finishes its execution via the terminator.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_AlgorithmCompletedEvent_ViaNaturalCompletion()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                Terminator = new MockTerminator()
            };
            AlgorithmCompletedPlugin plugin = new AlgorithmCompletedPlugin();
            algorithm.Plugins.Add(plugin);

            await algorithm.InitializeAsync();
            await algorithm.RunAsync();

            bool eventRaised = false;
            algorithm.AlgorithmCompleted += (sender, eventObj) =>
            {
                eventRaised = true;
            };

            algorithm.Complete();

            Assert.IsTrue(eventRaised);
            Assert.IsTrue(plugin.OnAlgorithmCompletedWasCalled);
        }

        /// <summary>
        /// Tests that metrics are sorted according to dependencies.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_InitializeAsync_SortMetrics()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            Metric1 metric1;
            Metric2 metric2;
            Metric3 metric3;
            Metric4 metric4;
            Metric5 metric5;

            algorithm.Metrics.AddRange(new Metric[]
            {
                metric1 = new Metric1(),
                metric2 = new Metric2(),
                metric3 = new Metric3(),
                metric4 = new Metric4(),
                metric5 = new Metric5(),
            });

            await algorithm.InitializeAsync();

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            List<Metric> metrics = (List<Metric>)accessor.GetField("sortedMetrics");

            CollectionAssert.AreEqual(new Metric[]
            {
                metric5,
                metric3,
                metric2,
                metric4,
                metric1
            }, metrics);
        }

        /// <summary>
        /// Tests that an exception is thrown when a cycle is detected while sorting metrics.
        /// </summary>
        [TestMethod]
        public async Task GeneticAlgorithm_InitializeAsync_SortMetrics_Cycle()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity()
            };

            algorithm.Metrics.AddRange(new Metric[]
            {
                new CycleMetric1(),
                new CycleMetric2(),
                new CycleMetric3(),
            });

            await AssertEx.ThrowsAsync<InvalidOperationException>(() => algorithm.InitializeAsync());
        }

        private async Task TestExternalConfigurationPropertyValidation(GeneticAlgorithm algorithm, ICustomComponent component)
        {
            component.MyProperty = -1;
            await AssertEx.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
            component.MyProperty = 0;
        }

        private void ValidateComponent(GeneticComponentWithAlgorithm component, GeneticAlgorithm algorithm, bool validationExceptionExpected)
        {
            component.Initialize(algorithm);

            if (validationExceptionExpected)
            {
                AssertEx.Throws<ValidationException>(() => component.Validate());
            }
            else
            {
                component.Validate();
            }
        }

        private static async Task<GeneticAlgorithm> TestInitializeAsync(MockGeneticAlgorithm algorithm, bool initializeExceptionExpected = false)
        {
            bool eventCalled = false;
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(delegate (object sender, EnvironmentFitnessEvaluatedEventArgs args)
            {
                eventCalled = true;
            });

            if (initializeExceptionExpected)
            {
                await AssertEx.ThrowsAsync<ValidationException>(() => algorithm.InitializeAsync());
                return null;
            }
            else
            {
                await algorithm.InitializeAsync();
            }

            Assert.AreEqual(0, algorithm.CurrentGeneration, "Generation should be initialized.");

            Assert.AreEqual(algorithm.MinimumEnvironmentSize, algorithm.Environment.Populations.Count, "Environment not initialized correctly.");
            Assert.AreEqual(algorithm.PopulationSeed.MinimumPopulationSize, algorithm.Environment.Populations[0].Entities.Count, "Population not initialized correctly.");

            MockEntity entity = (MockEntity)algorithm.Environment.Populations[0].Entities[0];
            double entityId = Double.Parse(entity.Identifier);
            Assert.AreEqual(entityId, entity.RawFitnessValue, "Entity fitness was not evaluated.");
            if (algorithm.Metrics.Count > 0)
                Assert.IsTrue(((MockMetric)algorithm.Metrics.OfType<MockMetric>().FirstOrDefault()).MetricEvaluated, "Metrics were not evaluated.");
            Assert.IsTrue(eventCalled, "GenerationCreated event was not raised.");
            return algorithm;
        }

        private TestGeneticAlgorithm GetAlgorithm()
        {
            int environmentSize = 2;
            int populationSize = 100;

            TestGeneticAlgorithm algorithm = new TestGeneticAlgorithm();
            algorithm.MinimumEnvironmentSize = environmentSize;

            MockPopulation popConfig = new MockPopulation();
            popConfig.MinimumPopulationSize = populationSize;
            algorithm.PopulationSeed = popConfig;

            MockCrossoverOperator crossConfig = new MockCrossoverOperator();
            crossConfig.CrossoverRate = .7;
            algorithm.CrossoverOperator = crossConfig;

            MockElitismStrategy eliteConfig = new MockElitismStrategy();
            eliteConfig.ElitistRatio = .1;
            algorithm.ElitismStrategy = eliteConfig;

            MockMutationOperator mutConfig = new MockMutationOperator();
            mutConfig.MutationRate = .01;
            algorithm.MutationOperator = mutConfig;

            MockSelectionOperator selConfig = new MockSelectionOperator();
            selConfig.SelectionBasedOnFitnessType = FitnessType.Scaled;
            algorithm.SelectionOperator = selConfig;

            algorithm.FitnessEvaluator = new MockFitnessEvaluator();
            algorithm.FitnessScalingStrategy = new MockFitnessScalingStrategy();
            algorithm.GeneticEntitySeed = new MockEntity();
            algorithm.Metrics.Add(new MockMetric());
            algorithm.Terminator = new TestTerminator();
            return algorithm;
        }

        [RequiredCrossoverOperator(typeof(MockCrossoverOperator))]
        private class CrossoverDependentEntity : GeneticEntity
        {
            public override string Representation
            {
                get
                {
                    return null;
                }
            }
        }

        [RequiredCrossoverOperator(typeof(MockCrossoverOperator2))]
        private class CrossoverDependentEntity2 : GeneticEntity
        {
            public override string Representation
            {
                get
                {
                    return null;
                }
            }
        }

        [RequiredElitismStrategy(typeof(MockElitismStrategy))]
        private class ElitismDependentCrossover : CrossoverOperator
        {
            public ElitismDependentCrossover() : base(2)
            {
            }

            protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredElitismStrategy(typeof(MockElitismStrategy2))]
        private class ElitismDependentCrossover2 : CrossoverOperator
        {
            public ElitismDependentCrossover2() : base(2)
            {
            }

            protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessEvaluator(typeof(MockFitnessEvaluator))]
        private class FitnessEvaluatorDependentSelectionOperator : SelectionOperator
        {
            protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessEvaluator(typeof(MockFitnessEvaluator2))]
        private class FitnessEvaluatorDependentSelectionOperator2 : SelectionOperator
        {
            protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessScalingStrategy(typeof(MockFitnessScalingStrategy))]
        private class FitnessScalingStrategyDependentMutationOperator : MutationOperator
        {
            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredFitnessScalingStrategy(typeof(MockFitnessScalingStrategy2))]
        private class FitnessScalingStrategyDependentMutationOperator2 : MutationOperator
        {
            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredGeneticAlgorithm(typeof(MockGeneticAlgorithm))]
        private class GeneticAlgorithmDependentClass
        {
        }

        [RequiredGeneticAlgorithm(typeof(MockGeneticAlgorithm2))]
        private class GeneticAlgorithmDependentClass2
        {
        }

        [RequiredGeneticEntity(typeof(MockEntity))]
        private class EntityDependentPopulation : Population
        {
        }

        [RequiredGeneticEntity(typeof(MockEntity2))]
        private class EntityDependentPopulation2 : Population
        {
        }

        [RequiredMutationOperator(typeof(MockMutationOperator))]
        private class MutationOperatorDependentFitnessEvaluation : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredMutationOperator(typeof(MockMutationOperator2))]
        private class MutationOperatorDependentFitnessEvaluator2 : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredPopulation(typeof(MockPopulation))]
        private class PopulationDependentFitnessScaling : FitnessScalingStrategy
        {
            protected override void UpdateScaledFitnessValues(Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredPopulation(typeof(MockPopulation2))]
        private class PopulationDependentFitnessScaling2 : FitnessScalingStrategy
        {
            protected override void UpdateScaledFitnessValues(Population population)
            {
                throw new NotImplementedException();
            }
        }

        [RequiredSelectionOperator(typeof(MockSelectionOperator))]
        private class SelectionOperatorDependentTerminator : Terminator
        {
            public override bool IsComplete()
            {
                throw new NotImplementedException();
            }
        }

        [RequiredSelectionOperator(typeof(MockSelectionOperator2))]
        private class SelectionOperatorDependentTerminator2 : Terminator
        {
            public override bool IsComplete()
            {
                throw new NotImplementedException();
            }
        }

        [RequiredMetric(typeof(MockMetric))]
        private class MetricDependentPopulation : Population
        {
        }

        [RequiredMetric(typeof(MockMetric2))]
        private class MetricDependentPopulation2 : Population
        {
        }

        [RequiredPlugin(typeof(MockPlugin))]
        private class PluginDependentPopulation : Population
        {
        }

        [RequiredPlugin(typeof(MockPlugin2))]
        private class PluginDependentPopulation2 : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentPopulation : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator2))]
        private class TerminatorDependentPopulation2 : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator2Base))]
        private class TerminatorDependentPopulation3 : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator))]
        private class TerminatorDependentBasePopulation : Population
        {
        }

        [RequiredTerminator(typeof(MockTerminator2))]
        private class TerminatorDependentDerivedPopulation : TerminatorDependentBasePopulation
        {
        }

        private class TestTerminator : Terminator
        {
            public override bool IsComplete()
            {
                return this.Algorithm.CurrentGeneration == 3;
            }
        }

        private class TestGeneticAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                return Task.FromResult(true);
            }
        }

        private class FakeValidationMutationOperator : MutationOperator
        {
            private int value = -1;

            [ConfigurationProperty]
            [IntegerValidator(MinValue = 0)]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeMutationOperator : MutationOperator
        {
            private int value;
            private double value2;

            [ConfigurationProperty]
            public double Value2
            {
                get { return this.value2; }
                set { this.value2 = value; }
            }

            [ConfigurationProperty]
            public int Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [IntegerExternalValidator(typeof(FakeMutationOperator), "Value")]
        [CustomExternalPropertyValidator(typeof(FakeMutationOperator), "Value", typeof(FakeValidator))]
        [DoubleExternalValidator(typeof(FakeMutationOperator), "Value2")]
        private class FakeExternalValidatorTerminator : Terminator
        {
            public override bool IsComplete()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class FakeValidator : PropertyValidator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;
                return true;
            }
        }

        private interface ICustomComponent
        {
            int MyProperty { get; set; }
        }

        [IntegerExternalValidator(typeof(CustomElitismStrategy), "MyProperty", MinValue = 0)]
        private class CustomCrossoverOperator : CrossoverOperator, ICustomComponent
        {
            public CustomCrossoverOperator() : base(2)
            {
            }

            public int MyProperty { get; set; }

            protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
            {
                throw new NotImplementedException();
            }
        }

        [IntegerExternalValidator(typeof(CustomFitnessScalingStrategy), "MyProperty", MinValue = 0)]
        private class CustomElitismStrategy : ElitismStrategy, ICustomComponent
        {
            public int MyProperty { get; set; }
        }

        [IntegerExternalValidator(typeof(CustomFitnessEvaluator), "MyProperty", MinValue = 0)]
        private class CustomFitnessScalingStrategy : FitnessScalingStrategy, ICustomComponent
        {
            public int MyProperty { get; set; }

            protected override void UpdateScaledFitnessValues(Population population)
            {
            }
        }

        [IntegerExternalValidator(typeof(CustomGeneticAlgorithm), "MyProperty", MinValue = 0)]
        private class CustomFitnessEvaluator : FitnessEvaluator, ICustomComponent
        {
            public int MyProperty { get; set; }

            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                return Task.FromResult<double>(0);
            }
        }

        [IntegerExternalValidator(typeof(CustomGeneticEntity), "MyProperty", MinValue = 0)]
        private class CustomGeneticAlgorithm : GeneticAlgorithm, ICustomComponent
        {
            public int MyProperty { get; set; }

            protected override Task CreateNextGenerationAsync(Population population)
            {
                throw new NotImplementedException();
            }
        }

        [IntegerExternalValidator(typeof(CustomMutationOperator), "MyProperty", MinValue = 0)]
        private class CustomGeneticEntity : GeneticEntity, ICustomComponent
        {
            public int MyProperty { get; set; }

            public override string Representation
            {
                get
                {
                    return null;
                }
            }
        }

        [IntegerExternalValidator(typeof(CustomPlugin), "MyProperty", MinValue = 0)]
        private class CustomMutationOperator : MutationOperator, ICustomComponent
        {
            public int MyProperty { get; set; }

            protected override bool GenerateMutation(GeneticEntity entity)
            {
                throw new NotImplementedException();
            }
        }

        [IntegerExternalValidator(typeof(CustomPopulation), "MyProperty", MinValue = 0)]
        private class CustomPlugin : Plugin, ICustomComponent
        {
            public int MyProperty { get; set; }
        }

        [IntegerExternalValidator(typeof(CustomSelectionOperator), "MyProperty", MinValue = 0)]
        private class CustomPopulation : Population, ICustomComponent
        {
            public int MyProperty { get; set; }
        }

        [IntegerExternalValidator(typeof(CustomMetric), "MyProperty", MinValue = 0)]
        private class CustomSelectionOperator : SelectionOperator, ICustomComponent
        {
            public int MyProperty { get; set; }

            protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
            {
                throw new NotImplementedException();
            }
        }

        [IntegerExternalValidator(typeof(CustomTerminator), "MyProperty", MinValue = 0)]
        private class CustomMetric : Metric, ICustomComponent
        {
            public int MyProperty { get; set; }

            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [IntegerExternalValidator(typeof(CustomCrossoverOperator), "MyProperty", MinValue = 0)]
        private class CustomTerminator : Terminator, ICustomComponent
        {
            public int MyProperty { get; set; }

            public override bool IsComplete()
            {
                throw new NotImplementedException();
            }
        }

        private class AlgorithmCompletedPlugin : Plugin
        {
            public bool OnAlgorithmCompletedWasCalled = false;

            protected override void OnAlgorithmCompleted()
            {
                this.OnAlgorithmCompletedWasCalled = true;
                base.OnAlgorithmCompleted();
            }
        }

        [RequiredMetric(typeof(Metric2))]
        private class Metric1 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [RequiredMetric(typeof(Metric3))]
        private class Metric2 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [RequiredMetric(typeof(Metric5))]
        private class Metric3 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [RequiredMetric(typeof(Metric3))]
        private class Metric4 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        private class Metric5 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [RequiredMetric(typeof(CycleMetric2))]
        private class CycleMetric1 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [RequiredMetric(typeof(CycleMetric3))]
        private class CycleMetric2 : Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }

        [RequiredMetric(typeof(CycleMetric1))]
        private class CycleMetric3: Metric
        {
            public override object GetResultValue(Population population)
            {
                return 0;
            }
        }
    }
}
