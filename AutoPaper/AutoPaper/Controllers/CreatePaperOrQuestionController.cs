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
    public class CreatePaperOrQuestionController : Controller
    {
        private PaperShareDBContext db = new PaperShareDBContext();

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AssembleResult()
        {
            var knowledge_value = Request.Form["assemble-knowledge-value"];
            var choice_count = Request.Form["assemble-paper-choice-count"];
            var choice_score = Request.Form["assemble-paper-choice-score"];
            var choice_order = Request.Form["assemble-paper-choice-order"];
            var blanks_count = Request.Form["assemble-paper-fill-in-blanks-count"];
            var blanks_score = Request.Form["assemble-paper-fill-in-blanks-score"];
            var blanks_order = Request.Form["assemble-paper-fill-in-blanks-order"];
            var judge_count = Request.Form["assemble-paper-judge-count"];
            var judge_score = Request.Form["assemble-paper-judge-score"];
            var judge_order = Request.Form["assemble-paper-judge-order"];

            var v = from q in db.question_table where q.questionType == 1 select q;
            return View();
        }
	}
}