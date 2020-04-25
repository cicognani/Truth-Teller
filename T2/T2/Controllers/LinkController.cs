using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using T2.Models;
using T2.Utilities;

namespace T2.Controllers
{
    public class LinkController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

   
        // GET: api/Link/url (with ID)
        [HttpGet]
        [ResponseType(typeof(LinksModel))]
        public IHttpActionResult GetLink(string url)
        {
            LinksModel link = db.Links.Find(url);
            if (link == null)
            {
                return NotFound();
            }

            return Ok(link);
        }

   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

     
    }
}