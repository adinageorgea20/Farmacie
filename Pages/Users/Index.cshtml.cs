﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Farmacie.Data;
using Farmacie.Models;

namespace Farmacie.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Farmacie.Data.FarmacieContext _context;

        public IndexModel(Farmacie.Data.FarmacieContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
