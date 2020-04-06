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
using KmovieS.Utilities;

namespace KmovieS.Controllers
{
    public class FileUploadsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FileUploads
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public IQueryable<FileUpload> GetfileUpload()
        {
            return db.fileUpload;
        }

        // GET: api/FileUploads/5
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult GetFileUpload(int id)
        {
            string myURI = Url.Request.RequestUri.ToString();
            myURI = GlobalVariable.RetrieveFullAPIName(myURI);
            string myMethod = Url.Request.Method.ToString();
            FileUpload fileUpload = db.fileUpload.Find(id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return Ok(fileUpload);
        }

        // PUT: api/FileUploads/5
        [HttpPut]
        [Authorize(Roles = "Admin, User")]
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
                string myURI = Url.Request.RequestUri.ToString();
                string myAPIFullName = GlobalVariable.RetrieveFullAPIName(myURI);
                string myMethod = Url.Request.Method.ToString();
                string myAPIFullNameMethod = myAPIFullName + "-" + myMethod;

                //Controllo se ci sono abbastanza punti
                if (GlobalVariable.CheckPointCall(User.Identity.GetUserId(), myAPIFullNameMethod))
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
                            if ((mediaupload.mediatype.ToUpper() != "VIDEO") && (mediaupload.mediatype.ToUpper() != "VIDEO380") && (mediaupload.mediatype.ToUpper() != "IMAGE360") && (mediaupload.mediatype.ToUpper() != "IMAGE") && (mediaupload.mediatype.ToUpper() != "DOCUMENT"))
                            {
                                mediaupload.mediatype = "COMMON";
                            }
                            if (httpUploadedObjectReferenceId == "")
                            {
                                httpUploadedObjectReferenceId = "Unknown";
                            }
                            if (httpUploadedSourceFile == "")
                            {
                                httpUploadedSourceFile = "Unknown";
                            }

                            var MyBaseAddress = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority);
                            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/" + User.Identity.GetUserId() + "/" + httpUploadedObjectReferenceId + "/" + mediaupload.mediatype + "/"), httpPostedFile.FileName);
                            int length = httpPostedFile.ContentLength;
                            mediaupload.mediadata = new byte[length];
                            httpPostedFile.InputStream.Read(mediaupload.mediadata, 0, length);
                            mediaupload.medianame = Path.GetFileName(httpPostedFile.FileName);
                            mediaupload.mediadateupload = DateTime.Now;
                            // Nel campo TAG passato tramite APP deve esserci una di queste diciture: FOTO360_1, FOTO360_2, FOTO360_3, FOTO360_4, VIDEO, VIDEO380
                            mediaupload.mediatag = httpUploadedTagFile.ToString();
                            mediaupload.objectReferenceId = httpUploadedObjectReferenceId.ToString();
                            mediaupload.idUser = User.Identity.GetUserId();
                            mediaupload.mediaextension = Path.GetExtension(fileSavePath);
                            mediaupload.mediasize = length;
                            mediaupload.mediasource = httpUploadedSourceFile;
                            mediaupload.mediapath = fileSavePath;
                            mediaupload.urlpath = MyBaseAddress;
                            db.fileUpload.Add(mediaupload);
                            db.SaveChanges();
                            //Calcolo l'ID creato
                            long id = mediaupload.mediaid;

