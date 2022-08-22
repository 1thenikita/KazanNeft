using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KazanNeft.DB.Entities;

namespace KazanNeft.WebAPI.Controllers
{
    public class AssetsController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/Assets
        public IQueryable<Asset> GetAssets()
        {
            return db.Assets;
        }

        // GET: api/Assets/5
        [ResponseType(typeof(Asset))]
        public async Task<IHttpActionResult> GetAsset(long id)
        {
            Asset asset = await db.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        // PUT: api/Assets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAsset(long id, Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset.ID)
            {
                return BadRequest();
            }

            db.Entry(asset).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // POST: api/Assets
        [ResponseType(typeof(Asset))]
        public async Task<IHttpActionResult> PostAsset(Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Assets.Add(asset);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = asset.ID }, asset);
        }

        // DELETE: api/Assets/5
        [ResponseType(typeof(Asset))]
        public async Task<IHttpActionResult> DeleteAsset(long id)
        {
            Asset asset = await db.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            db.Assets.Remove(asset);
            await db.SaveChangesAsync();

            return Ok(asset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetExists(long id)
        {
            return db.Assets.Count(e => e.ID == id) > 0;
        }
    }
}