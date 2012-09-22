using System;
using System.Collections;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace SortedArray.sample
{
    public class SortedArrayResolver : ISubDependencyResolver
    {
        private readonly bool allowEmptyCollections;
        private readonly Func<object, object, int> _orderFunc;
        private readonly IKernel kernel;

        public SortedArrayResolver(IKernel kernel, bool allowEmptyCollections = false, Func<object, object, int> orderFunc = null)
        {
            this.kernel = kernel;
            this.allowEmptyCollections = allowEmptyCollections;
            _orderFunc = orderFunc;
        }

        public virtual bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model,
                                       DependencyModel dependency)
        {
            if (dependency.TargetItemType == null)
            {
                return false;
            }
            Type itemType = GetItemType(dependency.TargetItemType);
            if (itemType != null && !HasParameter(dependency))
            {
                return CanSatisfy(itemType);
            }
            return false;
        }

        public virtual object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model,
                                      DependencyModel dependency)
        {
            Array result = kernel.ResolveAll(GetItemType(dependency.TargetItemType), null);
            if (_orderFunc != null)
            {
                Array.Sort(result, new GenericComparer(_orderFunc));
            }
            return result;
        }

        private class GenericComparer : IComparer
        {
            private readonly Func<object, object, int> _comparer;

            public GenericComparer(Func<object, object, int> comparer)
            {
                _comparer = comparer;
            }

            public int Compare(object x, object y)
            {
                return _comparer(x, y);
            }
        }

        public virtual bool CanSatisfy(Type itemType)
        {
            return allowEmptyCollections || kernel.HasComponent(itemType);
        }

        private Type GetItemType(Type targetItemType)
        {
            return targetItemType.IsArray ? targetItemType.GetElementType() : null;
        }

        private static bool HasParameter(DependencyModel dependency)
        {
            return dependency.Parameter != null;
        }
    }
}