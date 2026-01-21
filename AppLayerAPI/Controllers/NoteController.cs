using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        NoteService noteserv;

        public NoteController(NoteService noteserv)
        {
            this.noteserv = noteserv;
        }
        [HttpPost("{userId}/create")]
        public IActionResult Create(int userId, NoteCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, error) = noteserv.Create(userId, dto);
            if (!success)
            {
                return BadRequest(new { Msg = error });
            }
               
            return StatusCode(201, new { Msg = "Note created successfully" });
        }

        [HttpGet("{userId}")]
        public IActionResult GetByUser(int userId)
        {
            var notes = noteserv.GetByUser(userId);
            return Ok(notes);
        }

        [HttpGet("{userId}/{noteId}")]
        public IActionResult Get(int userId, int noteId)
        {
            var note = noteserv.Get(noteId);
            if (note == null || note.UserId != userId)
            {
                return NotFound(new { Msg = "Note not found" });
            }
            
            return Ok(note);
        }

        [HttpPut("{userId}/{noteId}")]
        public IActionResult Update(int userId, int noteId, NoteCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var (success, error) = noteserv.Update(noteId, userId, dto);
            if (!success)
            {
                return BadRequest(new { Msg = error });
            }

            return Ok(new { Msg = "Note updated successfully" });
        }

        [HttpDelete("{userId}/{noteId}")]
        public IActionResult Delete(int userId, int noteId)
        {
            var ok = noteserv.Delete(noteId, userId);
            if (!ok)
            {
                return BadRequest(new { Msg = "Delete failed or not authorized" });
            }   

            return Ok(new { Msg = "Note deleted successfully" });
        }
        [HttpPost("{userId}/{noteId}/review")]
        public IActionResult AddReview(int userId, int noteId, ReviewCreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var (success, error) = noteserv.AddReview(userId, noteId, dto);
            if (!success)
            {
                return BadRequest(new { Msg = error });
            }
               
            return Ok(new { Msg = "Review added" });
        }

        [HttpPost("{userId}/{noteId}/buy")]
        public IActionResult Buy(int userId, int noteId)
        {
            var (success, error) = noteserv.BuyNote(userId, noteId);
            if (!success)
            {
                return BadRequest(new { Msg = error });
            }
                
            return Ok(new { Msg = "Purchase Successful" });
        }
        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] NoteFilterDTO filter)
        {
            var results = noteserv.GetFilteredNotes(filter);
            return Ok(results);
        }

    }
}
