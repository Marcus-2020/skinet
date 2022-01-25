using System.Linq.Expressions;

namespace Core.Entities.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria {get; }
    List<Expression<Func<T, object>>> Includes {get; }
    
}
