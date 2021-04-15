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

        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<Users> GetByName(string name)
        {
            return _context.Users.Where(u => u.Name == name).ToList();
        }

        public IEnumerable<Users> GetBetweenDates(DateTime startDate, DateTime endDate)
        {
            return _context.Users
                   .Where(u => u.BirthDate >= startDate && u.BirthDate <= endDate).ToList();
        }

        public Users GetByCPF(string cpf)
        {
            cpf = cpf.Replace(@"/\D/g", "");
            
            if(cpf.Length == 11)
                return _context.Users.Where(u => u.CPF == cpf).FirstOrDefault();

            return null;
        }
    }
}
