using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using T2.Models;

namespace T2.Controllers
{
    public class LinksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: api/Links
        //public IQueryable<LinksModel> GetLinks()
        //{
        //    return db.Links;
        //}

        // GET: api/Links/5
        [ResponseType(typeof(LinksModel))]
        public IHttpActionResult GetLinksModel(string id)
        {         
            var httpUrl = HttpContext.Current.Request.Form["Url"];     
 
            LinksModel linksModel = db.Links.Find(httpUrl);

            // Not found
            if (linksModel == null)
            {
                return Json(new { URL = httpUrl, state = "not_found", rating = "0" });
            }
        
            // Certified FAKE
            if (linksModel.IsFalseCertified)
            {
            return Json(new { URL = httpUrl, state = "certified_fake", rating = "-100" });
            }

            // Certified TRUE
            if (linksModel.IsTrueCertified)
            {
             return Json(new { URL = httpUrl, state = "certified_ok", rating = "100" });
            }

            // Search in Raties
            var ratiesModel = db.Raties
                   .Where(b => b.Link == httpUrl)
                   .FirstOrDefault();

            if (ratiesModel==null)

            {
                return Json(new { URL = httpUrl, state = "dont_know", rating = "0" });
            }
            else
            {
                var ratingCalculated = linksModel.UrlRating;

                if (ratingCalculated <= 0)
                {
                    return Json(new { URL = httpUrl, state = "fake", rating = ratingCalculated.ToString() });
                }
                else
                {
                    return Json(new { URL = httpUrl, state = "ok", rating = ratingCalculated.ToString() });
                }

            }

           
        }

        //// PUT: api/Links/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutLinksModel(string id, LinksModel linksModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != linksModel.Url)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(linksModel).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!LinksModelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Links
        //[ResponseType(typeof(LinksModel))]
        //public IHttpActionResult PostLinksModel(LinksModel linksModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Links.Add(linksModel);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LinksModelExists(linksModel.Url))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = linksModel.Url }, linksModel);
        //}

        //// DELETE: api/Links/5
        //[ResponseType(typeof(LinksModel))]
        //public IHttpActionResult DeleteLinksModel(string id)
        //{
        //    LinksModel linksModel = db.Links.Find(id);
        //    if (linksModel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Links.Remove(linksModel);
        //    db.SaveChanges();

        //    return Ok(linksModel);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LinksModelExists(string id)
        {
            return db.Links.Count(e => e.Url == id) > 0;
        }
    }
}