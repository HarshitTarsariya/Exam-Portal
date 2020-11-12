﻿using AutoMapper;
using ExamPortal.DTOS;
using ExamPortal.Models;
using ExamPortal.Repositories;
using ExamPortal.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Services
{
    //Teacher based services
    public interface ITeacherService
    {
        /// <summary>
        /// save paper to database and returns sharable code
        /// </summary>
        public string CreateMCQPaper(MCQPaperDTO paper);
        /// <summary>
        /// return paper associated with given code.
        /// </summary>
        public MCQPaperDTO getMCQPaperByCode(string code);
        public IEnumerable<PaperDTO> getMCQPapersByEmailId(string emailId);
        public Task<string> CreateDescriptivePaper(DescriptivePaperDTO DesPaper);
        public DescriptivePaperDTO getDescriptivePaper(string code);

        /// <summary>
        /// can delete any paper associated with given code
        /// </summary>
        public Task deletePaper(string papercode);
        public (MCQPaperDTO, List<MCQAnswerSheetDTO>) GetAnswerSheetsBycode(string papercode);
        public (DescriptivePaperDTO, List<DescriptiveAnswerSheetDTO>) GetDescriptiveAnswerSheetsBycode(string papercode);
        public void SetMarksInDescriptivePaper(string papercode, int marksgiven, string studentname);
    }

    public class TeacherServiceImpl : ITeacherService
    {
        #region Constructor and Properties

        public TeacherServiceImpl(IMapper mapper, IMCQPaperRepo paperRepo
            , IMCQAnswerSheetRepo answerSheetRepo, IFirebaseUpload fire, IDescriptivePaperRepo descriptivePaperRepo, IEmailService emailService, IDescriptiveAnswerSheetRepo descriptiveAnswerSheetRepo)
        {
            Mapper = mapper;
            McqPaperRepo = paperRepo;
            AnswerSheetRepo = answerSheetRepo;
            Fire = fire;
            DescriptivePaperRepo = descriptivePaperRepo;
            EmailService = emailService;
            DescriptiveAnswerSheetRepo = descriptiveAnswerSheetRepo;
        }

        public IMapper Mapper { get; }
        public IMCQPaperRepo McqPaperRepo { get; }
        public IMCQAnswerSheetRepo AnswerSheetRepo { get; }
        public IFirebaseUpload Fire { get; }
        public IDescriptivePaperRepo DescriptivePaperRepo { get; }
        public IEmailService EmailService { get; }
        public IDescriptiveAnswerSheetRepo DescriptiveAnswerSheetRepo { get; }
        #endregion
        public string CreateMCQPaper(MCQPaperDTO paper)
        {
            string code = CodeGenerator.GetSharableCode(EPaperType.MCQ);
            MCQPaper mcqPaper = Mapper.Map<MCQPaperDTO, MCQPaper>(paper);
            mcqPaper.PaperCode = code;
            foreach (var que in paper.Questions)
                mcqPaper.Questions.Add(que.DtoTOEntity());
            McqPaperRepo.Create(mcqPaper);

            string linktosend = $"https://localhost:44394/Teacher/PaperDetails/{code}";
            EmailService.SendMailForPaper(code, linktosend, paper.PaperTitle, paper.CreatedDate.ToString(), paper.DeadLine.ToString(), paper.TeacherEmailId);
            //sends mail regarding paper update

            return code;
        }

        public MCQPaperDTO getMCQPaperByCode(string code)
        {
            var paper = McqPaperRepo.GetByPaperCode(code);
            var paperdto = Mapper.Map<MCQPaper, MCQPaperDTO>(paper);
            foreach (var que in paper.Questions)
            {
                que.MCQOptions.Shuffle();
                paperdto.Questions.Add(que.EntityToDto());
            }
            return paperdto;
        }

        public IEnumerable<PaperDTO> getMCQPapersByEmailId(string emailId)
        {
            var ans = Mapper.Map<IEnumerable<MCQPaper>, List<PaperDTO>>(McqPaperRepo.GetByTeacherEmail(emailId));
            var ansfinal = ans.Concat(Mapper.Map<IEnumerable<DescriptivePaper>, List<PaperDTO>>(DescriptivePaperRepo.GetByTeacherEmail(emailId)));
            return ansfinal;
        }

        public async Task<string> CreateDescriptivePaper(DescriptivePaperDTO DesPaper)
        {
            string code = CodeGenerator.GetSharableCode(EPaperType.Descriptive);
            DesPaper.PaperCode = code;
            string linkwith = await Fire.Upload(DesPaper.paper.OpenReadStream(), null, code);
            DesPaper.PaperPdfUrl = linkwith.Replace("&", Fire.Ampersand);

            DescriptivePaper paper = Mapper.Map<DescriptivePaperDTO, DescriptivePaper>(DesPaper);
            DescriptivePaperRepo.Create(paper);

            EmailService.SendMailForPaper(code, linkwith, paper.PaperTitle, paper.CreatedDate.ToString(), paper.DeadLine.ToString(), paper.TeacherEmailId); //sends mail regarding paper update

            return code;
        }

        public DescriptivePaperDTO getDescriptivePaper(string code)
        {
            var paper = DescriptivePaperRepo.GetByPaperCode(code);
            var paperdto = Mapper.Map<DescriptivePaper, DescriptivePaperDTO>(paper);
            paperdto.PaperPdfUrl = paperdto.PaperPdfUrl.Replace(Fire.Ampersand, "&");
            return paperdto;
        }

        public async Task deletePaper(string papercode)
        {
            switch (CodeGenerator.GetPaperType(papercode))
            {
                case EPaperType.MCQ:
                    McqPaperRepo.Delete(papercode);
                    break;
                case EPaperType.Descriptive:
                    await Fire.DeleteEverything(papercode, DescriptiveAnswerSheetRepo.GetAllResponseByCode(papercode).Select(paper => paper.StudentEmailId).ToList());
                    DescriptivePaperRepo.Delete(papercode);
                    break;
            }
        }
        public (MCQPaperDTO, List<MCQAnswerSheetDTO>) GetAnswerSheetsBycode(string papercode)
        {
            var answerSheet = AnswerSheetRepo.GetByPaperCode(papercode);
            var paper = McqPaperRepo.GetByPaperCode(papercode);
            var paperdto = Mapper.Map<MCQPaper, MCQPaperDTO>(paper);
            var ans = Mapper.Map<IEnumerable<MCQAnswerSheet>, List<MCQAnswerSheetDTO>>(answerSheet);

            foreach (var que in paperdto.Questions)
            {
                paperdto.TotalMarks += que.Marks;
            }
            return (paperdto, ans);
        }
        public (DescriptivePaperDTO, List<DescriptiveAnswerSheetDTO>) GetDescriptiveAnswerSheetsBycode(string papercode)
        {

            var answersheets = DescriptiveAnswerSheetRepo.GetAllResponseByCode(papercode).ToList();
            var paper = DescriptivePaperRepo.GetByPaperCode(papercode);

            var ans = (
               paper: Mapper.Map<DescriptivePaper, DescriptivePaperDTO>(paper),
               answersheets: Mapper.Map<IEnumerable<DescriptiveAnswerSheet>, List<DescriptiveAnswerSheetDTO>>(answersheets)
            );
            for (int i = 0; i < ans.Item2.Count; i++)
            {
                ans.answersheets[i].AnswerLink = ans.answersheets[i].AnswerLink.Replace("__AMP__", "&");
            }
            return ans;
        }
        public void SetMarksInDescriptivePaper(string papercode, int marksgiven, string studentname)
        {
            DescriptiveAnswerSheetRepo.SetMarksInDescriptivePaper(papercode, marksgiven, studentname);
        }
    }
}
