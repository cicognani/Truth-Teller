using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2.Models
{
    public class RatiesModel
    {
        /* ID */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id
        {
            get;
            set;
        }

        /* URL LINK */
        [Required]
        public string Link
        {
            get;
            set;
        }

        /* User that is rating*/
        [Required]
        public string IdUser
        {
            get;
            set;
        }

        /* Date and hour rating */
        [Required]
        public DateTime DateRate
        {
            get;
            set;
        }

        /* Rate true */
        public bool IsTrue
        {
            get;
            set;
        }

        /* Rate false */
        [Required]
        public bool IsFake
        {
            get;
            set;
        }

    }
}