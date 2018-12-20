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

      if (_context.Tareas.Count() == 0)
      {
        // Create a new Tarea if collection is empty,
        // which means you can't delete all Tareas.
        _context.Tareas.Add(new Tarea { Nombre = "tarea1" });
        _context.SaveChanges();
      }
    }

    [HttpGet]
    //La diferencia entre IEnumerable y List es que 1ro es de solo lectura
    public async Task<ActionResult<IEnumerable<Tarea>>> GetAll()
    {
      // MVC automaticamente serializa los objetos a json. 
      return await _context.Tareas.ToListAsync();
    }

    /* Name = "GetTodo" creates a named route. Named routes:
       Enable the app to create an HTTP link using the route name.*/
    [HttpGet("{id}", Name = "GetTodo")]
    public async Task<ActionResult<Tarea>> GetById(long id)
    {
      var item = await _context.Tareas.FindAsync(id);
      if (item == null)
      {
        //retorna HTTP 404
        return NotFound();
      }
        return item;
    }

    [HttpGet("user/{idUsuario:long}")]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetByUser(long idUsuario)
    {
      if(!await _context.Usuarios.AnyAsync( u => u.Id == idUsuario)) return NotFound();

      /* var todos = await _context.Tareas
      .Where(t => t.UserId == idUsuario)
      .Include(t => t.User)
      .ToListAsync(); */
      
      var tareas = await _context.Tareas
      .Where(t => t.IdUsuario == idUsuario)
      .Include(t => t.Usuario)
      .Select(t => new
      {
        t.Id,
        t.Nombre,
        t.Completado,
        Usuario = t.Usuario.Username
      }).ToListAsync();

      /*
      Para un getById:
      .SingleOrDefaultAsync(b => b.Id == id);
      obtenido de: https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
      */ 

      return Ok(tareas);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tarea item)
		{
			_context.Tareas.Add(item);
			await _context.SaveChangesAsync();

      /*  CreatedAtRoute(): Retorna HTTP 201 Created
          Agrega la ubicacion del elemento creado en la cabecera del response */
      return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Tarea item)
    {
      var tarea = await _context.Tareas.FindAsync(id);
      if (tarea == null)
      {
        return NotFound();
      }

      tarea.Completado = item.Completado;
      tarea.Nombre = item.Nombre;
      tarea.IdUsuario = item.IdUsuario;

      _context.Tareas.Update(tarea);
      await _context.SaveChangesAsync();
      return NoContent();
    }

  	[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      var tarea = await _context.Tareas.FindAsync(id);
      if (tarea == null)
      {
        return NotFound();
      }
      _context.Tareas.Remove(tarea);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}