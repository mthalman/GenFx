﻿using GenFx.UI.ViewModels;
using GenFx.Validation;
using Moq;
using System;
using System.Threading.Tasks;
using TestCommon;
using TestCommon.Helpers;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExecutionPanelViewModel"/> class.
    /// </summary>
    public class ExecutionPanelViewModelTest
    {
        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.CanStartExecution"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_CanStartExecution()
        {
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                context.ExecutionState = enumVal;

                switch (enumVal)
                {
                    case ExecutionState.Idle:
                    case ExecutionState.Paused:
                        Assert.True(viewModel.CanStartExecution());
                        break;
                    default:
                        Assert.False(viewModel.CanStartExecution());
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.CanStopExecution"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_CanStopExecution()
        {
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                context.ExecutionState = enumVal;

                switch (enumVal)
                {
                    case ExecutionState.Running:
                    case ExecutionState.Paused:
                        Assert.True(viewModel.CanStopExecution());
                        break;
                    default:
                        Assert.False(viewModel.CanStopExecution());
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.CanPauseExecution"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_CanPauseExecution()
        {
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                context.ExecutionState = enumVal;

                switch (enumVal)
                {
                    case ExecutionState.Running:
                        Assert.True(viewModel.CanPauseExecution());
                        break;
                    default:
                        Assert.False(viewModel.CanPauseExecution());
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StartExecutionAsync"/> method works correctly.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StartExecutionAsync_FromIdle()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            Task task = Task.Run(async () => await viewModel.StartExecutionAsync());

            // Wait for the algorithm to begin execution
            await Task.Delay(50);

            Assert.Equal(ExecutionState.Running, context.ExecutionState);

            // Trigger the algorithm to complete
            ((TestTerminator)algorithm.Terminator).IsCompleteValue = true;

            // Wait for the algorithm to finish
            await Task.Delay(50);

            Assert.Equal(ExecutionState.Idle, context.ExecutionState);
            Assert.True(task.IsCompleted);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StepExecutionAsync"/> method works correctly.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StepExecutionAsync_FromIdle()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            Task task = Task.Run(async () => await viewModel.StepExecutionAsync());

            // Wait for the algorithm to finish
            await Task.Delay(50);

            Assert.Equal(ExecutionState.Paused, context.ExecutionState);
            Assert.True(task.IsCompleted);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StepExecutionAsync"/> method works correctly.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StepExecutionAsync_FromIdle_ToCompletion()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            ((TestTerminator)algorithm.Terminator).IsCompleteValue = true;

            Task task = Task.Run(async () => await viewModel.StepExecutionAsync());

            // Wait for the algorithm to finish
            await Task.Delay(50);

            Assert.Equal(ExecutionState.Idle, context.ExecutionState);
            Assert.True(task.IsCompleted);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StartExecutionAsync"/> method works correctly.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StartExecutionAsync_FromPaused()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            Task task = Task.Run(async () => await viewModel.StartExecutionAsync());

            // Wait for the algorithm to begin execution
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Running, context.ExecutionState);

            GeneticEnvironment environment = algorithm.Environment;

            context.ExecutionState = ExecutionState.PausePending;

            // Wait for the algorithm to become paused
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Paused, context.ExecutionState);

            // Resume execution
            task = Task.Run(async () => await viewModel.StartExecutionAsync());

            // Wait for the algorithm to begin execution
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Running, context.ExecutionState);

            // Verify InitializeAsync was not called
            Assert.Same(environment, algorithm.Environment);

            // Trigger the algorithm to complete
            ((TestTerminator)algorithm.Terminator).IsCompleteValue = true;

            // Wait for the algorithm to finish
            await Task.Delay(50);

            Assert.Equal(ExecutionState.Idle, context.ExecutionState);
            Assert.True(task.IsCompleted);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StepExecutionAsync"/> method works correctly.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StepExecutionAsync_FromPaused()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            Task task = Task.Run(async () => await viewModel.StepExecutionAsync());

            // Wait for the algorithm to finish execution
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Paused, context.ExecutionState);

            GeneticEnvironment environment = algorithm.Environment;

            // Resume execution
            task = Task.Run(async () => await viewModel.StepExecutionAsync());

            // Wait for the algorithm to begin finish
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Paused, context.ExecutionState);

            // Verify InitializeAsync was not called
            Assert.Same(environment, algorithm.Environment);
            Assert.True(task.IsCompleted);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StepExecutionAsync"/> method works correctly.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StepExecutionAsync_FromPaused_ToCompletion()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            Task task = Task.Run(async () => await viewModel.StepExecutionAsync());

            // Wait for the algorithm to finish execution
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Paused, context.ExecutionState);

            GeneticEnvironment environment = algorithm.Environment;

            // Trigger the algorithm to complete
            ((TestTerminator)algorithm.Terminator).IsCompleteValue = true;

            // Resume execution
            task = Task.Run(async () => await viewModel.StepExecutionAsync());

            // Wait for the algorithm to finish
            await Task.Delay(50);
            Assert.Equal(ExecutionState.Idle, context.ExecutionState);

            // Verify InitializeAsync was not called
            Assert.Same(environment, algorithm.Environment);
            Assert.True(task.IsCompleted);
        }

        /// <summary>
        /// Tests that an exception is handled correctly when thrown from <see cref="GeneticAlgorithm.InitializeAsync"/>.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StartExecutionAsync_ExceptionOnInitialize()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            // Use a population that will cause a validation exception
            algorithm.PopulationSeed = new TestPopulation2();

            await Assert.ThrowsAsync<ValidationException>(() => viewModel.StartExecutionAsync());
            Assert.IsType<ValidationException>(context.AlgorithmException);
        }

        /// <summary>
        /// Tests that an exception is handled correctly when thrown from <see cref="GeneticAlgorithm.InitializeAsync"/>.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StepExecutionAsync_ExceptionOnInitialize()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            // Use a population that will cause a validation exception
            algorithm.PopulationSeed = new TestPopulation2();

            await Assert.ThrowsAsync<ValidationException>(() => viewModel.StepExecutionAsync());
            Assert.IsType<ValidationException>(context.AlgorithmException);
        }

        /// <summary>
        /// Tests that an exception is handled correctly when thrown from <see cref="GeneticAlgorithm.RunAsync"/>.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StartExecutionAsync_ExceptionOnRun()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            // Use a terminator that will cause an exception
            algorithm.Terminator = new TestTerminator2();

            await Assert.ThrowsAsync<NotSupportedException>(() => viewModel.StartExecutionAsync());
            Assert.IsType<NotSupportedException>(context.AlgorithmException);
        }

        /// <summary>
        /// Tests that an exception is handled correctly when thrown from <see cref="GeneticAlgorithm.StepAsync"/>.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_StepExecutionAsync_ExceptionOnRun()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);
            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            // Use a terminator that will cause an exception
            algorithm.Terminator = new TestTerminator2();

            await Assert.ThrowsAsync<NotSupportedException>(() => viewModel.StepExecutionAsync());
            Assert.IsType<NotSupportedException>(context.AlgorithmException);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StopExecution"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_StopExecution_FromRunningState()
        {
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            context.ExecutionState = ExecutionState.Running;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            viewModel.StopExecution();

            Assert.Equal(ExecutionState.IdlePending, context.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.StopExecution"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_StopExecution_FromPausedState()
        {
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            context.ExecutionState = ExecutionState.Paused;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            viewModel.StopExecution();

            Assert.Equal(ExecutionState.Idle, context.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.PauseExecution"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_PauseExecution()
        {
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            context.ExecutionState = ExecutionState.Running;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                context.ExecutionState = enumVal;
                viewModel.PauseExecution();

                Assert.Equal(ExecutionState.PausePending, context.ExecutionState);
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanelViewModel.Dispose"/> method works correctly.
        /// </summary>
        [Fact]
        public void ExecutionPanelViewModel_Dispose()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm();
            
            ExecutionContext context = new ExecutionContext(algorithm);

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);
            context.ExecutionState = ExecutionState.Running;

            viewModel.Dispose();

            // Verify the ExecutionState is not reset when the genetic algorithm completes.
            PrivateObject algorithmAccessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            algorithmAccessor.Invoke("OnAlgorithmCompleted");
            Assert.Equal(ExecutionState.Running, context.ExecutionState);

            // Verify the view model has unsubscribed from the FitnessEvaluated event.
            context.ExecutionState = ExecutionState.PausePending;
            EnvironmentFitnessEvaluatedEventArgs eventArgs = new EnvironmentFitnessEvaluatedEventArgs(new GeneticEnvironment(algorithm), 0);
            algorithmAccessor.Invoke("OnFitnessEvaluated", eventArgs);
            Assert.False(eventArgs.Cancel);
        }

        /// <summary>
        /// Tests that the correct action is taken when the <see cref="GeneticAlgorithm.AlgorithmCompleted"/> event is raised.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_AlgorithmCompleted()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm();
            
            ExecutionContext context = new ExecutionContext(algorithm);

            context.ExecutionState = ExecutionState.Idle;

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            int propertyChangedCallCount = 0;
            context.PropertyChanged += (sender, args) =>
            {
                propertyChangedCallCount++;
                switch (propertyChangedCallCount)
                {
                    case 1:
                        Assert.Equal(ExecutionState.Running, context.ExecutionState);
                        break;
                    case 2:
                        Assert.Equal(ExecutionState.Idle, context.ExecutionState);
                        break;
                    default:
                        break;
                }
            };

            await viewModel.StartExecutionAsync();

            Assert.Equal(2, propertyChangedCallCount);
        }

        /// <summary>
        /// Tests that the correct action is taken when the <see cref="GeneticAlgorithm.FitnessEvaluated"/> event is raised.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_FitnessEvaluated_IdlePending()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            Task task = Task.Run(async () => await viewModel.StartExecutionAsync());

            await Task.Delay(50); // Wait just a bit so the algorithm can start executing
            context.ExecutionState = ExecutionState.IdlePending;
            await Task.Delay(50); // Wait for the IdlePending to take effect on the executing algorithm.

            Assert.True(task.IsCompleted);
            Assert.Equal(ExecutionState.Idle, context.ExecutionState);
        }

        /// <summary>
        /// Tests that the correct action is taken when the <see cref="GeneticAlgorithm.FitnessEvaluated"/> event is raised.
        /// </summary>
        [Fact]
        public async Task ExecutionPanelViewModel_FitnessEvaluated_PausePending()
        {
            GeneticAlgorithm algorithm = CreateTestAlgorithm(true);

            ExecutionContext context = new ExecutionContext(algorithm);

            ExecutionPanelViewModel viewModel = new ExecutionPanelViewModel(context);

            Task task = Task.Run(async () => await viewModel.StartExecutionAsync());

            await Task.Delay(50); // Wait just a bit so the algorithm can start executing
            context.ExecutionState = ExecutionState.PausePending;
            await Task.Delay(50); // Wait for the PausePending to take effect on the executing algorithm.

            PrivateObject algorithmAccessor = new PrivateObject(algorithm);

            Assert.True(task.IsCompleted);
            Assert.Equal(ExecutionState.Paused, context.ExecutionState);
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

        private class TestTerminator2 : Terminator
        {
            public override bool IsComplete()
            {
                throw new NotSupportedException();
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

        [RequiredTerminator(typeof(TestTerminator2))]
        private class TestPopulation2 : Population
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
