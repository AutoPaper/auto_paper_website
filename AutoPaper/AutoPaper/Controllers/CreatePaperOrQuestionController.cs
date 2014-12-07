﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoPaper.Models;
using System.IO;
using System.Text;


namespace AutoPaper.Controllers
{
    public class CreatePaperOrQuestionController : Controller
    {
        private PaperShareDBContext db = new PaperShareDBContext();
        private int uploadNum = 0;
        public class Question
        {
            public int ID { get; set; }
            public int teacherID { get; set; }
            public string subject { get; set; }
            public string content { get; set; }
            public string answer { get; set; }
            public int questionType { get; set; }
            public int difficulty { get; set; }
            public int citationCount { get; set; }
            public DateTime updateTime { get; set; }
            public string teacherName { get; set; }
        }
        public ActionResult Default()
        {

            var t_choiceQuestion = (from a in db.question_table
                                    join b in db.user_table
                                    on a.teacherID equals b.ID
                                    where a.questionType == 1
                                    orderby a.citationCount
                                    select new { a, b.name }).Take(2).ToList();
            List<Question> choiceQuestion = new List<Question>();
            foreach (var t_q in t_choiceQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                choiceQuestion.Add(t_qestion);
            }
            ViewBag.choiceQuestion = choiceQuestion;
            var t_blankQuestion = (from a in db.question_table
                                   join b in db.user_table
                                   on a.teacherID equals b.ID
                                   where a.questionType == 2
                                   orderby a.citationCount
                                   select new { a, b.name }).Take(2).ToList();
            List<Question> blankQuestion = new List<Question>();
            foreach (var t_q in t_blankQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                blankQuestion.Add(t_qestion);
            }
            ViewBag.blankQuestion = blankQuestion;
            var t_judgeQuestion = (from a in db.question_table
                                   join b in db.user_table
                                   on a.teacherID equals b.ID
                                   where a.questionType == 3
                                   orderby a.citationCount
                                   select new { a, b.name }).Take(2).ToList();
            List<Question> judgeQuestion = new List<Question>();
            foreach (var t_q in t_judgeQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                judgeQuestion.Add(t_qestion);
            }
            ViewBag.judgeQuestion = judgeQuestion;
            var t_integratedQuestion = (from a in db.question_table
                                        join b in db.user_table
                                        on a.teacherID equals b.ID
                                        where a.questionType == 4
                                        orderby a.citationCount
                                        select new { a, b.name }).Take(2).ToList();
            List<Question> integratedQuestion = new List<Question>();
            foreach (var t_q in t_integratedQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                integratedQuestion.Add(t_qestion);
            }
            ViewBag.integratedQuestion = integratedQuestion;

            ViewBag.isUpload = "false";
            return View();
        }
        public ActionResult getQuestion()
        {
            string match = Request.Form["search-input"];
            var t_choiceQuestion = (from a in db.question_table
                                    join d in db.user_table
                                    on a.teacherID equals d.ID
                                    where
                                    (from b in db.QTags_table
                                     where
                                     (from c in db.tag_table
                                      where KMPMatch(c.content, match, 0) != -1
                                      select c.ID).Contains(b.tagID)
                                     select b.questionID).Contains(a.ID) && a.questionType == 1
                                    orderby a.citationCount
                                    select new { a, d.name }).Take(10).ToList();

            List<Question> choiceQuestion = new List<Question>();
            foreach (var t_q in t_choiceQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                choiceQuestion.Add(t_qestion);
            }
            ViewBag.choiceQuestion = choiceQuestion;

            var t_blankQuestion = (from a in db.question_table
                                   join d in db.user_table
                                   on a.teacherID equals d.ID
                                   where
                                   (from b in db.QTags_table
                                    where
                                    (from c in db.tag_table
                                     where KMPMatch(c.content, match, 0) != -1
                                     select c.ID).Contains(b.tagID)
                                    select b.questionID).Contains(a.ID) && a.questionType == 2
                                   orderby a.citationCount
                                   select new { a, d.name }).Take(10).ToList();

            List<Question> blankQuestion = new List<Question>();
            foreach (var t_q in t_blankQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                blankQuestion.Add(t_qestion);
            }
            ViewBag.blankQuestion = blankQuestion;
            var t_judgeQuestion = (from a in db.question_table
                                   join d in db.user_table
                                   on a.teacherID equals d.ID
                                   where
                                   (from b in db.QTags_table
                                    where
                                    (from c in db.tag_table
                                     where KMPMatch(c.content, match, 0) != -1
                                     select c.ID).Contains(b.tagID)
                                    select b.questionID).Contains(a.ID) && a.questionType == 3
                                   orderby a.citationCount
                                   select new { a, d.name }).Take(10).ToList();

            List<Question> judgeQuestion = new List<Question>();
            foreach (var t_q in t_judgeQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                judgeQuestion.Add(t_qestion);
            }
            ViewBag.judgeQuestion = judgeQuestion;
            var t_integratedQuestion = (from a in db.question_table
                                        join d in db.user_table
                                        on a.teacherID equals d.ID
                                        where
                                        (from b in db.QTags_table
                                         where
                                         (from c in db.tag_table
                                          where KMPMatch(c.content, match, 0) != -1
                                          select c.ID).Contains(b.tagID)
                                         select b.questionID).Contains(a.ID) && a.questionType == 4
                                        orderby a.citationCount
                                        select new { a, d.name }).Take(10).ToList();

            List<Question> integratedQuestion = new List<Question>();
            foreach (var t_q in t_integratedQuestion)
            {
                Question t_qestion = new Question();
                t_qestion.ID = t_q.a.ID;
                t_qestion.teacherID = t_q.a.teacherID;
                t_qestion.subject = t_q.a.subject;
                t_qestion.content = t_q.a.content;
                t_qestion.answer = t_q.a.answer;
                t_qestion.questionType = t_q.a.questionType;
                t_qestion.difficulty = t_q.a.difficulty;
                t_qestion.citationCount = t_q.a.citationCount;
                t_qestion.updateTime = t_q.a.updateTime;
                t_qestion.teacherName = t_q.name;
                integratedQuestion.Add(t_qestion);
            }
            ViewBag.integratedQuestion = integratedQuestion;
            return PartialView("_searchPartial");
        }


