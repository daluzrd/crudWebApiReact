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

        public void Post(Users user)
        {
            ValidateVariables(user.Name, nameof(user.Name), false ,2, 50);

            if(user.BirthDate > DateTime.Now)
                throw new Exception("Birthdate invalid. Please, retry with a valid birthdate");

            Regex regex = new Regex(@"[^\d]");
            user.CPF = regex.Replace(user.CPF, string.Empty);
            ValidateVariables(user.CPF, nameof(user.CPF), false, 11, 11);

            ValidateVariables(user.User, nameof(user.User), true, 1, 10);

            ValidateVariables(user.Pwd, nameof(user.Pwd), true, 8, 32);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private void ValidateVariables(string model, string variableName, bool isNullable = false, int minLength = int.MinValue, int maxLength = int.MaxValue)
        {
            if(!isNullable && model == null)
                throw new Exception("The " + variableName + " cannot be null");
            if(model==string.Empty)
                throw new Exception("The " + variableName + " cannot be empty");
            if(model != null && model.Length < minLength)
                throw new Exception("The " + variableName + " length cannot be smaller then " + minLength + " characters");
            if(model != null && model.Length > maxLength)
                throw new Exception("The " + variableName + " length cannot be bigger then " + maxLength + " characters");
        }
    }
}
