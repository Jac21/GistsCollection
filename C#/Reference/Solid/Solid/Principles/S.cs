using System;
using System.IO;
using Solid.Helpers;

namespace Solid.Principles
{
    /// <summary>
    /// S — Single responsibility principle
    /// "a class or module should have one, and only one, reason to be changed."
    /// </summary>
    public class S
    {
        // Let’s do an example of how to write a piece of code that violates this principle.

        public class User
        {
            public void CreatePost(Database db, string postMessage)
            {
                try
                {
                    db.Add(postMessage);
                }
                catch (Exception ex)
                {
                    db.LogError("An error occured: ", ex.ToString());
                    File.WriteAllText("\\LocalErrors.txt", ex.ToString());
                }
            }
        }

        // Let’s try to correct it.

        public class ErrorLogger
        {
            private readonly Database db = new Database();

            public void Log(string error)
            {
                db.LogError("An error occured: ", error);
                File.WriteAllText("\\LocalErrors.txt", error);
            }
        }

        public class Post
        {
            private readonly ErrorLogger errorLogger = new ErrorLogger();

            public void CreatePost(Database db, string postMessage)
            {
                try
                {
                    db.Add(postMessage);
                }
                catch (Exception ex)
                {
                    errorLogger.Log(ex.ToString());
                }
            }
        }
    }
}