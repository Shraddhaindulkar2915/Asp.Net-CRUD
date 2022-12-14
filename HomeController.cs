using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(Student_data s)
        {
            // To open a connection to the database
            using (var context = new StudentEntities())
            {
                // Add data to the particular table
                context.Student_data.Add(s);

                // save the changes
                context.SaveChanges();
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;

            return View();
        }

        [HttpGet] // Set the attribute to Read
        public ActionResult Read()
        {
            using (var context = new StudentEntities())
            {

                // Return the list of data from the database
                var data = context.Student_data.ToList();
                return View(data);
            }
        }
        public ActionResult Update(int Studentid)
        {
            using (var context = new StudentEntities())
            {
                var data = context.Student_data.Where(x => x.Student_Id == Studentid).SingleOrDefault();
                return View(data);
            }
        }

        // To specify that this will be
        // invoked when post method is called
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int Studentid, Student_data model)
        {
            using (var context = new StudentEntities())
            {

                // Use of lambda expression to access
                // particular record from a database
                var data = context.Student_data.FirstOrDefault(x => x.Student_Id == Studentid);

                // Checking if any such record exist
                if (data != null)
                {
                    data.Student_Id = model.Student_Id;
                    data.Student_Name = model.Student_Name;
                    data.Student_Email = model.Student_Email;
                    data.Student_Contact = model.Student_Contact;
                    context.SaveChanges();

                    // It will redirect to
                    // the Read method
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }

        }
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Studentid)
        {
            using (var context = new StudentEntities())
            {
                var data = context.Student_data.FirstOrDefault(x => x.Student_Id == Studentid);
                if (data != null)
                {
                    context.Student_data.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }
    }
}