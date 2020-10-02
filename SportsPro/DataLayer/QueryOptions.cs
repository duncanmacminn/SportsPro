using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsPro.DataLayer
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public Expression<Func<T, bool>> Where
        {
            set
            {
                if (WhereClauses == null)
                {
                    WhereClauses = new WhereClauses<T>();
                }
                WhereClauses.Add(value);
            }
        }
        //public Expression<Func<T, bool>> Where { get; set; }

        private string[] includes;

        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        public string[] GetIncludes() => includes ?? new string[0];

        public bool HasWhere => WhereClauses != null;
        public bool HasOrderBy => OrderBy != null;

        public WhereClauses<T> WhereClauses { get; set; }

        
    
    }
    public class WhereClauses<T> : List<Expression<Func<T, bool>>> { }

}


