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
using KmovieS.Models;

namespace KmovieS.Controllers
{
    public class FileUploadsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FileUploads
        public IQueryable<FileUpload> GetfileUpload()
        {
            return db.fileUpload;
        }

        // GET: api/FileUploads/5
        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult GetFileUpload(int id)
        {
            FileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return Ok(fileUpload);
        }

        // PUT: api/FileUploads/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFileUpload(int id, FileUpload fileUpload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileUpload.mediaid)
            {
                return BadRequest();
            }

            db.Entry(fileUpload).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileUploadExists(id))
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

        // POST: api/FileUploads
        [ResponseType(typeof(FileUpload))]
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IHttpActionResult PostFileUpload()
        {


            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded media from the Files collection  

                    var httpUploadedObjectReferenceId = HttpContext.Current.Request.Form["UploadedObjectReferenceId"];
                    var httpUploadedTypeFile = HttpContext.Current.Request.Form["UploadedType"];
                    var httpUploadedTagFile = HttpContext.Current.Request.Form["UploadedTag"];
                    var httpUploadedSourceFile = HttpContext.Current.Request.Form["UploadedMediaSource"];
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadedMedia"];
                    if (httpPostedFile != null)
                    {
                        FileUpload mediaupload = new FileUpload();
                        mediaupload.mediatype = httpUploadedTypeFile.ToString();
                        if ((mediaupload.mediatype.ToUpper() !="VIDEO") && (mediaupload.mediatype.ToUpper() != "IMAGE") && (mediaupload.mediatype.ToUpper() != "DOCUMENT"))
                        {
                            mediaupload.mediatype = "COMMON";
                        }
                        if(httpUploadedObjectReferenceId=="")
                        {
                            httpUploadedObjectReferenceId = "Unknown";
                        }
                        if (httpUploadedSourceFile == "")
                        {
                            httpUploadedSourceFile = "Unknown";
                        }

                        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/" + User.Identity.GetUserId() + "/" + httpUploadedObjectReferenceId + "/" + mediaupload.mediatype + "/"), httpPostedFile.FileName);
                        int length = httpPostedFile.ContentLength;
                        mediaupload.mediadata = new byte[length];
                        httpPostedFile.InputStream.Read(mediaupload.mediadata, 0, length);
                        mediaupload.medianame = Path.GetFileName(httpPostedFile.FileName);
                        mediaupload.mediadateupload = DateTime.Now;
                        mediaupload.mediatag = httpUploadedTagFile.ToString();
                        mediaupload.objectReferenceId = httpUploadedObjectReferenceId.ToString();
                        mediaupload.idUser = User.Identity.GetUserId();
                        mediaupload.mediaextension = Path.GetExtension(fileSavePath);
                        mediaupload.mediasize = length;                     
                        mediaupload.mediasource = httpUploadedSourceFile;
                        db.fileUpload.Add(mediaupload);
                        db.SaveChanges();

                        //Crea la directory e salva il file 
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedFiles/" + User.Identity.GetUserId() + "/" + httpUploadedObjectReferenceId + "/" + mediaupload.mediatype + "/"));
                        httpPostedFile.SaveAs(fileSavePath);
                        return Ok("Media Uploaded");
                    }
                }
                return Ok("Media is not Uploaded");
            }
            catch (Exception e)
            {
                return Ok("Media error");
            }

        }

        // DELETE: api/FileUploads/5
        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult DeleteFileUpload(int id)
        {
            FileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            db.fileUpload.Remove(fileUpload);
            db.SaveChanges();

            return Ok(fileUpload);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileUploadExists(int id)
        {
            return db.fileUpload.Count(e => e.mediaid == id) > 0;
        }
    }
}