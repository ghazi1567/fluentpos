// --------------------------------------------------------------------------------------------------
// <copyright file="QueryableExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Filters;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : class
        {
            if (source == null)
            {
                throw new CustomException("Empty");
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }


        public static IQueryable<TEntity> AdvanceSearch<TEntity>(this IQueryable<TEntity> queryable, List<FilterModel> filterList, string advancedSearchType, bool disableTracking = true)
           where TEntity : class
        {
            if (disableTracking) queryable = queryable.AsNoTracking();
            if (filterList.Count > 0)
            {
                var filter = filterList.GenerateFilter<TEntity>(advancedSearchType);
                if (filter is null) throw new ArgumentNullException($"Filter criteria not found.");
                if (filter != null) queryable = queryable.Where(filter);
            }

            return queryable;
        }

        private static Expression<Func<TEntity, bool>> GenerateFilter<TEntity>(this List<FilterModel> filters, string advancedSearchType)
        {
            var type = typeof(TEntity);
            var parameter = Expression.Parameter(type, "p");

            List<Expression> expressions = new List<Expression>();

            Expression expression = default(Expression);
            foreach (var item in filters)
            {
                Expression exp = GetExpression<TEntity>(parameter, item);

                if (expression == null)
                {
                    expression = exp;
                }
                else
                {
                    if (advancedSearchType == "and")
                    {
                        expression = Expression.AndAlso(expression, exp);
                    }
                    else
                    {
                        expression = Expression.Or(expression, exp);
                    }
                }
            }

            // expressions.Add(expression);
            // Expression finalExpression = default(Expression);
            // foreach (var item in expressions)
            // {
            //     if (finalExpression == null)
            //         finalExpression = item;
            //     else
            //         finalExpression = Expression.Or(finalExpression, item);
            // }
            // if (finalExpression is null) throw new ArgumentNullException($"Unable to  generate dynamic predicate for model {type.FullName} ");

            return Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
        }

        private static Expression GetExpression<TEntity>(ParameterExpression parameter, FilterModel item)
        {
            var type = typeof(TEntity);
            Expression exp = default(Expression);
            PropertyInfo propertyInfo = type.GetProperty(item.FieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo is null) throw new ArgumentNullException($"Unble to find a field : '{item.FieldName}' from model {type.FullName} ");


            switch (item.Action)
            {
                case "=":
                    exp = Expression.Equal(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
                case "!=":
                    exp = Expression.NotEqual(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
                case ">":
                    exp = Expression.GreaterThan(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
                case ">=":
                    exp = Expression.GreaterThanOrEqual(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
                case "<":
                    exp = Expression.LessThan(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
                case "<=":
                    exp = Expression.LessThanOrEqual(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
                case "like":

                    exp = Expression.Equal(
                        Expression.Call(
                            typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) }),
                            Expression.Constant(EF.Functions),
                            Expression.Property(parameter, propertyInfo.Name),
                            propertyInfo.GetValueExpression(item.SearchTerm, "like")), Expression.Constant(true));
                    break;

                // case "IN":
                //    var listType = GetLisType(propertyInfo.PropertyType);
                //    var methodInfo = listType.GetMethod("Contains", new Type[] { propertyInfo.PropertyType }); // Contains Method
                //    var member = Expression.Property(parameter, propertyInfo.Name);
                //    var constant = ConvertList(propertyInfo.PropertyType, item.ValueList);
                //    Expression body = Expression.Call(constant, methodInfo, member);
                //    exp = body;
                //    //exp = Expression.Equal(body, Expression.Constant(true));
                //    break;

                default:
                    exp = Expression.Equal(Expression.PropertyOrField(parameter, propertyInfo.Name), propertyInfo.GetValueExpression(item.SearchTerm));
                    break;
            }

            return exp;
        }

        public static Expression GetValueExpression(this PropertyInfo property, string value, string Opr = "")
        {

            // Get the type code so we can switch
            TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
            TypeConverter typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
            object propValue = typeConverter.ConvertFromString(value);

            if (typeCode == TypeCode.Object)
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    var date = DateTime.Parse(value);
                    return Expression.Constant(date.ToString("MM-dd-yyyy"), typeof(DateTime));
                }
                else if (property.PropertyType == typeof(DateTime?))
                {

                    var date = DateTime.Parse(value);
                    return Expression.Constant(date.Date, typeof(DateTime?));

                }
                if (property.PropertyType == typeof(int))
                {
                    return Expression.Constant(propValue, typeof(int));
                }
                else if (property.PropertyType == typeof(int?))
                {
                    return Expression.Constant(propValue, typeof(int?));
                }
                if (property.PropertyType == typeof(Int16))
                {
                    return Expression.Constant(propValue, typeof(Int16));
                }
                else if (property.PropertyType == typeof(Int16?))
                {
                    return Expression.Constant(propValue, typeof(Int16?));
                }
                if (property.PropertyType == typeof(Int32))
                {
                    return Expression.Constant(propValue, typeof(Int32));
                }
                else if (property.PropertyType == typeof(Int32?))
                {
                    return Expression.Constant(propValue, typeof(Int32?));
                }
                if (property.PropertyType == typeof(Int64))
                {
                    return Expression.Constant(propValue, typeof(Int64));
                }
                else if (property.PropertyType == typeof(Int64?))
                {
                    return Expression.Constant(propValue, typeof(Int64?));
                }
            }

            if (typeCode == TypeCode.String)
            {
                if (Opr == "like")
                {
                    return Expression.Constant($"%{propValue}%", typeof(string));
                }

                return Expression.Constant($"{propValue}", typeof(string));
            }

            return Expression.Constant(propValue);
        }

    }
}