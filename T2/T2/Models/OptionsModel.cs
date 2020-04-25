using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace T2.Models
{
    public class OptionsModel
    {

        /* ID Option*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id
        {
            get;
            set;
        }

        /*Option Name*/
        [Required]
        public string OptionName
        {
            get;
            set;
        }

        /*Option Value */
        [Required]
        public string OptionValue
        {
            get;
            set;
        }


    }
}