using MyFluentInterface.Entites;

namespace MyFluentInterface.Builders
{
    public class ConcreteBuilder : Builder
    {
        private readonly Product product = new Product();
        private int partCount;

        public override void BuildPart()
        {
            product.Parts.Add("Adding part #" + partCount++);
        }

        public override Product Construct()
        {
            return product;
        }
    }
}