        public string getAnswer(int id)//答案
        {
            var answer = ((from o in db.question_table
                           where o.ID == id
                           select o.answer)).ToArray<string>();
            if (answer.Length > 0)
                return answer[0];
            else
                return "未找到答案";
        }

        public JsonResult findError(int id)//挑错
        {
            //获得该题上传者id
            var teacherId = ((from o in db.question_table
                              where o.ID == id
                              select o.teacherID)).ToArray<int>();

            string message = "";
            //查找user_table表，找到该上传者
            if (teacherId.Length > 0)
            {
                int t_teacherID = teacherId[0];
                var teacherName = ((from o in db.user_table
                                    where o.ID == t_teacherID
                                    select o.name)).ToArray<string>();
                if (teacherName.Length > 0)
                    message = "已将您的消息发送给本题目的上传者" + teacherName[0];
                else
                {
                    message = "未找到本题上传者";
                    return Json(message);
                }
            }
            else
            {
                message = "未找到本题上传者";
                return Json(message);
            }
            //获得纠错用户id
            int fromID = Convert.ToInt32(Request.Cookies["userID"].Value);
            var t_content = Request.Form["errorContent"];
            //更新数据库
            //errorHistory
            errorHistory t_errorHistory = new errorHistory();
            t_errorHistory.fromID = fromID;
            t_errorHistory.toID = teacherId[0];
            t_errorHistory.questionID = id;
            t_errorHistory.content = t_content;
            db.error_table.Add(t_errorHistory);
            //notices
            notices t_notices = new notices();
            t_notices.userID = teacherId[0];
            t_notices.noticeType = 0;
            t_notices.content = fromID.ToString() + t_content;

            db.SaveChanges();

            //返回结果
            return Json(message);
        }

        public ActionResult GetMyPaperList(int id)
        {
            int userID = Convert.ToInt32(Request.Cookies["userID"].Value);
            var paperList = (from a in db.paper_table
                             join b in db.PT_table
                             on a.ID equals b.paperID
                             where b.teacherID == userID
                             select a).ToList<papers>();
            ViewBag.paperList = paperList;
            return PartialView("*******");
        }

