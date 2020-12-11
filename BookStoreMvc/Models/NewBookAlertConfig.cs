using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Models
{
    public class NewBookAlertConfig
    {
        public bool DisplayNewBookAlert { get; set; }
        public string BookName { get; set; }
    }
}
