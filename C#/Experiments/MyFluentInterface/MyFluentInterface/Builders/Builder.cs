using MyFluentInterface.Entites;

namespace MyFluentInterface.Builders
{
    public abstract class Builder
    {
        public abstract void BuildPart();
        public abstract Product Construct();
    }
}