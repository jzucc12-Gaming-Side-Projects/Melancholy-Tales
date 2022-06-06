namespace CFR.ITINERARY
{
    public interface IItineraryElement
    {
        void SaveState();
        void HoldCurrentState();
        void RestoreCurrentState();
        void ShowState(int _index);
        void DeleteOldStates(int _limit);
        void RevertHeldState(int _index);
    }
}
