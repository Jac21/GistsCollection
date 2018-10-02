namespace MyFluentInterface.Conditions
{
    public interface ICanAddCondition
    {
        ICanAddWhereValue Where(string productModelName);
    }
}