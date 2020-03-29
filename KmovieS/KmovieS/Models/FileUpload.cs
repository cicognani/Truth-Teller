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
        /* id media */
        public long mediaid
        {
            get;
            set;
        }
        /* Nome del media */
        [Required]
        public string medianame
        {
            get;
            set;
        }
        /* Media memorizzato nel database */
        [Required]
        public byte[] mediadata
        {
            get;
            set;
        }

        /* Tag descrittivo del media */
        public string mediatag
        {
            get;
            set;
        }

        /* Tipo del media - Valori consentiti sono IMAGE,IMAGE360, VIDEO, VIDEO380, DOCUMENT, se non viene inserito finisce nella cartella COMMON*/
        [Required]
        public string mediatype
        {
            get;
            set;
        } = "COMMON";

        /* Data e ora del caricamento */
        [Required]
        public DateTime mediadateupload
        {
            get;
            set;
        }

        /* Utente che ha caricato il media */
        [Required]
        public string idUser
        {
            get;
            set;
        }

        /* Id dell'oggetto a cui si riferisce */
        [Required]
        public string objectReferenceId
        {
            get;
            set;
        } = "Unknown";

        /* Estensione del media */
        [Required]
        public string mediaextension
        {
            get;
            set;
        }

        /* Dimensione del media */
        [Required]
        public long mediasize
        {
            get;
            set;
        }

        /* Provenienza del media */
        [Required]
        public string mediasource
        {
            get;
            set;
        } = "Unknown";





    }
}