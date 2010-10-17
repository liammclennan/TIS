using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TodayIShall.Core.Domain;

namespace TodayIShall.Web.Models
{
    public class NewAccountModel
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(500)]
        public string Email { get; set; }
        [Required]
        public string TimeZoneInfoId { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 4)]
        public string Password { get; set; }
        
        public SelectList TimeZones()
        {
            ReadOnlyCollection<TimeZoneInfo> zones = TimeZoneInfo.GetSystemTimeZones();
            return new SelectList(zones, "Id", "DisplayName", zones.First(z => z.Id.Equals("UTC")));
        }

        public void Update(Account account)
        {
            Mapper.Map(this, account);
        }
    }
}