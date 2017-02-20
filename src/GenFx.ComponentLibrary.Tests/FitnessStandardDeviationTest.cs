﻿using GenFx.ComponentLibrary.Metrics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="FitnessStandardDeviation"/> class.
    ///</summary>
    [TestClass]
    public class FitnessStandardDeviationTest
    {
        /// <summary>
        /// Tests that the <see cref="FitnessStandardDeviation.GetResultValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void FitnessStandardDeviation_GetResultValue_NoScaling()
        {
            FitnessStandardDeviation metric = new FitnessStandardDeviation();
            metric.Initialize(new MockGeneticAlgorithm());

            MockPopulation population = new MockPopulation();
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            populationAccessor.SetField("rawStandardDeviation", (double)18);
            object result = metric.GetResultValue(population);
            Assert.AreEqual((double)18, result);
        }

        /// <summary>
        /// Tests that the <see cref="FitnessStandardDeviation.GetResultValue"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void FitnessStandardDeviation_GetResultValue_WithScaling()
        {
            FitnessStandardDeviation metric = new FitnessStandardDeviation();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm { FitnessScalingStrategy = new MockFitnessScalingStrategy() };
            MeanFitness meanFitness = new MeanFitness();
            meanFitness.GetResults(0).Add(new MetricResult(0, 0, (double)12, meanFitness));
            algorithm.Metrics.Add(meanFitness);
            metric.Initialize(algorithm);

            MockPopulation population = new MockPopulation();
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 10 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 11 });
            population.Entities.Add(new MockEntity { ScaledFitnessValue = 15 });
            object result = metric.GetResultValue(population);
            Assert.AreEqual(2.16025, Math.Round((double)result, 5));
        }

        /// <summary>
        /// Tests that an exception is throw when a null population is passed.
        /// </summary>
        [TestMethod]
        public void FitnessStandardDeviation_GetResultValue_NullPopulation()
        {
            FitnessStandardDeviation metric = new FitnessStandardDeviation();
            AssertEx.Throws<ArgumentNullException>(() => metric.GetResultValue(null));
        }
    }
}
