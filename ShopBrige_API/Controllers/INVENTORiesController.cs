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
using ShopBrige_API;

namespace ShopBrige_API.Controllers
{
    public class INVENTORiesController : ApiController
    {
        private ShopBridgeEntities db = new ShopBridgeEntities();

        // GET: api/INVENTORies
        public IQueryable<INVENTORY> GetINVENTORY()
        {
            return db.INVENTORY;
        }

        // GET: api/INVENTORies/5
        [ResponseType(typeof(INVENTORY))]
        public IHttpActionResult GetINVENTORY(int id)
        {
            INVENTORY iNVENTORY = db.INVENTORY.Find(id);
            if (iNVENTORY == null)
            {
                return NotFound();
            }

            return Ok(iNVENTORY);
        }

       
        // PUT: api/INVENTORies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutINVENTORY(int id, INVENTORY iNVENTORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iNVENTORY.INV_ID)
            {
                return BadRequest();
            }

            db.Entry(iNVENTORY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!INVENTORYExists(id))
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

        // POST: api/INVENTORies
        [ResponseType(typeof(INVENTORY))]
        public IHttpActionResult PostINVENTORY(INVENTORY iNVENTORY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (INVENTORYCodeExists(iNVENTORY.INV_CODE))
            {
                return NotFound();
            }
            db.INVENTORY.Add(iNVENTORY);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iNVENTORY.INV_ID }, iNVENTORY);
        }

        // DELETE: api/INVENTORies/5
        [ResponseType(typeof(INVENTORY))]
        public IHttpActionResult DeleteINVENTORY(int id)
        {
            INVENTORY iNVENTORY = db.INVENTORY.Find(id);
            if (iNVENTORY == null)
            {
                return NotFound();
            }

            db.INVENTORY.Remove(iNVENTORY);
            db.SaveChanges();

            return Ok(iNVENTORY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool INVENTORYExists(int id)
        {
            return db.INVENTORY.Count(e => e.INV_ID == id) > 0;
        }

        private bool INVENTORYCodeExists(string code)
        {
            return db.INVENTORY.Count(e => e.INV_CODE == code) > 0;
        }
    }
}