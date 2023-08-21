using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Net;
using System.Net.Http;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RMSModel.Models;
using System.Text.Json;
using RMSModel.Tools;
using System.Linq.Dynamic.Core;
using RMSServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RMSServices.Controllers
{
    public abstract class CrudController<TEntity>
              : Controller where TEntity : class
    {
        private readonly RMSContext _db = new RMSContext();
        private readonly DbSet<TEntity> _dbSet;

        protected CrudController(Func<RMSContext, DbSet<TEntity>> dbSet)
        {
            _db = new RMSContext();
            _dbSet = dbSet(_db);
        }

        [HttpGet]
        [Authorize]
        //[Route("/{controller}/Get")]
        public ActionResult Get()
        {
            return Json(_dbSet.ToList(), new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpGet]
        [Authorize]
        [Route("/[controller]/GetById/{id?}")]

        public ActionResult GetById(long? id)
        {
            
            return Json(_dbSet.FirstOrDefault(String.Format("ID={0}", id)), new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        /// <response code="400">If the request is null</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpPost]
        [Authorize]
        //[Route("/GetByFilter")]
        public ActionResult GetByFilter([FromBody] RequestWrapper request)
        {
            if (request != null)
            {
                try
                {
                    
                    return Json(
                        _dbSet.Where(request.FilterString)
                            .Skip(request.Limit.Skip)
                            .Take(request.Limit.Take)
                            .OrderBy(request.Sort.Property + " "+request.Sort.Direction).ToList(),
                        new JsonSerializerOptions { PropertyNamingPolicy = null }
                    );
                    

                }
                catch (Exception ex)
                {

                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                

            }
            else
            {
                return BadRequest();
            }
            

        }

        /// <response code="400">If the data update is conflicting</response>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        [HttpPost]
        [Authorize]
        //[Route("/Post")]

        public ActionResult Post(TEntity entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _db.Entry(entity).State = EntityState.Added;

                _db.SaveChanges();

                return Created(this.ControllerContext.HttpContext.Request.Path, Json(entity, new JsonSerializerOptions { PropertyNamingPolicy = null }));
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        //[Route("/Put")]

        public ActionResult Put(TEntity entity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                _db.Entry(entity).State = EntityState.Modified;

                _db.SaveChanges();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        [Authorize]
        //[Route("/Delete")]

        public ActionResult Delete(TEntity entity)
        {
            try
            {
                _db.Entry(entity).State = EntityState.Deleted;

                _db.SaveChanges();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
