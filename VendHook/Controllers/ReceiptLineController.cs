
namespace VendHook.Controllers
{
    using System;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using VendHook.Models;

    public class ReceiptLineController : ApiController
    {
        private ReceiptContext db = new ReceiptContext();

        // GET api/ReceiptLine
        [Queryable]
        public IQueryable<ReceiptLine> GetReceiptLines()
        {
            return db.ReceiptLines.AsQueryable();
        }

        // GET api/ReceiptLine/5
        public ReceiptLine GetReceiptLine(Guid id)
        {
            ReceiptLine receiptline = db.ReceiptLines.Find(id);
            if (receiptline == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return receiptline;
        }

        // PUT api/ReceiptLine/5
        public HttpResponseMessage PutReceiptLine(Guid id, ReceiptLine receiptline)
        {
            if (ModelState.IsValid && id == receiptline.Id)
            {
                db.Entry(receiptline).State = EntityState.Modified;

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

        // POST api/ReceiptLine
        public HttpResponseMessage PostReceiptLine(ReceiptLine receiptline)
        {
            if (ModelState.IsValid)
            {
                if (receiptline.Id == Guid.Empty)
                {
                    receiptline.Id = Guid.NewGuid();
                }

                db.ReceiptLines.Add(receiptline);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, receiptline);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = receiptline.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/ReceiptLine/5
        public HttpResponseMessage DeleteReceiptLine(Guid id)
        {
            ReceiptLine receiptline = db.ReceiptLines.Find(id);
            if (receiptline == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ReceiptLines.Remove(receiptline);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, receiptline);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}