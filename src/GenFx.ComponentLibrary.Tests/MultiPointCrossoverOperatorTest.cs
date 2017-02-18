﻿using GenFx.ComponentLibrary.Lists;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MultiPointCrossoverOperator"/> class.
    /// </summary>
    [TestClass]
    public class MultiPointCrossoverOperatorTest
    {
        [TestCleanup]
        public void TestCleanup()
        {
            RandomNumberService.Instance = new RandomNumberService();
        }

        /// <summary>
        /// Tests that an exception is thrown if the operator is configured incorrectly.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_Validation()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 3,
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity
                {
                    RequiresUniqueElementValues = true
                }
            };
            op.Initialize(algorithm);

            AssertEx.Throws<ValidationException>(() => op.Validate());
        }

        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method
        /// works correctly when crossing entities with the same length over two points.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_TwoPointsSameLength()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 2,
                CrossoverRate = 1
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity()
            };
            op.Initialize(algorithm);

            TestEntity entity1 = new TestEntity();
            entity1.InnerList.AddRange(new object[] { 0, 1, 2, 3, 4 });

            TestEntity entity2 = new TestEntity();
            entity2.InnerList.AddRange(new object[] { 4, 2, 1, 3, 0 });

            List<GeneticEntity> parents = new List<GeneticEntity>();
            parents.Add(entity1);
            parents.Add(entity2);

            RandomNumberService.Instance = new FakeRandomNumberService(5,
                new Queue<int>(new int[] { 1, 4 }));

            PrivateObject accessor = new PrivateObject(op);
            IEnumerable<GeneticEntity> result = (IEnumerable<GeneticEntity>)accessor.Invoke(
                "GenerateCrossover", parents);

            Assert.AreEqual(2, result.Count());
            TestEntity resultEntity1 = (TestEntity)result.ElementAt(0);
            TestEntity resultEntity2 = (TestEntity)result.ElementAt(1);

            CollectionAssert.AreEqual(new int[] { 0, 2, 1, 3, 4 }, resultEntity1);
            CollectionAssert.AreEqual(new int[] { 4, 1, 2, 3, 0 }, resultEntity2);
        }

        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method
        /// works correctly when crossing entities with different lengths over two points.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_TwoPointsDifferentLength()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 2,
                CrossoverRate = 1
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity()
            };
            op.Initialize(algorithm);

            TestEntity entity1 = new TestEntity();
            entity1.InnerList.AddRange(new object[] { 0, 1, 2, 3 });

            TestEntity entity2 = new TestEntity();
            entity2.InnerList.AddRange(new object[] { 4, 2, 1, 0, 0, 1, 2 });

            List<GeneticEntity> parents = new List<GeneticEntity>();
            parents.Add(entity1);
            parents.Add(entity2);

            RandomNumberService.Instance = new FakeRandomNumberService(4,
                new Queue<int>(new int[] { 2, 3 }));

            PrivateObject accessor = new PrivateObject(op);
            IEnumerable<GeneticEntity> result = (IEnumerable<GeneticEntity>)accessor.Invoke(
                "GenerateCrossover", parents);

            Assert.AreEqual(2, result.Count());
            TestEntity resultEntity1 = (TestEntity)result.ElementAt(0);
            TestEntity resultEntity2 = (TestEntity)result.ElementAt(1);

            CollectionAssert.AreEqual(new int[] { 0, 1, 1, 3 }, resultEntity1);
            CollectionAssert.AreEqual(new int[] { 4, 2, 2, 0, 0, 1, 2 }, resultEntity2);
        }

        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method
        /// works correctly when crossing entities with different lengths over three points.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_ThreePointsDifferentLength()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 3,
                CrossoverRate = 1
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity()
            };
            op.Initialize(algorithm);

            TestEntity entity1 = new TestEntity();
            entity1.InnerList.AddRange(new object[] { 0, 1, 2, 3, 4 });

            TestEntity entity2 = new TestEntity();
            entity2.InnerList.AddRange(new object[] { 4, 2, 1, 0, 0, 1, 2 });

            List<GeneticEntity> parents = new List<GeneticEntity>();
            parents.Add(entity1);
            parents.Add(entity2);

            RandomNumberService.Instance = new FakeRandomNumberService(5,
                new Queue<int>(new int[] { 1, 3, 4 }));

            PrivateObject accessor = new PrivateObject(op);
            IEnumerable<GeneticEntity> result = (IEnumerable<GeneticEntity>)accessor.Invoke(
                "GenerateCrossover", parents);

            Assert.AreEqual(2, result.Count());
            TestEntity resultEntity1 = (TestEntity)result.ElementAt(0);
            TestEntity resultEntity2 = (TestEntity)result.ElementAt(1);

            CollectionAssert.AreEqual(new int[] { 0, 2, 1, 3, 0, 1, 2 }, resultEntity1);
            CollectionAssert.AreEqual(new int[] { 4, 1, 2, 0, 4 }, resultEntity2);
        }

        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method
        /// works correctly when crossing entities with the same lengths over three points.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_ThreePointsSameLength()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 3,
                CrossoverRate = 1
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity()
            };
            op.Initialize(algorithm);

            TestEntity entity1 = new TestEntity();
            entity1.InnerList.AddRange(new object[] { 0, 1, 2, 3, 4 });

            TestEntity entity2 = new TestEntity();
            entity2.InnerList.AddRange(new object[] { 4, 2, 1, 0, 0 });

            List<GeneticEntity> parents = new List<GeneticEntity>();
            parents.Add(entity1);
            parents.Add(entity2);

            RandomNumberService.Instance = new FakeRandomNumberService(5,
                new Queue<int>(new int[] { 0, 3, 4 }));

            PrivateObject accessor = new PrivateObject(op);
            IEnumerable<GeneticEntity> result = (IEnumerable<GeneticEntity>)accessor.Invoke(
                "GenerateCrossover", parents);

            Assert.AreEqual(2, result.Count());
            TestEntity resultEntity1 = (TestEntity)result.ElementAt(0);
            TestEntity resultEntity2 = (TestEntity)result.ElementAt(1);

            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 3, 0 }, resultEntity1);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 0, 4 }, resultEntity2);
        }

        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method
        /// works correctly when crossing entities with the same length and unique values over two points.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_TwoPointsSameLength_UniqueValues()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 2,
                CrossoverRate = 1
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity
                {
                    RequiresUniqueElementValues = true
                }
            };
            op.Initialize(algorithm);

            TestEntity entity1 = new TestEntity();
            entity1.InnerList.AddRange(new object[] { 4, 0, 1, 2, 3 });

            TestEntity entity2 = new TestEntity();
            entity2.InnerList.AddRange(new object[] { 4, 2, 1, 3, 0 });

            List<GeneticEntity> parents = new List<GeneticEntity>();
            parents.Add(entity1);
            parents.Add(entity2);

            RandomNumberService.Instance = new FakeRandomNumberService(5,
                new Queue<int>(new int[] { 1, 4 }));

            PrivateObject accessor = new PrivateObject(op);
            IEnumerable<GeneticEntity> result = (IEnumerable<GeneticEntity>)accessor.Invoke(
                "GenerateCrossover", parents);

            Assert.AreEqual(2, result.Count());
            TestEntity resultEntity1 = (TestEntity)result.ElementAt(0);
            TestEntity resultEntity2 = (TestEntity)result.ElementAt(1);

            CollectionAssert.AreEqual(new int[] { 4, 2, 1, 3, 0 }, resultEntity1);
            CollectionAssert.AreEqual(new int[] { 4, 0, 1, 2, 3 }, resultEntity2);
        }

        /// <summary>
        /// Tests that the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method
        /// works correctly when crossing entities with different lengths and unique values over two points.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_TwoPointsDifferentLength_UniqueValues()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator
            {
                CrossoverPointCount = 2,
                CrossoverRate = 1
            };
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new TestEntity
                {
                    RequiresUniqueElementValues = true
                }
            };
            op.Initialize(algorithm);

            TestEntity entity1 = new TestEntity();
            entity1.InnerList.AddRange(new object[] { 0, 1, 2, 3 });

            TestEntity entity2 = new TestEntity();
            entity2.InnerList.AddRange(new object[] { 3, 5, 4, 1, 6, 2, 0 });

            List<GeneticEntity> parents = new List<GeneticEntity>();
            parents.Add(entity1);
            parents.Add(entity2);

            RandomNumberService.Instance = new FakeRandomNumberService(4,
                new Queue<int>(new int[] { 2, 3 }));

            PrivateObject accessor = new PrivateObject(op);
            IEnumerable<GeneticEntity> result = (IEnumerable<GeneticEntity>)accessor.Invoke(
                "GenerateCrossover", parents);

            Assert.AreEqual(2, result.Count());
            TestEntity resultEntity1 = (TestEntity)result.ElementAt(0);
            TestEntity resultEntity2 = (TestEntity)result.ElementAt(1);

            CollectionAssert.AreEqual(new int[] { 0, 1, 4, 3 }, resultEntity1);
            CollectionAssert.AreEqual(new int[] { 3, 5, 2, 1, 6, 4, 0 }, resultEntity2);
        }

        /// <summary>
        /// Tests that an exception is thrown when null parents are passed to the <see cref="MultiPointCrossoverOperator.GenerateCrossover"/> method.
        /// </summary>
        [TestMethod]
        public void MultiPointCrossoverOperator_GenerateCrossover_NullParents()
        {
            MultiPointCrossoverOperator op = new MultiPointCrossoverOperator();
            PrivateObject accessor = new PrivateObject(op);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("GenerateCrossover", (IList<GeneticEntity>)null));
        }
        
        private class FakeRandomNumberService : IRandomNumberService
        {
            private Queue<int> getRandomValueValues;
            private int expectedMaxValue;

            public FakeRandomNumberService(int expectedMaxValue, Queue<int> getRandomValueValues)
            {
                this.expectedMaxValue = expectedMaxValue;
                this.getRandomValueValues = getRandomValueValues;
            }

            public double GetDouble()
            {
                throw new NotImplementedException();
            }

            public int GetRandomValue(int maxValue)
            {
                Assert.AreEqual(this.expectedMaxValue, maxValue);
                return this.getRandomValueValues.Dequeue();
            }

            public int GetRandomValue(int minValue, int maxValue)
            {
                throw new NotImplementedException();
            }
        }

        private class TestEntity : ListEntityBase
        {
            public List<object> InnerList = new List<object>();

            public override bool IsFixedSize
            {
                get;
                set;
            }

            public override int Length
            {
                get { return this.InnerList.Count; }
                set
                {
                    if (value < this.InnerList.Count)
                    {
                        this.InnerList.RemoveRange(value, this.InnerList.Count - value);
                    }
                    else if (value > this.InnerList.Count)
                    {
                        this.InnerList.AddRange(Enumerable.Repeat<object>(null, value - this.InnerList.Count));
                    }
                }
            }

            public override bool RequiresUniqueElementValues
            {
                get;
                set;
            }

            public override object GetValue(int index)
            {
                return this.InnerList[index];
            }

            public override void SetValue(int index, object value)
            {
                this.InnerList[index] = value;
            }

            public override void CopyTo(GeneticEntity entity)
            {
                base.CopyTo(entity);

                ((TestEntity)entity).InnerList.AddRange(this.InnerList);
            }
        }
    }
}
