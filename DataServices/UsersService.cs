using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataServices
{
    public class UsersServices
    {
        private readonly DataContext _context;

        public UsersServices(DataContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users.ToList();
        }

        public Users GetById(int Id)
        {
            return _context.Users.Find(Id);
        }

        public IEnumerable<Users> GetByName(string name)
        {
            return _context.Users.Where(u => u.Name.Contains(name)).ToList();
        }

        public IEnumerable<Users> GetBetweenBirthDates(DateTime startDate, DateTime endDate)
        {
            return _context.Users
                   .Where(u => u.BirthDate >= startDate && u.BirthDate <= endDate).ToList();
        }

        public Users GetByCPF(string cpf)
        {
            return _context.Users.Where(u => u.CPF == cpf).FirstOrDefault();
        }
    }
}
