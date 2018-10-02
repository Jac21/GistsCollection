using MyFluentInterface.Entites;

namespace MyFluentInterface.Conditions
{
    public interface ICanAddPartsOrWhereOrBuild
    {
        ICanAddWhereValue Where(string productModelName);
        ICanAddPartsOrWhereOrBuild AddEngine();
        ICanAddPartsOrWhereOrBuild AddSteeringWheel();
        ICanAddPartsOrWhereOrBuild AddTires(int numberOfTires);
        Product Build();
    }
}