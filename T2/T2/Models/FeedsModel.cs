using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T2.Models
{
    public class FeedsModel
    {

        /*FEED URL */
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FeedURL
        {
            get;
            set;
        }

        /*Feed Domain */
        public string Domain
        {
            get;
            set;
        }

        /* Feed Category */
        public string Category
        {
            get;
            set;
        }

        /* Date and hour of feed insertion */
        public DateTime DateFeedInsertion
        {
            get;
            set;
        }

        /* User that insert feed */
        public string IdUser
        {
            get;
            set;
        }

        /* User that insert feed */
        public bool FeedEnabled
        {
            get;
            set;
        }

    }
}