using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMvc.Enum
{
    public enum LanguageEnum
    {
        //English,
        //French,
        //German,
        //Chinese,
        //Japanese,
        //Spanish
        [Display(Name ="English Language")]
        English = 10,
        [Display(Name = "French Language")]
        French = 11,
        [Display(Name = "German Language")]
        German = 12,
        [Display(Name = "Chinese Language")]
        Chinese = 13,
        [Display(Name = "Japanese Language")]
        Japanese = 14,
        [Display(Name = "Spanish Language")]
        Spanish = 17
    }
}
