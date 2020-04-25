using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2.Models
{
    public class BlacklistModel
    {
        /* ID */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id
        {
            get;
            set;
        }

        /* Link */
        [Required]
        public string Link
        {
            get;
            set;
        }

        /*Is entire domain */
        [Required]
        public bool IsEntireDomain
        {
            get;
            set;
        }

        /* Date added*/
        [Required]
        public DateTime DateAdded
        {
            get;
            set;
        } = DateTime.Now;

        /*IdUser that added */
        [Required]
        public string IdUSer
        {
            get;
            set;
        }

    }
}