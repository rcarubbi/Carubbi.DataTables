﻿using System.Linq;
using System.Text;
using DataTables.Mvc;

namespace Carubbi.Datatables
{
    public static class Extensions
    {
        public static string ToDynamicExpression<TViewModel>(this IOrderedEnumerable<Column> instance)
        {
            var stbSortColumnsDynamicExpression = new StringBuilder();
            var viewModelType = typeof(TViewModel);

            var expression = string.Empty;

            foreach (var column in instance)
            {
                var property = viewModelType.GetProperty(column.Data);
                var attribute =
                    (DataTablesColumnAttribute) property.GetCustomAttributes(typeof(DataTablesColumnAttribute), false)
                        .Single();
                if (!attribute.Hidden)
                    stbSortColumnsDynamicExpression.AppendFormat("{0} {1}, ",
                        attribute.SortMap ?? property.Name,
                        column.SortDirection == Column.OrderDirection.Ascendant
                            ? "ASC"
                            : "DESC");
            }

            if (stbSortColumnsDynamicExpression.Length > 0)
                stbSortColumnsDynamicExpression =
                    stbSortColumnsDynamicExpression.Remove(stbSortColumnsDynamicExpression.Length - 2, 2);

            return stbSortColumnsDynamicExpression.ToString();
        }
    }
}