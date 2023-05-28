using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.DTO;
using School.API.Helpers;
using School.API.Service;
using School.API.ViewModel;
using School.Domain.Aggregates;
using School.Domain.SeedWork;
using System.Net;

namespace School.API.Controllers
{

    [Route("api/v1/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IGenericRepository<Todo> _todoRepository;
        private readonly IRestClient client;
        private readonly IConfiguration configuration;
        private readonly User user;

        public TodoController(ILogger<TodoController> logger, IGenericRepository<Todo> todoRepository, IRestClient client, IConfiguration configuration)
        {
            _logger = logger;
            _todoRepository = todoRepository;
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
                var todos = await _todoRepository.GetAll(t=>t.StudentId==studentId.Value);
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
                await _todoRepository.AddAsync(new Todo(studentId.Value, todo.Name, todo.Description));
                await _todoRepository.UnitOfWork.SaveAsync();
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

                //use cqrs approach later
                var target = await _todoRepository.Get(s => s.Id == todoId && s.StudentId == studentId);
                target.CompleteTodo(todo.DateCompleted, target);
                await _todoRepository.UpdateAsync(target);
                await _todoRepository.UnitOfWork.SaveAsync();
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

        [HttpDelete, Route("[action]/{todoId:int}")]
        public async Task<IActionResult> DeleteTodo(int todoId)
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

                Todo todo = await _todoRepository.Get(t => t.Id == todoId && t.StudentId == studentId);
                await _todoRepository.DeleteAsync(todo);
                todo.DeleteTodo(studentId.Value, todo.Id); //entity behavior that adds the domain events
                await _todoRepository.UnitOfWork.SaveAsync();
                return Ok(new ApiResult
                {
                    Message = "todo deleted successfully",
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