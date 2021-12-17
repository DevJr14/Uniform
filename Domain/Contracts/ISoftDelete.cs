using System;

namespace Domain.Contracts
{
    public interface ISoftDelete
    {
        string DeletedBy { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
