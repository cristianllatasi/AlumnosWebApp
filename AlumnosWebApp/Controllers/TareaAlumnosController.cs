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
    public class TareaAlumnosController : ApiController
    {
        private AlumnosWebAppContext db = new AlumnosWebAppContext();

        // GET api/TareaAlumnos
        public IEnumerable<TareaAlumno> GetTareaAlumnoes()
        {
            var tareaalumnoes = db.TareaAlumnoes.Include(t => t.Tarea).Include(t => t.Alumno);
            return tareaalumnoes.AsEnumerable();
        }

        // GET api/TareaAlumnos/5
        public TareaAlumno GetTareaAlumno(int id)
        {
            TareaAlumno tareaalumno = db.TareaAlumnoes.Find(id);
            if (tareaalumno == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tareaalumno;
        }

        // PUT api/TareaAlumnos/5
        public HttpResponseMessage PutTareaAlumno(int id, TareaAlumno tareaalumno)
        {
            if (ModelState.IsValid && id == tareaalumno.IdTarea)
            {
                db.Entry(tareaalumno).State = EntityState.Modified;

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

        // POST api/TareaAlumnos
        public HttpResponseMessage PostTareaAlumno(TareaAlumno tareaalumno)
        {
            if (ModelState.IsValid)
            {
                db.TareaAlumnoes.Add(tareaalumno);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tareaalumno);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tareaalumno.IdTarea }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/TareaAlumnos/5
        public HttpResponseMessage DeleteTareaAlumno(int id)
        {
            TareaAlumno tareaalumno = db.TareaAlumnoes.Find(id);
            if (tareaalumno == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.TareaAlumnoes.Remove(tareaalumno);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tareaalumno);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}