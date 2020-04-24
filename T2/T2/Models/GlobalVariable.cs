using T2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2.Utilities
{
    public class GlobalVariable
    {
        private string parAPIFullName;

        public static long LogCallID
        {
            get;
            set;
        }

        public static int ApiCost(string myAPIName)
        {
           ApplicationDbContext db = new ApplicationDbContext();
            // Recupero il costo dell'API                    
            var myRecord = db.pointCost.Single(a => a.APIFullname == myAPIName);
            return myRecord.cost;
        }

        public static int UserPointLeft(string myUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            // Controllo i punti dell'utente
            var UserCall = db.Users.Single(a => a.Id == myUserId);
            return UserCall.PointsLeft;
        }


        public static void UserPointSubtract(int parmyAPICost, string myUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();           
            // Scalo i punti all'utente
            var UserCall = db.Users.Single(a => a.Id == myUserId);       
            UserCall.PointsLeft = UserCall.PointsLeft - parmyAPICost;
            if (UserCall.PointsLeft < 0)
            {
                UserCall.PointsLeft = 0;
            }
            db.SaveChanges();
        }


        public static void UserPointAdd(int PointBuyed, string myUserId)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            // Aggiungo i punti all'utente
            var UserCall = db.Users.Single(a => a.Id == myUserId);
            UserCall.PointsLeft = UserCall.PointsLeft + PointBuyed;
            UserCall.PointsDateExpiration = DateTime.Today.AddDays(360);
            db.SaveChanges();
        }



        public static string RetrieveFullAPIName(string Req)
        {
        string myAPIFullName;
        string lastchar = Req.Substring(Req.Length - 1);
        int lastslash = Req.LastIndexOf("/");
        var IsNumeric = int.TryParse(lastchar,out int n);
            if (!IsNumeric)
            {
                myAPIFullName = Req.Substring(lastslash + 1, Req.Length-lastslash-1);
            }
            else
            {
                int penlastslash = Req.LastIndexOf("/", lastslash-1);
                myAPIFullName = Req.Substring(penlastslash + 1, lastslash-penlastslash - 1);
            }
        
        return myAPIFullName;
        }
        
        
        public static Boolean CheckPointCall(string myUsername, string parAPIFullname)
        {
            Boolean permitted = false;
            if (UserPointLeft(myUsername)-ApiCost(parAPIFullname)>=0)
            {
                permitted = true;
            }
            return permitted;
        }




    }
}