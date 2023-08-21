using Microsoft.AspNetCore.Mvc;

namespace todo_api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static readonly List<ToDo> _todoItems = new();

    private readonly ILogger<TodoController> _logger;

    public TodoController(ILogger<TodoController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetMyItems")]
    public IEnumerable<ToDo> Get()
    {
        return _todoItems;
    }


    [HttpPost(Name = "CreateItem")]
    public ToDo Post([FromBody] ToDo item)
    {
        var lastItem = _todoItems.LastOrDefault();
        item.Id = lastItem == null ? 1 : lastItem.Id + 1;

        _todoItems.Add(item);

        return item;
    }

    [HttpPut("{id:double}", Name = "UpdateItem")]
    public IActionResult Put([FromRoute] double id, [FromBody] ToDo item)
    {
        var found = _todoItems.Find(item => item.Id == id);

        if (found == null)
            return NotFound();


        found.IsCompleted = item.IsCompleted;

        return Ok(found);
    }

    [HttpDelete("{id:double}", Name = "DeleteItem")]
    public IActionResult Delete([FromRoute] double id)
    {
        var found = _todoItems.Find(item => item.Id == id);

        if (found == null)
            return NotFound();


        _todoItems.Remove(found);

        return Ok(true);
    }
}

public class SetToDoStatus
{
    public bool IsCompleted { get; set; }
}

public class ToDo
{
    public long? Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
}
