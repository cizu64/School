using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.DTO;
using School.API.Helpers;
using School.API.Service;
using School.API.ViewModel;
using School.Domain.Entities;
using School.Domain.Interfaces;
using System.Net;

namespace School.API.Controllers
{
    
    [Route("api/v1/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoRepository todoRepository;
        private readonly ITodoService todoService;
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly User user;

        public TodoController(ILogger<TodoController> logger, ITodoRepository todoRepository, ITodoService todoService, IRestClient client, IConfiguration configuration)
        {
            _logger = logger;
            this.todoRepository = todoRepository;
            this.todoService = todoService;
            this.client = client;
            this.configuration = configuration;
            user = new User(this.client, this.configuration);
        }

        [HttpGet, Route("[action]/{pageSize:int}/{pageIndex:int}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Todo>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Get(int pageSize=10, int pageIndex=0)
        {
            try
            {
                var studentId = await user.GetUserIdFromToken(Request.Headers["Authorization"]);
                if (studentId == null)
                {
                    return BadRequest(new ApiResult
                    {
                        Message = "User not found",
                        Succeeded = false
                    });
                }
                var todos = await todoRepository.GetTodos(studentId.Value);
                var paged = todos.Skip(pageSize * pageIndex).Take(pageSize);
                int totalItem = todos.Count;
                var model = new PaginatedItemsViewModel<Todo>(pageIndex, pageSize, totalItem, paged);
                return Ok(new ApiResult
                {
                    Message = "Retrieved successfully",
                    Result = model
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiResult
                {
                    Message = "An error occured",
                    Succeeded = false
                });
            }
        }

        [HttpPost, ValidateModel, Route("[action]")]
        public async Task<IActionResult> Post([FromBody] TodoDTO todo)
        {
            try
            {
                var studentId = await user.GetUserIdFromToken(Request.Headers["Authorization"]);
                if (studentId == null)
                {
                    return BadRequest(new ApiResult
                    {
                        Message = "User not found",
                        Succeeded = false
                    });
                }
                await todoRepository.AddTodo(studentId.Value, todo.Name, todo.Description);
                return Ok(new ApiResult
                {
                    Message = "Added successfully"
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiResult
                {
                    Message = "An error occured",
                    Succeeded = false
                });
            }
        }

        [HttpPut, Route("[action]/{todoId:int}"), ValidateModel]
        public async Task<IActionResult> CompleteTodo(int todoId,CompleteTodoDTO todo)
        {
            try
            {
                var studentId = await user.GetUserIdFromToken(Request.Headers["Authorization"]);
                if (studentId == null)
                {
                    return BadRequest(new ApiResult
                    {
                        Message = "User not found",
                        Succeeded = false
                    });
                }
                todo.StudentId = studentId.Value;
                await todoService.CompleteTodo(todoId, todo.StudentId, todo.DateCompleted);
                return Ok(new ApiResult
                {
                    Message = "todo completed successfully",
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new ApiResult
                {
                    Message = "An error occured",
                    Succeeded = false
                });
            }
        }
    }
}