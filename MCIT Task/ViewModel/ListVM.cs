using MCIT_Task.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCIT_Task.ViewModel
{
    public class ListVM
    {
        public IEnumerable<IGrouping<DateTime,topic>> TopicsSortedByDAte { get; set; }
    }
}