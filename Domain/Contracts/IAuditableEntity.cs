using System;

namespace Domain.Contracts
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IBaseEntity<TId>
    {

    }

    public interface IAuditableEntity : IBaseEntity
    {
        string CreatedBy { get; set; }

        DateTime CreatedOn { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModifiedOn { get; set; }
    }
}
