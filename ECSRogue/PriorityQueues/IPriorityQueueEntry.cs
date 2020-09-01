namespace ECSRogue.PriorityQueues
{
    public interface IPriorityQueueEntry<TItem>
    {
        TItem Item { get; }
    }
}