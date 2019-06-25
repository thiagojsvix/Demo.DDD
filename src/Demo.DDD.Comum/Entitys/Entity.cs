using System.Diagnostics.CodeAnalysis;
using Demo.DDD.Shared.Validations;

namespace Demo.DDD.Shared.Entitys
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity: Validation
    {
        public long Id { get; protected set; }
    }
}
