using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2.Models
{
    public class LinksModel
    {
        /* URL */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Url
        {
            get;
            set;
        }

        /*Link title */
        public string Title
        {
            get;
            set;
        }
      

        /* Link domain */
        public string Domain
        {
            get;
            set;
        }

        /* Link intro */
        public string Intro
        {
            get;
            set;
        }

        /* Category */
        public string Category
        {
            get;
            set;
        }

        /* Is certified as true */
        public bool IsTrueCertified
        {
            get;
            set;
        }

        /* Is certified as false */
        public bool IsFalseCertified
        {
            get;
            set;
        }

        /*Rating*/
        public float UrlRating
        {
            get;
            set;
        }


        /* Date and hour of certification */
        public DateTime DateCertified
        {
            get;
            set;
        }

        /* User that certified */
        public string IdUserCertified
        {
            get;
            set;
        }


    }
}