using System;
using System.Linq.Expressions;

namespace PSE.Core.Specification;

internal sealed class IdentitySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}