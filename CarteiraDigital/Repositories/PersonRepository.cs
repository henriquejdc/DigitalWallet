﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarteiraDigital.Models;
using NHibernate;

namespace CarteiraDigital.Repositories
{
    public class PersonRepository : IRepository<Person>
    {
        private ISession _session;
        public PersonRepository(ISession session) => _session = session;
        public async Task Add(Person item)
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

        public IEnumerable<Person> FindAll() => _session.Query<Person>().ToList();

        public string CountPeople()
        {
            var count = _session.Query<Person>().Count();
            return count.ToString();
        }

        public List<Person> FindAllById(long id) => _session.Query<Person>().Where(
            p => p.Id == id
        ).ToList();

        public List<Person> FindByName(string name)
        {
            var result = _session.Query<Person>().Where(p => p.Name.Contains(name));
            return result.ToList();
        }

        public Person FindByUsername(string username)
        {
            Person user = _session.Query<Person>().Where(
                u => u.Username.Contains(username)
            ).FirstOrDefault();
            return user;
        }

        public async Task<Person> FindByID(long id) => await _session.GetAsync<Person>(id);

        public async Task Remove(long id)
        {
            ITransaction transaction = null;
            try
            {
                transaction = _session.BeginTransaction();
                var item = await _session.GetAsync<Person>(id);
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

        public async Task Update(Person item)
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