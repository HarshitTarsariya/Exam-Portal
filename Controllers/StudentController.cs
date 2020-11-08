﻿using ExamPortal.DTOS;
using ExamPortal.Services;
using ExamPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ExamPortal.Controllers
{
    public class StudentController : Controller
    {
        #region Constructor and Properties
        public IStudentService StudentService { get; }
        public ITeacherService TeacherService { get; }

        public StudentController(IStudentService studentService, ITeacherService teacherService)
        {
            StudentService = studentService;
            TeacherService = teacherService;
        }
        #endregion

        public IActionResult Index()
        {
            return RedirectToAction(nameof(GetResults));
        }
        [HttpGet]
        public IActionResult PaperCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PaperCode(string papercode)
        {
            ViewBag.User = User.Identity.Name;

            Func<IActionResult> InvalidCode = () =>
            {
                ViewBag.Data = "Invalid Paper code";
                return View();
            };
            Func<string, IActionResult> AlreadySubmitted = (teacher) =>
             {
                 ViewBag.Data = "You have Already Submitted the Paper Once , if Required Contact Paper Setter - " + teacher;
                 return View();
             };
            var ansSheet = StudentService.GetAnswerSheet(papercode, User.Identity.Name);
            if (CodeGenerator.GetPaperType(papercode) == EPaperType.MCQ)
            {
                if (ansSheet != null)
                    AlreadySubmitted((ansSheet as MCQAnswerSheetDTO).Paper.TeacherEmailId);
                var paperdto = StudentService.GetMcqPaper(papercode);
                if (paperdto == null)
                    return InvalidCode();

                return View("Verify", paperdto);
            }
            else if (CodeGenerator.GetPaperType(papercode) == EPaperType.Descriptive)
            {
                if (ansSheet != null)
                    return AlreadySubmitted((ansSheet as DescriptiveAnswerSheetDTO).Paper.TeacherEmailId);
                var paperdto = StudentService.GetDescriptiveAnswerSheetForExam(papercode).Paper;
                if (paperdto == null)
                    return InvalidCode();
                return View("Verify", paperdto);
            }
            else
                return InvalidCode();
        }
        [HttpGet]
        public IActionResult SubmitMCQPaper(string papercode)
        {
            var paperdto = StudentService.GetMcqPaper(papercode);
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return View(paperdto);
        }
        [HttpPost]
        public IActionResult SubmitMCQPaper(MCQPaperDTO mCQPaperDTO)
        {
            if (mCQPaperDTO.DeadLine >= new DateTime())
            {
                Func<int, int, string> GetMessage = (total, obtained) =>
                {
                    double per = (obtained * 100.0) / (total);
                    string msg;
                    if (per > 90.0)
                        msg = "Excellent Work!!";
                    else if (per > 80.0)
                        msg = "Great Going !! Keep it up";
                    else if (per > 70.0)
                        msg = "Good Progress , Require extra practise";
                    else if (per > 50.0)
                        msg = "Ahh Quite less Marks , Require regular practise , Don't pressurise Practise makes man perfect!!";
                    else
                        msg = "Requires Special Meeting with Faculty";
                    return msg;
                };
                KeyValuePair<int, int> marks = StudentService.SetMCQAnswerSheet(mCQPaperDTO, User.Identity.Name);
                ViewBag.MarksObtained = marks.Value;
                ViewBag.TotalMarks = marks.Key;
                ViewBag.Message = GetMessage(marks.Key, marks.Value);
                ViewBag.User = User.Identity.Name;
                return View();
            }
            return View("SubmitMCQPaper", mCQPaperDTO);
        }
        [HttpGet]
        public IActionResult SubmitDescriptivePaper(string papercode)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return View(StudentService.GetDescriptiveAnswerSheetForExam(papercode));
        }
        [HttpPost]
        public IActionResult SubmitDescriptivePaper(DescriptiveAnswerSheetDTO answerSheet)
        {
            return RedirectToAction(nameof(GetResults));
        }
        [HttpGet]
        public IActionResult GetResults()
        {
            return View(StudentService.GetMCQAnswerSheets(User.Identity.Name));
        }


    }
}