﻿using ExamPortal.DTOS;
using ExamPortal.Repositories;
using ExamPortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ExamPortal.Models;

namespace ExamPortal.Utilities
{
    public static class ExtensionContainer
    {
        public static IServiceCollection configureDI(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherServiceImpl>();
            services.AddScoped<IStudentService, StudentServiceImpl>();
            services.AddScoped<IMCQPaperRepo, MCQPaperRepoImpl>();
            services.AddScoped<IMCQAnswerSheetRepo, MCQAnswerSheetRepoImpl>();
            services.AddAutoMapper(typeof(AutoMapperConfig));

            return services;
        }

        public static void configureAuth(this IServiceCollection services)
        {
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "429011546677-b4aqo982bjmj49422n4lk796lrbq1rt9.apps.googleusercontent.com";
                options.ClientSecret = "CPywROTlE_Hrj-z77ynSdaQS";
            });
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>();
        }

        public static MCQQuestion DtoTOEntity(this MCQQuestionDTO questionDTO)
        {
            MCQQuestion question = new MCQQuestion();
            for (var i = 0; i < questionDTO.Opetions.Count(); i++)
            {
                question.Marks = questionDTO.Marks;
                question.QuestionText = questionDTO.QuestionText;
                var opetion = new MCQOption() { OptionText = questionDTO.Opetions[i] };
                question.MCQOptions.Add(opetion);
                if (i == questionDTO.TrueAnswer)
                    question.TrueAnswer = opetion;
            }
            return question;
        }
    }
}
