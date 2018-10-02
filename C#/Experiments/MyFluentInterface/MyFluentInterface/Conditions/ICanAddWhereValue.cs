namespace MyFluentInterface.Conditions
{
    public interface ICanAddWhereValue
    {
        ICanAddPartsOrWhereOrBuild IsEqualTo(object value);
        ICanAddPartsOrWhereOrBuild IsNotEqualTo(object value);
    }
}