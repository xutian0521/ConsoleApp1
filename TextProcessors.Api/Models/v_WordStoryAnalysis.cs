using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextProcessors.Api.Models
{
    public class v_WordStoryAnalysis
    {
        public string WordAndCount { get; set; }
        public string WordNotAppear { get; set; }
        public string WordAppeared { get; set; }
    }

}
