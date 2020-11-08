﻿using ExamPortal.DTOS;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ExamPortal.Utilities
{
    public interface IFirebaseUpload
    {
        public string Ampersand => "__AMP__";
        Task<string> Upload(DescriptivePaperDTO DesPaper);

        Task Delete(string papercode);
    }
    public class FirebaseUpload : IFirebaseUpload
    {
        private string ApiKey = "AIzaSyBvU1BhdtjG5FLwV1KFNDZlh7C_jNHcyDU";
        private string Bucket = "exam-portal-292805.appspot.com";
        private string AuthEmail = "exam@gmail.com";
        private string AuthPassword = "Exam123";

        public async Task Delete(string papercode)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var aa = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(aa.FirebaseToken)
                    }
                )
                .Child(papercode)
                .DeleteAsync();
            await task;

        }

        public async Task<string> Upload(DescriptivePaperDTO DesPaper)
        {
            Stream stream = DesPaper.paper.OpenReadStream();
            string FileName = DesPaper.paper.FileName;
            string papercode = DesPaper.PaperCode;


            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var aa = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(aa.FirebaseToken)
                    }
                )
                .Child(papercode)
                .Child(FileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                string link = await task;
                System.Diagnostics.Debug.WriteLine(link);
                return link;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception :{0}", ex);
                return "";
            }
        }
    }
}
