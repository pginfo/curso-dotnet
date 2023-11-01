﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoRazorPages.Data;
using DemoRazorPages.Models;

namespace DemoRazorPages.Pages.Alunos
{
    public class DetailsModel : PageModel
    {
        private readonly DemoRazorPages.Data.ApplicationDbContext _context;

        public DetailsModel(DemoRazorPages.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Aluno Aluno { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aluno = await _context.Aluno.FirstOrDefaultAsync(m => m.Id == id);

            if (Aluno == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}