using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KmovieS.Models
{
    public class LogCalls
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /* id chiamata */
        public long callid
        {
            get;
            set;
        }
        /* Nome dell'API chiamata */
        [Required]
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

        /* Data e ora della chiamata */
        [Required]
        public DateTime calldate
        {
            get;
            set;
        }

        /* Utente che ha fatto la chiamata */
        [Required]
        public string idUser
        {
            get;
            set;
        }

    }

}
