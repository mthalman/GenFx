using System;
using System.Collections.Generic;
using System.Text;
using GenFx;
using GenFx.ComponentLibrary.Base;

namespace GenFxTests.Mocks
{
    class MockEntity : GeneticEntity<MockEntity, MockEntityConfiguration>
    {
        internal string Identifier;

        public override string Representation
        {
            get { return this.Identifier; }
        }

        public MockEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void InitializeCore()
        {
            this.Identifier = "11111";
        }
        
        public override void CopyTo(MockEntity entity)
        {
            base.CopyTo(entity);
            ((MockEntity)entity).Identifier = this.Identifier;
        }
    }

    class MockEntityConfiguration : GeneticEntityConfiguration<MockEntityConfiguration, MockEntity>
    {
    }

    class MockEntity2 : GeneticEntity<MockEntity2, MockEntity2Configuration>
    {
        public override string Representation
        {
            get { throw new Exception(); }
        }

        public MockEntity2(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        protected override void InitializeCore()
        {
        }
        
        public override void CopyTo(MockEntity2 entity)
        {
            throw new Exception();
        }
    }

    class MockEntity2Configuration : GeneticEntityConfiguration<MockEntity2Configuration, MockEntity2>
    {
    }
}
