using T2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2.Utilities
{
    public class GlobalVariable
    {
      

        public static void UserCorrectAnswersAdd(string myUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();           
            // The user receive +1 point
            var UserCall = db.Users.Single(a => a.Id == myUserId);       
            UserCall.NCorrectAnswers = UserCall.NCorrectAnswers+1;
            db.SaveChanges();
        }


        public static void UserFaultAnswersAdd( string myUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            // The user receive +1 point
            var UserCall = db.Users.Single(a => a.Id == myUserId);
            UserCall.NFaultAnswers = UserCall.NFaultAnswers + 1;
            db.SaveChanges();
        }


    }
}