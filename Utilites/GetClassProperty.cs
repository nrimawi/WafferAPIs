using System;
using System.Linq.Expressions;

namespace WafferAPIs.Utilites
{
    public class ClassProperty
    {
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
    }
}