        public bool addToPaper(int id)
        {
            //获得试卷id
            var paperId = (from o in db.paper_table
                           where o.name == Request.Form["*****"]
                           select o.ID).ToArray<int>();

            if (paperId.Length > 0)
            {
                //更新数据库
                PQ t_PQ = new PQ();
                t_PQ.paperID = paperId[0];
                t_PQ.questionID = id;
                db.PQ_table.Add(t_PQ);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool collectQuestion(int id)//收藏试题
        {
            int ID = Convert.ToInt32(Request.Cookies["userID"].Value);
            QU t_pu = new QU();
            t_pu.userID = ID;
            t_pu.questionID = id;
            db.QU_table.Add(t_pu);
            db.SaveChanges();
            return true;
        }
        public ActionResult dealwithDoc()//处理上传的文档，解析并发回前端
        {
            uploadNum = 0;
            string data = "";
            Stream fileStream = Request.Files["upload-doc"].InputStream;
           
            //    Stream fileStream = Request.Files[upload].InputStream;

                /*     string fileName = Path.GetFileName(Request.Files[upload].FileName);
                     int fileLength = Request.Files[upload].ContentLength;
                     //本地存放路径
                     string path = AppDomain.CurrentDomain.BaseDirectory + "uploads/";
                     //将文件以文件名filename存放在path路径中
                     Request.Files[upload].SaveAs(Path.Combine(path, fileName));

                     //从本地读取doc文件
                     StreamReader sr = new StreamReader(path, Encoding.Unicode);
                     //将数据存入data中 */

                StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
                data = sr.ReadToEnd();


            

            List<string> questionList = new List<string>();
            int start = 0, end = 0;
            data.Replace("\\r\\n\\d+\\.", "@@@");
            
            start = KMPMatch(data, "@@@", start) + 1;
            while (end < data.Length)
            {
                end = KMPMatch(data, "@@@", start) - 3;
                if (end == -4)
                    end = data.Length;
                string t_question = "";
                t_question = data.Substring(start-1, end-start+1);
                questionList.Add(t_question);
                uploadNum++;
                start = end + 4;
            }
            ViewBag.questionList = questionList;
            ViewBag.isUpload = "true";
            return View("Default");
        }
        public bool docSave()//文档保存成功
        {
            if (uploadNum <= 0)
                return false;
            for (int i = 0; i < uploadNum; i++)
            {
                question t_question = new question();
                t_question.teacherID = Convert.ToInt32(Request.Cookies["userID"].Value);
                t_question.subject = Request.Form["subject-" + i.ToString()];
                t_question.content = Request.Form["content-" + i.ToString()];
                t_question.answer = Request.Form["answer-" + i.ToString()];
                string str = Request.Form["questionType-" + i.ToString()];
                if (str == "选择题")
                    t_question.questionType = 0;
                else if (str == "填空题")
                    t_question.questionType = 1;
                else if (str == "判断题")
                    t_question.questionType = 2;
                else if (str == "综合题")
                    t_question.questionType = 3;
                str = Request.Form["difficulty-" + i.ToString()];
                if (str == "简单")
                    t_question.difficulty = 1;
                else if (str == "一般")
                    t_question.difficulty = 2;
                else if (str == "困难")
                    t_question.difficulty = 3;
                t_question.citationCount = 0;
                t_question.updateTime = DateTime.Now;
                db.question_table.Add(t_question);
                db.SaveChanges();
            }
            return true;
        }



        int KMPMatch(string s, string p, int start)
        {
            int[] next = new int[100];
            int i, j;
            i = start;
            j = 0;
            Getnext(p, next);
            while (i < s.Length)
            {
                if (j == -1 || s[i] == p[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    j = next[j];       //消除了指针i的回溯
                }
                if (j == p.Length)
                    return i;       //返回end下标
            }
            return -1;
        }
        void Getnext(string T, int[] next)
        {
            int k = -1;
            int j = 0;
            next[0] = -1;
            while (j < T.Length - 1)
            {
                if (k == -1 || T.Take(j) == T.Take(k))
                {
                    j++;
                    k++;
                    next[j] = k;
                }
                else
                    k = next[k];
            }
        }
    }
}