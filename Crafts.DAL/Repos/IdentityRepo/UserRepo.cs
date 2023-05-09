﻿using Crafts.DAL.Context;
using Crafts.DAL.Models;
using Crafts.DAL.Repos.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crafts.DAL.Repos.IdentityRepo;

public class UserRepo: GenericRepo<User>, IUserRepo
{
    private readonly CraftsContext _context;

    public UserRepo(CraftsContext context) : base(context)
    {
        _context = context;
    }

    public User GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }
}
