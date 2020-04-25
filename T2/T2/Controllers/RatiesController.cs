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
using T2.Models;

namespace T2.Controllers
{
    public class RatiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        // GET: api/Raties/5
        [ResponseType(typeof(RatiesModel))]
        public IHttpActionResult GetRatiesModel(long id)
        {
            RatiesModel ratiesModel = db.Raties.Find(id);
            if (ratiesModel == null)
            {
                return NotFound();
            }

            return Ok(ratiesModel);
        }

        // PUT: api/Raties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRatiesModel(long id, RatiesModel ratiesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ratiesModel.Id)
            {
                return BadRequest();
            }

            db.Entry(ratiesModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatiesModelExists(id))
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

        // POST: api/Raties
        [ResponseType(typeof(RatiesModel))]
        public IHttpActionResult PostRatiesModel(RatiesModel ratiesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Raties.Add(ratiesModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ratiesModel.Id }, ratiesModel);
        }

        // DELETE: api/Raties/5
        [ResponseType(typeof(RatiesModel))]
        public IHttpActionResult DeleteRatiesModel(long id)
        {
            RatiesModel ratiesModel = db.Raties.Find(id);
            if (ratiesModel == null)
            {
                return NotFound();
            }

            db.Raties.Remove(ratiesModel);
            db.SaveChanges();

            return Ok(ratiesModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatiesModelExists(long id)
        {
            return db.Raties.Count(e => e.Id == id) > 0;
        }
    }
}