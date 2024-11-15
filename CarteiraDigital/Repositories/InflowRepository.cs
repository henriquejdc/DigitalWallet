﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarteiraDigital.Models;
using NHibernate;

namespace CarteiraDigital.Repositories
{
    public class InflowRepository
    {
        private ISession _session;
        public InflowRepository(ISession session) => _session = session;

        public async Task Add(Inflow item)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.SaveAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public List<Inflow> FindAll() => _session.Query<Inflow>().ToList();

        public string CountAllInflows()
        {
            var count = _session.Query<Inflow>().Count();
            return count.ToString();
        }

        public string CountUserInflows(long id)
        {
            var count = _session.Query<Inflow>().Where(p => p.Person.Id == id).Count();
            return count.ToString();
        }

        public List<Inflow> SearchFilter(Filter filter)
        {
            var result = _session.Query<Inflow>();

            result = _session.Query<Inflow>().Where(p =>
            p.Person.Name.Contains(filter.Name != null ? filter.Name : "[a-zA-Z]") &&
            p.InflowDate >= (filter.Periodo > 0 ?
                filter.Periodo == 1 ? DateTime.Now.AddDays(-7) :
                filter.Periodo == 2 ? DateTime.Now.AddDays(-15) :
                filter.Periodo == 3 ? DateTime.Now.AddDays(-30) :
                filter.MinDate : filter.MinDate) &&
            p.InflowDate <= (filter.Periodo > 0 ? DateTime.Now : filter.MaxDate != DateTime.MinValue ? filter.MaxDate : DateTime.MaxValue)
            );

            return result.ToList();
        }

       public double SumAmount()
        {
            return _session.Query<Inflow>().Sum(i => i.InflowAmount);
        }

        public async Task<Inflow> FindByID(long id) => await _session.GetAsync<Inflow>(id);

        public List<Inflow> FindAllById(long id) => _session.Query<Inflow>().Where(p => p.Person.Id == id).ToList();

        public async Task Remove(long id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Inflow>(id);
                await _session.DeleteAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task Update(Inflow item)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                await _session.UpdateAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await transaction?.RollbackAsync();
            }
            finally
            {
                transaction?.Dispose();
            }
        }
    }
}