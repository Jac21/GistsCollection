using MyFluentInterface.Builders;
using MyFluentInterface.Entites;

namespace MyFluentInterface.Directors
{
    public class Director
    {
        private readonly Builder builder = new ConcreteBuilder();

        public Product Construct()
        {
            for (int i = 0; i < 5; i++)
            {
                builder.BuildPart();
            }

            return builder.Construct();
        }
    }
}