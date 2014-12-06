using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoPaper.Models;

namespace AutoPaper.Controllers
{
    public class IndividualCentreController : Controller
    {
        private PaperShareDBContext db = new PaperShareDBContext();

        public ActionResult Default()
        {
            int userID = Convert.ToInt32(Request.Cookies["userID"].Value);
            var noticesList = ((from o in db.notice_table
                              where o.userID == userID
                              select o)).ToList<notices>();
            ViewBag.noticesList = noticesList;
            return View();
        }
        public ActionResult Account()
        {
            return View();
        }
        public bool KeyChangeConfirm()
        {
            int userID = Convert.ToInt32(Request.Cookies["userID"].Value);
            string oldPassword = Request.Form["user-key"];
            string newPassword1 = Request.Form["user-new-key"];
            string newPassword2 = Request.Form["user-new-key-confirm"];
            //判断用户输入密码是否匹配
            var Users = ((from o in db.user_table
                              where o.ID == userID
                              select o)).ToArray<users>();
            
            //匹配成功，更新数据库，返回true
            if (Users[0].keyhash == oldPassword)
                if (newPassword1 == newPassword2)
                {
                    Users[0].keyhash = newPassword1;
                    db.SaveChanges();
                    return true;
                }
            return false;
        }
        public ActionResult MyCollectQuestion()
        {
            return View();
        }
        public ActionResult MyCollectPapers()
        {
            //获取用户id
            int userID = Convert.ToInt32(Request.Cookies["userID"].Value);
            //找出收藏试卷id
            var paperIdList = (from o in db.PU_table
                               where o.userID == userID && o.logAddr == null
                               select o.paperID).ToList<int>();
            List<papers> paperList = new List<papers>();
            //根据id查找papers表
            foreach (var m in paperIdList)
            {
                paperList.AddRange((from o in db.paper_table
                                    where o.ID == m
                                    select o).ToList<papers>());
            }
            ViewBag.paperList = paperList;
            return View();
        }
        public ActionResult MyErrorsFound()
        {
         //获取用户id
            int userID = Convert.ToInt32(Request.Cookies["userID"].Value);
            var MyErrorsFoundList = (from o in db.error_table
                                     where o.fromID == userID
                                     select o).ToList<errorHistory>();
            ViewBag.MyErrorsFoundList = MyErrorsFoundList;
            return View();
        }
    }
}