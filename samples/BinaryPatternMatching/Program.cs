﻿using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentLibrary.Terminators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinaryPatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAlgorithmAsync().Wait();
        }

        private static async Task RunAlgorithmAsync()
        {
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm
            {
                EnvironmentSize = 1,
                FitnessEvaluator = new FitnessEvaluator
                {
                    EvaluationMode = FitnessEvaluationMode.Minimize,
                    TargetBinary = "010101010101"
                },
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MaximumStartingLength = 10,
                    MinimumStartingLength = 5
                },
                PopulationSeed = new SimplePopulation
                {
                    PopulationSize = 100
                },
                SelectionOperator = new FitnessProportionateSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                },
                CrossoverOperator = new VariableSinglePointCrossoverOperator
                {
                    CrossoverRate = 0.8
                },
                MutationOperator = new BinaryStringMutationOperator
                {
                    MutationRate = 0.01
                },
                Terminator = new FitnessTargetTerminator
                {
                    FitnessTarget = 0,
                    FitnessValueType = FitnessType.Raw
                }
            };
            algorithm.GenerationCreated += Algorithm_GenerationCreated;
            await algorithm.InitializeAsync();
            await algorithm.RunAsync();

            IEnumerable<GeneticEntity> top10Entities =
                algorithm.Environment.Populations[0].Entities.GetEntitiesSortedByFitness(
                    FitnessType.Raw, FitnessEvaluationMode.Minimize)
                .Reverse()
                .Take(10);

            Console.WriteLine();
            Console.WriteLine("Top 10 entities:");
            foreach (GeneticEntity entity in top10Entities)
            {
                Console.WriteLine("Bits: " + entity.Representation + ", Fitness: " + entity.RawFitnessValue);
            }

            Console.ReadLine();
        }

        private static void Algorithm_GenerationCreated(object sender, EventArgs e)
        {
            Console.WriteLine("Generation: {0}", ((GeneticAlgorithm)sender).CurrentGeneration);
        }
    }
}
