﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2001___RESTful_API.Models;
using COMP2001___RESTful_API.Attributes;

namespace COMP2001___RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class userController : ControllerBase
    {
        private readonly DataAccess database;

        public userController(DataAccess context)
        {
            database = context;
        }

        //Validate method to check if user exists in database
        [HttpGet]
        [HttpHead("Content-Type: application/json, application/xml")]
        [HttpHead("Accept: application/json, application/xml")]
        [Produces("application/xml", "application/json")]
        public IActionResult loginUser(User user)
        {
            if (getValidation(user) == true)
            {
                return Ok(new { verified = "true" });
            }

            return Ok(new { verified = "false" });
        }

        //gets validation from dataaccess class
        private bool getValidation(User user)
        {
            if (database.Validate(user) == true)
            {
                return true;
            }

            return false;
        }

        //Register method to add new user to database
        [HttpPost]
        [HttpHead("Content-Type: application/json, application/xml")]
        [HttpHead("Accept: application/json, application/xml")]
        [Consumes("application/json", "application/xml")]
        [Produces("application/xml", "application/json")]
        public IActionResult RegisterUser(User user)
        {
            string responseMessage = "";

            register(user, out responseMessage);

            if (user == null)
            {
                return StatusCode(404);
            }
            return Ok(new { UserID = responseMessage });
        }

        private void register(User usersRegistered, out string responseMessage)
        {
            database.Register(usersRegistered, out responseMessage);
        }

        // Update method to update users in database
        [HttpPut("{id}")]
        [HttpHead("Content-Type: application/json, application/xml")]
        [HttpHead("Accept: application/json, application/xml")]
        [Consumes("application/json", "application/xml")]
        [Produces("application/xml", "application/json")]
        public IActionResult updateUser(int id, User user)
        {

            database.Update(id, user);

            return StatusCode(204);
        }

        // Delete method to Delete users in database
        [HttpDelete("{id}")]
        [HttpHead("Content-Type: application/json, application/xml")]
        [HttpHead("Accept: application/json, application/xml")]
        [Produces("application/xml", "application/json")]
        public IActionResult deleteUser(int id)
        {
            database.Delete(id);

            return StatusCode(204);
        }
    }
}
