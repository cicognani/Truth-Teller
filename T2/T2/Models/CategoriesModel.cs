using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2.Models
{
    public class CategoriesModel
    {
        /* ID */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Category
        {
            get;
            set;
        }
    }
}