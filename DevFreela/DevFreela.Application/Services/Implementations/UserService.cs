using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFreela.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;
        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(NewUserInputModel inputModel)
        {
            var user = new User(
                inputModel.FullName,
                inputModel.Email,
                inputModel.BirthDate,
                inputModel.Password,
                inputModel.Role
                );

            _dbContext.Users.Add(user);

            _dbContext.SaveChanges();

            return user.Id;
        }

        public List<UserViewModel> GetAll(string query)
        {
            var users = _dbContext.Users;

            var usersViewModel = users.Select(p => new UserViewModel(p.Id, p.FullName, p.Email)).ToList();

            return usersViewModel;
        }

        public UserViewModel GetUser(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null) { return null; }

           return new UserViewModel(user.Id, user.FullName, user.Email);
        }

        public void Update(UpdateUserInputModel inputModel)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == inputModel.Id);

            user.Update(inputModel.FullName, inputModel.Email, inputModel.BirthDate, inputModel.Active);

            _dbContext.SaveChanges();
        }
    }
}

