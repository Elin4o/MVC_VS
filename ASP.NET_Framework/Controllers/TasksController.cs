using ASP.NET_Framework.Data;
using ASP.NET_Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_Framework.Controllers
{
    public class TasksController : Controller
    {
        // GET: Tasks
        public ActionResult Index(string SortingOrder)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            TaskDAO taskDAO = new TaskDAO();

            tasks = taskDAO.FetchAll();

            return View("Index",tasks);
        }

        public ActionResult Details(int id)
        {
            TaskDAO taskDAO = new TaskDAO();
            TaskModel task = taskDAO.FetchOne(id);

            return View("Details", task);
        }


        public ActionResult Create()
        {
            var categoriesList = new List<string>() { "University", "School" ,"Work" ,"Shopping" ,"Chores" ,"Default"};
            ViewBag.list = categoriesList;
            return View("Create");
        }

        public ActionResult Edit(int id)
        {
            var categoriesList = new List<string>() { "University", "School", "Work", "Shopping", "Chores", "Default" };
            ViewBag.list = categoriesList;

            TaskDAO taskDAO = new TaskDAO();
            TaskModel task = taskDAO.FetchOne(id);

            return View("Create",task);
        }

        public ActionResult ProcessCreate(TaskModel taskModel)
        {
            TaskDAO taskDAO = new TaskDAO();

            taskDAO.CreateOrUpdate(taskModel);

            return View("Details", taskModel);
        }
        public ActionResult Delete(int id)
        {       
            TaskDAO taskDAO = new TaskDAO();
            taskDAO.Delete(id);

            List<TaskModel> tasks = taskDAO.FetchAll();

            return View("Index", tasks);
        }
    }

}