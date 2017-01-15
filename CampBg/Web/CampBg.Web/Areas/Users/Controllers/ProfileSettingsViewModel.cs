namespace CampBg.Web.Areas.Users.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    using CampBg.Web.Localization;

    public class ProfileSettingsViewModel
    {
        public ProfileSettingsViewModel()
        {
        }

        public ProfileSettingsViewModel(UserProfile profile)
        {
            this.Email = profile.Email;
            this.IsSubscribedForNewsletter = profile.IsSubscribedForNewsletter;
            this.PhoneNumber = profile.PhoneNumber;
        }

        public static Expression<Func<UserProfile, ProfileSettingsViewModel>> FromProfile
        {
            get
            {
                return pr => new ProfileSettingsViewModel
                                 {
                                     Email = pr.Email,
                                     PhoneNumber = pr.PhoneNumber,
                                     IsSubscribedForNewsletter = pr.IsSubscribedForNewsletter
                                 };
            }
        }

        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(ViewModels), Name = "Email_address")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Phone_number")]
        public string PhoneNumber { get; set; }

        [Display(ResourceType = typeof(ViewModels), Name = "Is_subscribed_newsletter")]
        public bool IsSubscribedForNewsletter { get; set; }
    }
}