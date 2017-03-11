﻿using GenFx.UI.Controls;
using GenFx.UI.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExecutionPanel"/> class.
    /// </summary>
    [TestClass]
    public class ExecutionPanelTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_Ctor()
        {
            ExecutionPanel panel = new ExecutionPanel();
            Assert.AreEqual(4, panel.CommandBindings.Count);
            CollectionAssert.AreEquivalent(
                new RoutedCommand[]
                {
                    ExecutionPanel.StartExecutionCommand,
                    ExecutionPanel.StepExecutionCommand,
                    ExecutionPanel.PauseExecutionCommand,
                    ExecutionPanel.StopExecutionCommand,
                },
                panel.CommandBindings.Cast<CommandBinding>().Select(c => c.Command).ToList());            
        }

        /// <summary>
        /// Tests that the control responds correctly to the <see cref="ExecutionPanel.ExecutionContext"/> property being set.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_ExecutionContext()
        {
            ExecutionPanel panel = new ExecutionPanel();

            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            context.ExecutionState = ExecutionState.Running;
            panel.ExecutionContext = context;

            Assert.IsFalse(panel.CanStart);
            Assert.IsTrue(panel.CanStop);
            Assert.IsTrue(panel.CanPause);

            // Verify an event handler was added to respond to the context changing its ExecutionState
            context.ExecutionState = ExecutionState.Idle;
            Assert.IsTrue(panel.CanStart);
            Assert.IsFalse(panel.CanStop);
            Assert.IsFalse(panel.CanPause);
        }

        /// <summary>
        /// Tests that the control responds correctly to the <see cref="ExecutionPanel.ExecutionContext"/> property being set.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_ExecutionContext_Overwrite()
        {
            ExecutionPanel panel = new ExecutionPanel();

            Mock<GeneticAlgorithm> algorithmMock = new Mock<GeneticAlgorithm>();

            ExecutionContext context = new ExecutionContext(algorithmMock.Object);
            panel.ExecutionContext = context;
            
            // Start execution to force event handlers to be added in order to 
            // verify they are removed when replacing the ExecutionContext.
            ExecutionPanel.StartExecutionCommand.Execute(null, panel);
            DispatcherHelper.DoEvents();

            // Since the command kicks off an unawaitable Task, wait for just a second to let it finish.
            Thread.Sleep(1000);

            Mock<GeneticAlgorithm> algorithmMock2 = new Mock<GeneticAlgorithm>();
            ExecutionContext context2 = new ExecutionContext(algorithmMock2.Object);
            context2.ExecutionState = ExecutionState.Running;
            
            panel.ExecutionContext = context2;

            Assert.IsFalse(panel.CanStart);
            Assert.IsTrue(panel.CanStop);
            Assert.IsTrue(panel.CanPause);

            // Verify the panel does not respond to the original context changing
            context.ExecutionState = ExecutionState.Idle;
            Assert.IsFalse(panel.CanStart);
            Assert.IsTrue(panel.CanStop);
            Assert.IsTrue(panel.CanPause);

            // Verify the panel does respond to the new context changing
            context2.ExecutionState = ExecutionState.Idle;
            Assert.IsTrue(panel.CanStart);
            Assert.IsFalse(panel.CanStop);
            Assert.IsFalse(panel.CanPause);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StartExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StartExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.StartExecutionCommand.CanExecute(null, panel);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StartExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StartExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.StartExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Idle:
                    case ExecutionState.Paused:
                        Assert.IsTrue(result);
                        break;
                    default:
                        Assert.IsFalse(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StartExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StartExecutionCommand_Execute()
        {
            ExecutionPanel panel = new ExecutionPanel();
            panel.ExecutionContext = new ExecutionContext(CreateTestAlgorithm(true));

            ExecutionPanel.StartExecutionCommand.Execute(null, panel);
            DispatcherHelper.DoEvents();

            // Since the command kicks off an unawaitable Task, wait for just a second to let it finish.
            Thread.Sleep(50);

            Assert.AreEqual(ExecutionState.Running, panel.ExecutionContext.ExecutionState);

            // Trigger the algorithm to complete
            ((TestTerminator)panel.ExecutionContext.GeneticAlgorithm.Terminator).IsCompleteValue = true;

            // Wait for the algorithm to complete
            Thread.Sleep(50);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StepExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StepExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.StepExecutionCommand.CanExecute(null, panel);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StepExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StepExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.StepExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Idle:
                    case ExecutionState.Paused:
                        Assert.IsTrue(result);
                        break;
                    default:
                        Assert.IsFalse(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StepExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StepExecutionCommand_Execute()
        {
            ExecutionPanel panel = new ExecutionPanel();
            panel.ExecutionContext = new ExecutionContext(CreateTestAlgorithm(true));

            ExecutionPanel.StepExecutionCommand.Execute(null, panel);
            Thread.Sleep(50);
            DispatcherHelper.DoEvents();

            // Since the command kicks off an unawaitable Task, wait for just a second to let it finish.
            Thread.Sleep(50);
            DispatcherHelper.DoEvents();

            Assert.AreEqual(ExecutionState.Paused, panel.ExecutionContext.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.PauseExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_PauseExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.PauseExecutionCommand.CanExecute(null, panel);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.PauseExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_PauseExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.PauseExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Running:
                        Assert.IsTrue(result);
                        break;
                    default:
                        Assert.IsFalse(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.PauseExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_PauseExecutionCommand_Execute()
        {
            ExecutionPanel panel = new ExecutionPanel();
                
            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            panel.ExecutionContext.ExecutionState = ExecutionState.Running;

            ExecutionPanel.PauseExecutionCommand.Execute(null, panel);

            Assert.AreEqual(ExecutionState.PausePending, panel.ExecutionContext.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StopExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.StopExecutionCommand.CanExecute(null, panel);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StopExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.StopExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Running:
                    case ExecutionState.Paused:
                        Assert.IsTrue(result);
                        break;
                    default:
                        Assert.IsFalse(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StopExecutionCommand_Execute_FromRunning()
        {
            ExecutionPanel panel = new ExecutionPanel();

            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            panel.ExecutionContext.ExecutionState = ExecutionState.Running;

            ExecutionPanel.StopExecutionCommand.Execute(null, panel);

            Assert.AreEqual(ExecutionState.IdlePending, panel.ExecutionContext.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [TestMethod]
        public void ExecutionPanel_StopExecutionCommand_Execute_FromPaused()
        {
            ExecutionPanel panel = new ExecutionPanel();

            panel.ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            panel.ExecutionContext.ExecutionState = ExecutionState.Paused;

            ExecutionPanel.StopExecutionCommand.Execute(null, panel);

            Assert.AreEqual(ExecutionState.Idle, panel.ExecutionContext.ExecutionState);
        }

        private static GeneticAlgorithm CreateTestAlgorithm(bool runsInfinitely = false)
        {
            TestAlgorithm algorithm = new TestAlgorithm();

            algorithm.CrossoverOperator = new Mock<CrossoverOperator>(2).Object;
            algorithm.FitnessEvaluator = Mock.Of<FitnessEvaluator>();
            algorithm.GeneticEntitySeed = new TestEntity();
            algorithm.PopulationSeed = new TestPopulation();
            algorithm.SelectionOperator = Mock.Of<SelectionOperator>();

            algorithm.Terminator = new TestTerminator
            {
                IsCompleteValue = !runsInfinitely
            };

            return algorithm;
        }

        private class TestTerminator : Terminator
        {
            public bool IsCompleteValue { get; set; }

            public override bool IsComplete()
            {
                return this.IsCompleteValue;
            }
        }

        private class TestAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                return Task.FromResult(0);
            }
        }

        private class TestPopulation : Population
        {
        }

        private class TestEntity : GeneticEntity
        {
            public override string Representation
            {
                get
                {
                    return null;
                }
            }

            public override int CompareTo(GeneticEntity other)
            {
                throw new NotImplementedException();
            }
        }
    }
}