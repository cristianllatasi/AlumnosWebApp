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
using AlumnosWebApp.Models;

namespace AlumnosWebApp.Controllers
{
    public class TareasController : ApiController
    {
        private AlumnosWebAppContext db = new AlumnosWebAppContext();

        // GET api/Tareas
        public IEnumerable<Tarea> GetTareas()
        {
            return db.Tareas.AsEnumerable();
        }

        // GET api/Tareas/5
        public Tarea GetTarea(int id)
        {
            Tarea tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tarea;
        }

        // PUT api/Tareas/5
        public HttpResponseMessage PutTarea(int id, Tarea tarea)
        {
            if (ModelState.IsValid && id == tarea.Id)
            {
                db.Entry(tarea).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Tareas
        public HttpResponseMessage PostTarea(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.Tareas.Add(tarea);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tarea);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tarea.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Tareas/5
        public HttpResponseMessage DeleteTarea(int id)
        {
            Tarea tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Tareas.Remove(tarea);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tarea);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}