﻿using GenFx;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.FitnessEvaluator and is intended
    /// to contain all GenFx.FitnessEvaluator Unit Tests
    /// </summary>
    [TestClass()]
    public class FitnessEvaluatorTest
    {

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void FitnessEvaluator_Ctor_NullAlgorithm()
        {
            MockFitnessEvaluator eval = new MockFitnessEvaluator();
            AssertEx.Throws<ArgumentNullException>(() => eval.Initialize(null));
        }

        /// <summary>
        /// Tests that the EvaluateFitness method works correctly.
        /// </summary>
        [TestMethod]
        public async Task FitnessEvaluator_EvaluateFitness_Async()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new MockPopulation(),
                GeneticEntitySeed = new MockEntity(),
                FitnessEvaluator = new FakeFitnessEvaluator2()
            };
            FakeFitnessEvaluator2 evaluator = new FakeFitnessEvaluator2();
            evaluator.Initialize(algorithm);
            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            double actualVal = await evaluator.EvaluateFitnessAsync(entity);

            Assert.AreEqual((double)99, actualVal, "Fitness was not evaluated correctly.");
        }

        private class FakeFitnessEvaluator : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        
        private class FakeFitnessEvaluator2 : FitnessEvaluator
        {
            public override Task<double> EvaluateFitnessAsync(GeneticEntity entity)
            {
                return Task.FromResult((double)99);
            }
        }
    }
}
