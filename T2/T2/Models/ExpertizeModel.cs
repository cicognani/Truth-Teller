using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace T2.Models
{
    public class ExpertizeModel
    {

        /* ID Expertize*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id
        {
            get;
            set;
        }

        /*The User*/
        [Required]
        public string IdUser
        {
            get;
            set;
        }

        /*Category */
        [Required]
        public string Category
        {
            get;
            set;
        }  

    }
}