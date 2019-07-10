using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MCIT_Task.Entites
{
    public class topic
    {
        public int TopicId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TopicDate { get; set; }
        public string TopicTitle { get; set; }
        [Display(Name ="IsActive")]
        public bool TopicIsActive { get; set; }
        public TopicDetails TopicDetails { get; set; }
    }
}