namespace Scripts.Interfaces.Data
{
    public interface IAdvertismentData : IAdvertismentStatus, ISaveable
    {
        new bool IsAdHided { get; set; }
    }
}