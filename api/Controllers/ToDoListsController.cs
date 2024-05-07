using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ToDoListsController(AppDbContext context){
            _context = context;
        }
        
        // Get the entire database
        [HttpGet]

        public async Task<IEnumerable<ToDoList>> getList(){
            var toDoList = await _context.ToDoLists.AsNoTracking().ToListAsync();
            return toDoList;
        }

        //Get single task

        [HttpGet("{id:int}")]

        public async Task<ActionResult<ToDoList>> GetTask(int id){
            var toDoList = await _context.ToDoLists.FindAsync(id);

            if(toDoList == null){
                return NotFound("Task was not found (ノಠ益ಠ)ノ彡┻━┻");
            }
            return Ok(toDoList);
        }

        [HttpPost]

        public async Task<IActionResult> Create(ToDoList ToDoList){
            if(!ModelState.IsValid){
                return BadRequest("(ノಠ益ಠ)ノ彡┻━┻");
            }
            await _context.AddAsync(ToDoList);

            var result = await _context.SaveChangesAsync();
            if(result > 0){
                return Ok();
            }
            return BadRequest("(ノಠ益ಠ)ノ彡┻━┻");
        }

        [HttpPut("{id:int}")]

        public async Task<IActionResult> EditList(int id, ToDoList ToDoList){
            var taskFromDb = await _context.ToDoLists.FindAsync(id);
            if(taskFromDb == null){
                return BadRequest("Task Not Found (ノಠ益ಠ)ノ彡┻━┻");
            }

            taskFromDb.Description = ToDoList.Description;

            var result = await _context.SaveChangesAsync();
            if(result > 0){
                return Ok();
            }
            return BadRequest("(ノಠ益ಠ)ノ彡┻━┻");
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id){
            var toDoList = await _context.ToDoLists.FindAsync(id);

            if(toDoList == null){
                return NotFound("Task was not found (ノಠ益ಠ)ノ彡┻━┻");
            }

            _context.Remove(toDoList);

            var result = await _context.SaveChangesAsync();
            if(result > 0){
                return Ok();
            }
            return BadRequest("Unable to delete task (ノಠ益ಠ)ノ彡┻━┻");
        }
    }
}