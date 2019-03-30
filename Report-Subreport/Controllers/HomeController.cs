using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Report_Subreport.Models;

namespace Report_Subreport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<Student> students = GetStudents();

            var booleanGroupQuery =
                from student in students
                group student by student.Scores.Average() >= 80; //pass or fail!


            var stuentList = new List<Student>();
            var keys = new List<string>();

            foreach (var bgq in booleanGroupQuery)
            {
                

                var key = bgq.Key ? "Score is greater than 80" : "Score is less than 80";
               
                keys.Add(key);

                foreach (var groupDetails in bgq)
                {
                    var student = new Student();
                    student.Key = key;
                    student.First = groupDetails.First;
                    student.Last = groupDetails.Last;
                    student.ID = groupDetails.ID;
                    student.Average = groupDetails.Scores.Average();

                    stuentList.Add(student);
                }
            }

            ViewBag.StudentList = stuentList;
            ViewBag.Keys = keys;
            
            return View();
        }

        public static List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores. source: msdn
            List<Student> students = new List<Student>
        {
           new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
           new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
           new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
           new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
           new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}} 
        };

            return students;

        }


    }
}
