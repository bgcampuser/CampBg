﻿namespace CampBg.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CampBg.Data.Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserProfile : IdentityUser, IDeletable, IAuditable
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }

        [DefaultValue(false)]
        public bool IsSubscribedForNewsletter { get; set; }

        public DateTime? DeletedOn { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid? ForgottenPasswordToken { get; set; }

        public string PhoneNumber { get; set; }
    }
}
