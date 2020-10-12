using ChequeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace ChequeApp.Controllers
{
    public class ChequesController : Controller
    {
        // GET: Cheques
        public ActionResult Index()
        {

            using (var context = new ChequeWebDBEntities())
            {
                List<CHEQUE> cheque = context.CHEQUE.Include(x => x.PAGO).Include( x=> x.PAGO.CLIENTE).ToList();

                return View(cheque);
            }
          
        }

        
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(CHEQUE cheque) 
        {

            if (ModelState.IsValid)
            {
                using (var context = new ChequeWebDBEntities())
                {
                    context.CHEQUE.Add(cheque);
                    context.SaveChanges();
                  
                }
            }

            return RedirectToAction("Index");


        }

        public ActionResult Update(int? id)
        {

            try
            {
                if (id == null)
                {
                    new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                else
                {
                    using (var context = new ChequeWebDBEntities())
                    {
                        var cheque = context.CHEQUE.Include(x => x.PAGO).Include(x => x.PAGO.CLIENTE).FirstOrDefault(x=> x.Id == id);
                        return View(cheque);
                    }
                }
            }
            catch (Exception ex )
            {

                return View(ex.Message);
            }


            return View();
        }

        [HttpPost]
        public ActionResult Update(CHEQUE cheque)
        {
            using (var context = new ChequeWebDBEntities())
            {
                var chequeEdit = context.CHEQUE.Include(x => x.PAGO).Include(x => x.PAGO.CLIENTE).SingleOrDefault(x => x.ClienteId == cheque.ClienteId);
                context.CHEQUE.Add(chequeEdit);
                context.Entry(chequeEdit).State = EntityState.Modified;
                context.SaveChanges();

            }

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int id )
        {
            using(var context = new ChequeWebDBEntities())
            {
                var cheque = context.CHEQUE.Find(id);
                context.CHEQUE.Remove(cheque);
                return View();
            }
        }
    }
}