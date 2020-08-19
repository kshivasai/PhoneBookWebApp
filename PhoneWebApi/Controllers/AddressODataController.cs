using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using Phone.DAL;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.Cors;
using PhoneWebApi.BasicAuth;

namespace PhoneWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [BasicAuthentication]
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Phone.DAL;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Address>("AddressOData");
    builder.EntitySet<City>("Cities"); 
    builder.EntitySet<Country>("Countries"); 
    builder.EntitySet<Person>("People"); 
    builder.EntitySet<State>("States"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AddressODataController : ODataController
    {
        private PhoneContext db = new PhoneContext();

        // GET: odata/AddressOData
        [EnableQuery]
        public IQueryable<Address> GetAddressOData()
        {
            return db.Addresses;
        }

        // GET: odata/AddressOData(5)
        [EnableQuery]
        public SingleResult<Address> GetAddress([FromODataUri] int key)
        {
            return SingleResult.Create(db.Addresses.Where(address => address.ID == key));
        }

        // PUT: odata/AddressOData(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Address> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = db.Addresses.Find(key);
            if (address == null)
            {
                return NotFound();
            }

            patch.Put(address);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(address);
        }

        // POST: odata/AddressOData
        public IHttpActionResult Post(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Addresses.Add(address);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AddressExists(address.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(address);
        }

        // PATCH: odata/AddressOData(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Address> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = db.Addresses.Find(key);
            if (address == null)
            {
                return NotFound();
            }

            patch.Patch(address);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(address);
        }

        // DELETE: odata/AddressOData(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Address address = db.Addresses.Find(key);
            if (address == null)
            {
                return NotFound();
            }

            db.Addresses.Remove(address);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/AddressOData(5)/City
        [EnableQuery]
        public SingleResult<City> GetCity([FromODataUri] int key)
        {
            return SingleResult.Create(db.Addresses.Where(m => m.ID == key).Select(m => m.City));
        }

        // GET: odata/AddressOData(5)/Country
        [EnableQuery]
        public SingleResult<Country> GetCountry([FromODataUri] int key)
        {
            return SingleResult.Create(db.Addresses.Where(m => m.ID == key).Select(m => m.Country));
        }

        // GET: odata/AddressOData(5)/Person
        [EnableQuery]
        public SingleResult<Person> GetPerson([FromODataUri] int key)
        {
            return SingleResult.Create(db.Addresses.Where(m => m.ID == key).Select(m => m.Person));
        }

        // GET: odata/AddressOData(5)/State
        [EnableQuery]
        public SingleResult<State> GetState([FromODataUri] int key)
        {
            return SingleResult.Create(db.Addresses.Where(m => m.ID == key).Select(m => m.State));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int key)
        {
            return db.Addresses.Count(e => e.ID == key) > 0;
        }
    }
}
