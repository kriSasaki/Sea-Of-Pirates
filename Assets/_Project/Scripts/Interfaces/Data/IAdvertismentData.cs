namespace Scripts.Interfaces.Data
{
    public interface IAdvertismentData : IAdvertismentStatus, ISaveable
    {
        void RemoveAd();
    }
}