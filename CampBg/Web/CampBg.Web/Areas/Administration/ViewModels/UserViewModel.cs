namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class UserViewModel
    {
        public static Expression<Func<UserProfile, UserViewModel>> FromUserProfile
        {
            get
            {
                return
                    user =>
                    new UserViewModel
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            IsSubscribedForNewsletter = user.IsSubscribedForNewsletter,
                            Email = user.Email,
                            IsDeleted = user.IsDeleted,
                            CreatedOn = user.CreatedOn,
                            DeletedOn = user.DeletedOn,
                            PhoneNumber = user.PhoneNumber
                        };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsSubscribedForNewsletter { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PhoneNumber { get; set; }
    }
}