using MutualFriend.Models;
using MutualFriend.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MutualFriend.Controllers
{
    public class FriendController : Controller
    {
        // GET: Friend
        FriendDB fd = new FriendDB();
        TestEntities _DB = new TestEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(fd.ListAll(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add(FormCollection form)
        {
            Employee emp = new Employee();
            emp.name = form["TxtName"].ToString();
            emp.IsActive = true;
            _DB.Employees.Add(emp);
            _DB.SaveChanges();
            int empId = _DB.Employees.Where(x => x.name == emp.name).Select(x => x.id).FirstOrDefault();

            foreach (string itemid in form.AllKeys)
            {
                MutualFriend.Models.Entity.MutualFriend mf = new MutualFriend.Models.Entity.MutualFriend();
                if (!itemid.Contains("txtName"))
                {
                    mf.PersonMutual = Convert.ToInt32(itemid);
                    mf.Person = empId;
                    _DB.MutualFriends.Add(mf);
                    _DB.SaveChanges();
                }
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact(int Id)
        {
            MutualFriend.Models.Entity.MutualFriend mf = new MutualFriend.Models.Entity.MutualFriend();
            List<FriendModel> mfrd = new List<FriendModel>();
            mfrd =  fd.getMutual(Id);
            return View(mfrd);
        }
    }
}