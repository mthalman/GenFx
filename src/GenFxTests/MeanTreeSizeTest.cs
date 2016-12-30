﻿using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using GenFx.ComponentLibrary.Trees;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Statistics.MeanTreeSize and is intended
    /// to contain all GenFx.ComponentLibrary.Statistics.MeanTreeSize Unit Tests
    /// </summary>
    [TestClass()]
    public class MeanTreeSizeTest
    {
        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod()]
        public void MeanTreeSize_GetResultValue()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new TestTreeEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
            };
            config.Statistics.Add(new MeanTreeSizeStatisticConfiguration());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);

            ITreeEntity entity = new TestTreeEntity(algorithm);
            entity.SetRootNode(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            entity.RootNode.ChildNodes[0].ChildNodes.Add(new TreeNode());
            population.Entities.Add(entity);

            entity = new TestTreeEntity(algorithm);
            entity.SetRootNode(new TreeNode());
            population.Entities.Add(entity);

            entity = new TestTreeEntity(algorithm);
            entity.SetRootNode(new TreeNode());
            entity.RootNode.ChildNodes.Add(new TreeNode());
            population.Entities.Add(entity);

            object result = target.GetResultValue(population);

            Assert.AreEqual(2.33, Math.Round((double)result, 2), "Incorrect result value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void MeanTreeSize_GetResultValue_NullPopulation()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new TestTreeEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
            };
            config.Statistics.Add(new MeanTreeSizeStatisticConfiguration());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MeanTreeSizeStatistic target = new MeanTreeSizeStatistic(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        private class TestTreeEntity : TreeEntity<TestTreeEntity, TestTreeEntityConfiguration, TreeNode>
        {
            public TestTreeEntity(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {
            }

            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            protected override void InitializeCore()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class TestTreeEntityConfiguration : TreeEntityConfiguration<TestTreeEntityConfiguration, TestTreeEntity, TreeNode>
        {
        }
    }
}
