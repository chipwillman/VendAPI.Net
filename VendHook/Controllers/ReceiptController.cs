namespace VendHook.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using VendHook.Models;

    public class ReceiptController : ApiController
    {
        private ReceiptContext db = new ReceiptContext();

        // GET api/Receipt
        [Queryable]
        public IQueryable<ReceiptHeader> GetReceiptHeaders()
        {
            return db.ReceiptHeaders.AsQueryable();
        }

        // GET api/Receipt/5
        public ReceiptHeader GetReceiptHeader(Guid id)
        {
            ReceiptHeader receiptheader = db.ReceiptHeaders.Find(id);
            if (receiptheader == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return receiptheader;
        }

        // PUT api/Receipt/5
        public HttpResponseMessage PutReceiptHeader(Guid id, ReceiptHeader receiptheader)
        {
            if (ModelState.IsValid && id == receiptheader.Id)
            {
                db.Entry(receiptheader).State = EntityState.Modified;

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

        // POST api/Receipt
        public HttpResponseMessage PostReceiptHeader(ReceiptHeader receiptheader)
        {
            if (ModelState.IsValid)
            {
                if (receiptheader.Id == Guid.Empty)
                {
                    receiptheader.Id = Guid.NewGuid();
                }
                db.ReceiptHeaders.Add(receiptheader);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, receiptheader);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = receiptheader.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Receipt/5
        public HttpResponseMessage DeleteReceiptHeader(Guid id)
        {
            ReceiptHeader receiptheader = db.ReceiptHeaders.Find(id);
            if (receiptheader == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ReceiptHeaders.Remove(receiptheader);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, receiptheader);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}