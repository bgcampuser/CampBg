namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Collections.Generic;

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
                            PhoneNumber = user.PhoneNumber,
                            UserRolesIDs = user.Roles.Select(x => x.RoleId).ToList()
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

        public List<string> UserRolesIDs { get; set; }

        public List<string> UserRolesValues { get; set; }

        public IEnumerable<UserRoleViewModel> UserRoles { get; set; }
    }
}