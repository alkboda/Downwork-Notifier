using System;
using System.Collections.Generic;

namespace Downwork_Notifier.Common.Comparers
{
    public class GenericComparer<TEntity> : IEqualityComparer<TEntity> where TEntity : class
    {
        private Func<TEntity, TEntity, Boolean> _equals = null;
        private Func<TEntity, Int32> _getHashCode = null;

        public GenericComparer(Func<TEntity, TEntity, Boolean> equalsFunction = null, Func<TEntity, Int32> getHashCodeFunction = null)
        {
            _equals = equalsFunction;
            _getHashCode = getHashCodeFunction;
        }

        public bool Equals(TEntity x, TEntity y)
        {
            if (_equals == null)
            {
                throw new NotImplementedException();
            }
            return _equals(x, y);
        }

        public int GetHashCode(TEntity obj)
        {
            if (_getHashCode == null)
            {
                throw new NotImplementedException();
            }
            return _getHashCode(obj);
        }
    }
}
