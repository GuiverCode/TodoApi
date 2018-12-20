using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoController : ControllerBase
  {
    private readonly TodoContext _context;
		
		//El constructor utiliza la inyección de dependencia para inyectar el contexto de la base de datos (TodoContext) al controlador
    public TodoController(TodoContext context)
    {
      _context = context;

      if (_context.TodoItems.Count() == 0)
      {
        // Create a new TodoItem if collection is empty,
        // which means you can't delete all TodoItems.
        _context.TodoItems.Add(new TodoItem { Name = "Item1" });
        _context.SaveChanges();
      }
    }

    [HttpGet]
    //La diferencia entre IEnumerable y List es que 1ro es de solo lectura
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
      // MVC automaticamente serializa los objetos a json. 
      return await _context.TodoItems.ToListAsync();
    }

    /* Name = "GetTodo" creates a named route. Named routes:
       Enable the app to create an HTTP link using the route name.*/
    [HttpGet("{id}", Name = "GetTodo")]
    public async Task<ActionResult<TodoItem>> GetById(long id)
    {
      var item = await _context.TodoItems.FindAsync(id);
      if (item == null)
      {
        //retorna HTTP 404
        return NotFound();
      }
        return item;
    }

    [HttpGet("user/{idUser:long}")]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetByUser(long idUser)
    {
      if(!await _context.Users.AnyAsync( u => u.Id == idUser)) return NotFound();

      /* var todos = await _context.TodoItems
      .Where(t => t.UserId == idUser)
      .Include(t => t.User)
      .ToListAsync(); */
      
      var todos = await _context.TodoItems
      .Where(t => t.UserId == idUser)
      .Include(t => t.User)
      .Select(t => new
      {
        t.Id,
        t.Name,
        t.IsComplete,
        User = t.User.Username
      }).ToListAsync();

      /*
      Para un getById:
      .SingleOrDefaultAsync(b => b.Id == id);
      obtenido de: https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
      */ 

      return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoItem item)
		{
			_context.TodoItems.Add(item);
			await _context.SaveChangesAsync();

      /*  CreatedAtRoute(): Retorna HTTP 201 Created
          Agrega la ubicacion del elemento creado en la cabecera del response */
      return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, TodoItem item)
    {
      var todo = await _context.TodoItems.FindAsync(id);
      if (todo == null)
      {
        return NotFound();
      }

      todo.IsComplete = item.IsComplete;
      todo.Name = item.Name;
      todo.UserId = item.UserId;

      _context.TodoItems.Update(todo);
      await _context.SaveChangesAsync();
      return NoContent();
    }

  	[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      var todo = await _context.TodoItems.FindAsync(id);
      if (todo == null)
      {
        return NotFound();
      }
      _context.TodoItems.Remove(todo);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}