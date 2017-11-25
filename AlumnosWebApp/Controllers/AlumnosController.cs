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
    public class AlumnosController : ApiController
    {
        private AlumnosWebAppContext db = new AlumnosWebAppContext();

        // GET api/Alumnos
        public IEnumerable<Alumno> GetAlumnoes()
        {
            return db.Alumnoes.AsEnumerable();
        }

        // GET api/Alumnos/5
        public Alumno GetAlumno(int id)
        {
            Alumno alumno = db.Alumnoes.Find(id);
            if (alumno == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return alumno;
        }

        // PUT api/Alumnos/5
        public HttpResponseMessage PutAlumno(int id, Alumno alumno)
        {
            if (ModelState.IsValid && id == alumno.Id)
            {
                db.Entry(alumno).State = EntityState.Modified;

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

        // POST api/Alumnos
        public HttpResponseMessage PostAlumno(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Alumnoes.Add(alumno);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, alumno);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = alumno.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Alumnos/5
        public HttpResponseMessage DeleteAlumno(int id)
        {
            Alumno alumno = db.Alumnoes.Find(id);
            if (alumno == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Alumnoes.Remove(alumno);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, alumno);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}