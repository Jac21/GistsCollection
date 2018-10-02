using MyFluentInterface.Entites;

namespace MyFluentInterface.FluentBuilders
{
    public class FluentBuilder
    {
        private Product product;

        public FluentBuilder Begin()
        {
            product = new Product();
            return this;
        }

        public FluentBuilder Engine
        {
            get
            {
                product.Parts.Add("Engine");
                return this;
            }
        }

        public FluentBuilder SteeringWheel
        {
            get
            {
                product.Parts.Add("Steering Wheel");
                return this;
            }
        }

        public FluentBuilder Tire()
        {
            product.Parts.Add("Tire");
            return this;
        }

        public Product Build()
        {
            return product;
        }
    }
}