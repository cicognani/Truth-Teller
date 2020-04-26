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
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Category
        {
            get;
            set;
        }
    }
}