                            //Crea la directory e salva il file 
                            var MyFilePath = "/UploadedFiles/" + User.Identity.GetUserId() + "/" + httpUploadedObjectReferenceId + "/" + mediaupload.mediatype + "/" + id.ToString() + "/";
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + MyFilePath));
                            fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles/" + User.Identity.GetUserId() + "/" + httpUploadedObjectReferenceId + "/" + mediaupload.mediatype + "/" + id.ToString() + "/"), httpPostedFile.FileName);
                            httpPostedFile.SaveAs(fileSavePath);

                            var MyJsonPath = MyBaseAddress + MyFilePath + httpPostedFile.FileName;

                            // Aggiorno il record con il percorso fisico e virtuale che include l'ID
                            var mediauploaded = db.fileUpload.Single(a => a.mediaid == id);
                            mediauploaded.mediapath = fileSavePath;
                            mediaupload.urlpath = MyJsonPath;
                            db.SaveChanges();

                            //Scrive nella tabella LogCalls la chiamata
                            LogCalls myLog = new LogCalls();
                            myLog.APIFullname = "temp";
                            myLog.idUser = mediaupload.idUser;
                            myLog.calldate = mediaupload.mediadateupload;
                            myLog.cost = 0;
                            db.logCall.Add(myLog);
                            db.SaveChanges();

                            //Calcolo l'ID creato e lo metto nella variabile globale
                            GlobalVariable.LogCallID = myLog.callid;

                            // Recupero il costo dell'API                    
                            int myAPICost = GlobalVariable.ApiCost(myAPIFullNameMethod);

                            // Scalo i punti all'utente
                            GlobalVariable.UserPointSubtract(myAPICost, User.Identity.GetUserId());

                            // Aggiorno nella tabella il record LOGCALLS il record interessato dall'aggiornamento
                            var RecordLogCalls = db.logCall.Single(a => a.callid == GlobalVariable.LogCallID);
                            RecordLogCalls.APIFullname = myAPIFullNameMethod;
                            RecordLogCalls.cost = myAPICost;
                            db.SaveChanges();

                            // Azzero le variabili globali LOGCallID
                            GlobalVariable.LogCallID = 0;


                            //Restituisce il JSON contenente URL assoluto e ID Media
                            return Json(new { mediaURL = MyJsonPath, mediaID = id });
                        }
                    }
                    return Ok("Media is not Uploaded");
                }
                else
                {
                    return Ok("Too less credits");
                }
            }
            catch (Exception e)
            {
                return Ok("Media error");
            }

        }

        // DELETE: api/FileUploads/5 -> Passo il mediaid che la APP avrà ricevuto in fase di upload tramite il JSON di ritorno
        [HttpDelete]
        [Authorize(Roles = "Admin, User")]
        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult DeleteFileUpload(int id)
        {
            string myURI = Url.Request.RequestUri.ToString();
            string myAPIFullName = GlobalVariable.RetrieveFullAPIName(myURI);
            string myMethod = Url.Request.Method.ToString();
            string myAPIFullNameMethod = myAPIFullName + "-" + myMethod;

            if (GlobalVariable.CheckPointCall(User.Identity.GetUserId(), myAPIFullNameMethod))
            {

                FileUpload fileUpload = db.fileUpload.Find(id);
                if (fileUpload == null)
                {
                    return Ok("Media not found");
                }

                //Se esiste cancello il file fisicamente dal disco e la cartella in cui è contenuto
                if (File.Exists(fileUpload.mediapath))
                {
                    try
                    {
                        File.Delete(fileUpload.mediapath);
                        Directory.Delete(Path.GetDirectoryName(fileUpload.mediapath));
                    }
                    catch (Exception ex)
                    {
                        return Ok("Media error");
                    }
                }

                // Rimuovo il record dalla tabella FileUpload         
                db.fileUpload.Remove(fileUpload);
                db.SaveChanges();

                //Scrive nella tabella LogCalls la chiamata
                LogCalls myLog = new LogCalls();
                myLog.APIFullname = myAPIFullNameMethod;
                myLog.idUser = User.Identity.GetUserId();
                myLog.calldate = DateTime.Now;
                // Il costo della cancellazione è gratuito
                myLog.cost = 0;
                db.logCall.Add(myLog);
                db.SaveChanges();

                //Calcolo l'ID creato e lo metto nella variabile globale
                GlobalVariable.LogCallID = myLog.callid;

                // Recupero il costo dell'API                    
                int myAPICost = GlobalVariable.ApiCost(myAPIFullNameMethod);

                // Scalo i punti all'utente
                GlobalVariable.UserPointSubtract(myAPICost, User.Identity.GetUserId());

                // Aggiorno nella tabella il record LOGCALLS il record interessato dall'aggiornamento
                var RecordLogCalls = db.logCall.Single(a => a.callid == GlobalVariable.LogCallID);
                RecordLogCalls.APIFullname = myAPIFullNameMethod;
                RecordLogCalls.cost = myAPICost;
                db.SaveChanges();

                // Azzero le variabili globali LOGCallID
                GlobalVariable.LogCallID = 0;

                return Ok("Media deleted");
            }
            else
            {
                return Ok("Too less credits");
            }

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