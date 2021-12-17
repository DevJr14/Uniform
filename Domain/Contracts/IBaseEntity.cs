namespace Domain.Contracts
{
    public interface IBaseEntity<TId> : IBaseEntity
    {
        public TId Id { get; set; }
    }
    public interface IBaseEntity
    {
    }
}
