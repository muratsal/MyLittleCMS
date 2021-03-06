﻿using MyLittleCMS.Core.Repository;
using MyLittleCMS.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyLittleCMS.Data.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> _table;
        protected EFContext _context;

        public EFRepository(IEFContext dbContext)
        {
            _context = (EFContext)dbContext;
            _table = _context.Set<T>();
        }

        public T Get(int id)
        {
            return _table.Find(id);
        }

        public IQueryable<T> Get()
        {
            return _table.AsQueryable();
        }


        public T Add(T entity)
        {
            _table.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public T Update(T entity)
        {
            var entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            return entity;
        }

        public T First(Expression<Func<T, bool>> where)
        {
            return _table.FirstOrDefault(where);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return _table.Where<T>(where);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        public void Delete(IQueryable<T> entities)
        {
            foreach (T entity in entities)
            {
                _table.Remove(entity);
            }
        }
        public  IQueryable<T> Table()
        {
            return _table;
        }
    }

    public static class ObjectQueryExtensions
    {
        public static ObjectQuery<T> Include<T>(this ObjectQuery<T> query, Expression<Func<T, object>> selector)
        {
            string path = new PropertyPathVisitor().GetPropertyPath(selector);
            return query.Include(path);
        }

        class PropertyPathVisitor : ExpressionVisitor
        {
            private Stack<string> _stack;

            public string GetPropertyPath(Expression expression)
            {
                _stack = new Stack<string>();
                Visit(expression);
                return _stack
                    .Aggregate(
                        new StringBuilder(),
                        (sb, name) =>
                            (sb.Length > 0 ? sb.Append(".") : sb).Append(name))
                    .ToString();
            }

            protected override Expression VisitMember(MemberExpression expression)
            {
                if (_stack != null)
                    _stack.Push(expression.Member.Name);
                return base.VisitMember(expression);
            }

            protected override Expression VisitMethodCall(MethodCallExpression expression)
            {
                if (IsLinqOperator(expression.Method))
                {
                    for (int i = 1; i < expression.Arguments.Count; i++)
                    {
                        Visit(expression.Arguments[i]);
                    }
                    Visit(expression.Arguments[0]);
                    return expression;
                }
                return base.VisitMethodCall(expression);
            }

            private static bool IsLinqOperator(MethodInfo method)
            {
                if (method.DeclaringType != typeof(Queryable) && method.DeclaringType != typeof(Enumerable))
                    return false;
                return Attribute.GetCustomAttribute(method, typeof(ExtensionAttribute)) != null;
            }
        }
    }

}
