using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coreprojecr2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreprojecr2.Controllers
{
    [Authorize]
    public class StudentController : Controller

    {
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;
        StudentDataAccess studentDataAccess = null;

        [Obsolete]
        public StudentController(IHostingEnvironment hostingEnvironment)
        {
            studentDataAccess = new StudentDataAccess();
            this.hostingEnvironment = hostingEnvironment;
        }

        
        public ActionResult Start()
        {
            IEnumerable<Student> students = studentDataAccess.GetAllStudent();
            return View(students);
        }


        // GET: Student
        public ActionResult Index()
        {
            IEnumerable<Student> students = studentDataAccess.GetAllStudent();
            return View(students);
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {   
            Student student = studentDataAccess.GetStudentData(id);
            if (student == null)
            {
                return View("404.chtml", id.Value);
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public ActionResult Create(Student student)
        {
          
            try
            {
                string uniqueFileName = null;

                if (student.Photo != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "uploadimages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + student.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    student.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                }

                Student newStudent = new Student();
                newStudent.FirstName = student.FirstName;
                newStudent.LastName = student.LastName;
                newStudent.Email = student.Email;
                newStudent.PhotoName = uniqueFileName;
                studentDataAccess.AddStudent(newStudent);
                return RedirectToAction(nameof(Start));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            Student student = studentDataAccess.GetStudentData(id);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            try
            {
                // TODO: Add update logic here

                studentDataAccess.UpdateStudent(student);

                return RedirectToAction(nameof(Start));
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            Student student = studentDataAccess.GetStudentData(id);
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Student student)
        {
            try
            {
                // TODO: Add delete logic here
                studentDataAccess.DeleteStudent(student.Id);
                return RedirectToAction(nameof(Start));
            }
            catch
            {
                return View();
            }
        }
    }
}