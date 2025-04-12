﻿using Microsoft.AspNetCore.Mvc;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;

namespace TranVanToan_2122110162.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_context.Users.ToList());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound("User not found");

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Password = dto.Password;
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null) return NotFound("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok(new { message = "User deleted." });
        }
    }
}
