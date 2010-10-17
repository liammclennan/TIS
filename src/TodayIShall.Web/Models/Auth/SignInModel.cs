using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TodayIShall.Web.Models
{
    public class SignInModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^/Today/[\w-]+$")]
        public string ReturnUrl { get; set; }

        public string NameSlug
        {
            get { return ReturnUrl.Split('/').Last(s => !string.IsNullOrEmpty(s)); }
        }
    }
}