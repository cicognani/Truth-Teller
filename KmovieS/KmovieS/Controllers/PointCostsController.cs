using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KmovieS.Models;

namespace KmovieS.Controllers
{
    public class PointCostsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       
        
        // GET: api/PointCosts
        public IQueryable<PointCost> GetpointCost()
        {
            return db.pointCost;
        }

        // GET: api/PointCosts/5
        [Authorize(Roles = "User")]
        [ResponseType(typeof(PointCost))]
        public IHttpActionResult GetPointCost(string id)
        {
            PointCost pointCost = db.pointCost.Find(id);
            if (pointCost == null)
            {
                return NotFound();
            }

            return Ok(pointCost);
        }

        // PUT: api/PointCosts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPointCost(string id, PointCost pointCost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pointCost.APIFullname)
            {
                return BadRequest();
            }

            db.Entry(pointCost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointCostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PointCosts
        [ResponseType(typeof(PointCost))]
        public IHttpActionResult PostPointCost(PointCost pointCost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pointCost.Add(pointCost);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PointCostExists(pointCost.APIFullname))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pointCost.APIFullname }, pointCost);
        }

        // DELETE: api/PointCosts/5
        [ResponseType(typeof(PointCost))]
        public IHttpActionResult DeletePointCost(string id)
        {
            PointCost pointCost = db.pointCost.Find(id);
            if (pointCost == null)
            {
                return NotFound();
            }

            db.pointCost.Remove(pointCost);
            db.SaveChanges();

            return Ok(pointCost);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PointCostExists(string id)
        {
            return db.pointCost.Count(e => e.APIFullname == id) > 0;
        }
    }
}