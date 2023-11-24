using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Paginacao.Data;
using Paginacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Paginacao.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : ControllerBase
    {
        [HttpGet("load")]
        public async Task<IActionResult> LoadAsync([FromServices]AppDbContext context)
        {
            for (var i = 0; i < 1348; i++)
            {
                var todo = new Todo()
                {
                    Id = i + 1,
                    Done = false,
                    CreatedAt = DateTime.Now,
                    Title = $"Tarefa {i}"
                };

                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
            }


            return Ok();
        }

        [HttpGet("page/{page:int}/take/{take:int}")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, int page = 1, int take = 5)
        {
            var total = await context.Todos.CountAsync();  
            var pages = Math.Ceiling(total/(decimal)take);

            if (take > 100) return BadRequest();

            List<Todo> todos = await context
                .Todos
                .AsNoTracking()
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            return Ok(new
            { 
                total,
                page,
                pages,
                data = todos
            });
        }
    }
}
