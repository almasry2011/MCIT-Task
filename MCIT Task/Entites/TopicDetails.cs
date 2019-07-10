using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MCIT_Task.Entites
{
    public class TopicDetails
    {
        public int TopicDetailsId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int TopicId { get; set; }
        public topic Topic { get; set; }

     
    }
}