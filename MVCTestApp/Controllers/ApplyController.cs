using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCTestApp.Infrastructure;
using MVCTestApp.ViewModels;
using CreditCardApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestApp.Controllers
{
    public class ApplyController : Controller
    {
        private readonly ICreditCardApplicationRepository repository;

        public ApplyController(ICreditCardApplicationRepository repository)
        {
            this.repository = repository;
        }

        // GET: ApplyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApplyController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NewCreditCardApplicationDetails applicationDetails )
        {
            if (ModelState.IsValid)
            {
                return View(applicationDetails);
            }
            var CreditCardApplication = new CreditCardApplication
            {
                 FirstName=applicationDetails.FirstName,
                 LastName=applicationDetails.LastName,
                 Age=applicationDetails.Age.Value,
                 GrossAnnualIncome=applicationDetails.GrossAnnualIncome.Value,
                  FrequentFlyerNumber=applicationDetails.FrequentFlyerNumber

            };

            return View();
        }
        // GET: ApplyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
