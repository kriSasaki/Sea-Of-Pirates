namespace Project.Interfaces.Data
{
    public interface IStickyData : ISaveable
    {
        bool IsStickyActive { get; set; }
    }
}