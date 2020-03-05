using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KmovieS.Models
{
    public class FileUpload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int imageid
        {
            get;
            set;
        }
        public string imagename
        {
            get;
            set;
        }
        public byte[] imagedata
        {
            get;
            set;
        }

        public DateTime imagedateupload
        {
            get;
            set;
        }

        public string idUser
        {
            get;
            set;
        }


        /* Aggiungere campo IdUtente */

    }
}