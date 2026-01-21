using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserService userserv;
        public UserController(UserService userserv)
        {
            this.userserv = userserv;
        }
        [HttpGet("all")]
        public IActionResult All()
        {
            var data = userserv.Get();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = userserv.Get(id);
            return Ok(data);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ok = userserv.Delete(id);
            if (!ok)
            {
                return NotFound(new { Msg = "User not found or delete failed" });
            }
            return Ok(new { Msg = "User deleted" });
        }
        [HttpPost("{id}/recharge")]
        public IActionResult Recharge(int id, RechargeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                
            var (success, error) = userserv.Recharge(id, dto);
            if (!success)
            {
                return BadRequest(new { Msg = error });
            }
                
            return Ok(new { Msg = "Recharge Successful" });
        }
        [HttpGet("{id}/balance")]
        public IActionResult Balance(int id)
        {
            var bal = userserv.GetBalance(id);
            if (bal == null)
            {
                return NotFound(new { Msg = "User not found" });
            }
            return Ok(new { Balance = bal.Value });
        }
    }
}
