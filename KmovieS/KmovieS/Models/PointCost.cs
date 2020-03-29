using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KmovieS.Models
{

    public class PointCost
    {
        /* Nome dell'API e metodo (sintassi API-Metodo)*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string APIFullname
        {
            get;
            set;
        }
       
        /* Costo chiamata*/
        [Required]
        public int cost
        {
            get;
            set;
        }



    }
}
