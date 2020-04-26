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
using System.Web;
using T2.Models;
using Google.Apis.Auth;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace T2.Controllers
{
    public class RatiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        

        //// GET: api/Raties/5
        //[ResponseType(typeof(RatiesModel))]
        //public IHttpActionResult GetRatiesModel(long id)
        //{
        //    RatiesModel ratiesModel = db.Raties.Find(id);
        //    if (ratiesModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(ratiesModel);
        //}

        //// PUT: api/Raties/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutRatiesModel(long id, RatiesModel ratiesModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != ratiesModel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(ratiesModel).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RatiesModelExists(id))
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

        // POST: api/Raties
        [ResponseType(typeof(RatiesModel))]
        public async Task<IHttpActionResult> PostRatiesModelAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strToken = HttpContext.Current.Request.Form["GoogleToken"];
            var strMail = HttpContext.Current.Request.Form["GoogleMail"];
            var strTrue = HttpContext.Current.Request.Form["IsTrue"];
            var strFake = HttpContext.Current.Request.Form["IsFake"];
            var strLink= HttpContext.Current.Request.Form["Link"];
            bool boolTrue;
            bool boolFake;


            try
            {
                // Check if user is authenticated
               //var validPayload = await GoogleJsonWebSignature.ValidateAsync(strToken);
                
                //validPayload != null
                if (null == null)
                {
                    //validPayload.Email == strMail
                    if (strMail == strMail)
                    {

                        // Check if user exists, if not I will create
                        string myId;
                        try
                        {
                            var myUser= db.Users.Single(a => a.Email == strMail);
                            myId = myUser.Id;
                        }
                        catch (Exception exc)
                        { 
                        // Create new user
                        var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

                        var newuser = new ApplicationUser()
                        {
                            UserName = strMail,
                            Email = strMail,
                            EmailConfirmed = true,
                            FirstName = "Name",
                            LastName = "Surname",
                            Level = 3,
                            JoinDate = DateTime.Now,
                            NCorrectAnswers = 0,
                            NFaultAnswers = 0
                        };

                        manager.Create(newuser, "MySimpleP@ssword!!!");
                        var simpleUser = manager.FindByName(strMail);

                        manager.AddToRoles(simpleUser.Id, new string[] { "User" });
                        myId = db.Users.Single(a => a.Email == strMail).Id;

                        }

                    // Write in tables Raties

                    if (strTrue == "1")
                    {
                        boolTrue = true;
                    }
                    else
                    {
                        boolTrue = false;
                    }

                    if (strFake == "1")
                    {
                        boolFake = true;
                    }
                    else
                    {
                        boolFake = false;
                    }

                    // Check if link exists, if not I save it             
                    LinksModel linksModel = db.Links.Find(strLink);
                    if (linksModel==null)
                    {
                        LinksModel myLink2 = new LinksModel();
                        myLink2.Url = strLink;
                        myLink2.DateCertified = DateTime.Now;
                        myLink2.UrlRating = 0;
                        myLink2.IsFalseCertified = false;
                        myLink2.IsTrueCertified = false;
                        db.Links.Add(myLink2);
                        db.SaveChanges();
                    }

                    // Save Rating
                    RatiesModel myRate = new RatiesModel();
                    myRate.DateRate = DateTime.Now;
                    myRate.Link = strLink;
                    myRate.IsTrue = boolTrue;
                    myRate.IsFake = boolFake;
                    myRate.IdUser = myId;
                    db.Raties.Add(myRate);
                    db.SaveChanges();

                    // Recalc rating and save in DB
                    float ratingCalculated = 0;
                    string SQLQuery = "SELECT SUM(FinalRate) as Rating FROM(SELECT RT.Id, RT.Link, RT.IdUser, RT.IsTrue * UserRate / 100.0 as FinalRate  FROM[dbo].[RatiesModels] as RT inner join (SELECT Id, FirstName, 50 + (NCorrectAnswers * 100 / (NCorrectAnswers + 3 * NFaultAnswers)) - ((300 * NFaultAnswers) / (NCorrectAnswers + 3 * NFaultAnswers)) as UserRate FROM[AspNetUsers] where(NCorrectAnswers > 0 OR NFaultAnswers > 0)) Rates on Rates.Id = RT.IdUser where RT.IsTrue = 1 UNION SELECT RT.Id, RT.Link, RT.IdUser, RT.IsFake * UserRate / -100.0 as FinalRate  FROM[dbo].[RatiesModels] as RT inner join (SELECT Id, FirstName, 50 + (NCorrectAnswers * 100 / (NCorrectAnswers + 3 * NFaultAnswers)) - ((300 * NFaultAnswers) / (NCorrectAnswers + 3 * NFaultAnswers)) as UserRate FROM[dbo].[AspNetUsers] where(NCorrectAnswers > 0 OR NFaultAnswers > 0)) Rates on Rates.Id = RT.IdUser where RT.IsFake = 1) Somma group by Link HAVING Link = @Link";
                    ratingCalculated = db.Database.SqlQuery<float>(SQLQuery, new SqlParameter("@link", strLink)).FirstOrDefault();
                    var myLink = db.Links.Single(a => a.Url== strLink);
                    myLink.UrlRating = ratingCalculated;
                    db.SaveChanges();

                    // Return JSON
                    if (ratingCalculated <= 0)
                    {
                        return Json(new { URL = strLink, state = "fake", rating = ratingCalculated.ToString() });
                    }
                    else
                    {
                        return Json(new { URL = strLink, state = "ok", rating = ratingCalculated.ToString() });
                    }

                    }
                    else

                    {
                        return BadRequest(ModelState);

                    }

                }
                else
                {
                    return BadRequest(ModelState);

                }
            }
            catch (Exception exp)
            {
                return BadRequest(ModelState);
            } 
           
        }

    

    //// DELETE: api/Raties/5
    //[ResponseType(typeof(RatiesModel))]
    //public IHttpActionResult DeleteRatiesModel(long id)
    //{
    //    RatiesModel ratiesModel = db.Raties.Find(id);
    //    if (ratiesModel == null)
    //    {
    //        return NotFound();
    //    }

    //    db.Raties.Remove(ratiesModel);
    //    db.SaveChanges();

    //    return Ok(ratiesModel);
    //}

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