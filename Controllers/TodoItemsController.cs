
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoListe.Controllers
{
    [ApiController]
    [Route("api/todoitems")]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodosService _todosService;

        public TodoItemsController(TodosService todosService) =>
            _todosService = todosService;

        // GET: api/todoitems
        [HttpGet]
        public async Task<List<TodoItem>> GetTodoItems() =>
            (await _todosService.GetAsync()).ToList();
        
        // GET: api/todoitems/642f0a27be61fb00becd02ce
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(string id)
        {
            var todoItem = await _todosService.GetAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/todoitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostTodoItem(TodoItem todoItem)
        {
            await _todosService.CreateAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/todoitems/642f0a27be61fb00becd02ce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> PutTodoItem(string id, TodoItem updatedTodoItem)
        {
            var todoItem = await _todosService.GetAsync(id);
            if (todoItem is null)
            {
                return NotFound();
            }
            updatedTodoItem.Id = todoItem.Id;
            await _todosService.UpdateAsync(id, updatedTodoItem);
            return NoContent();
        }

        // DELETE: api/todoitems/642f0a27be61fb00becd02ce
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteTodoItem(string id)
        {
            var todoItem = await _todosService.GetAsync(id);
            if (todoItem is null)
            {
                return NotFound();
            }
            await _todosService.RemoveAsync(id);
            return NoContent();
        }
    }
}
