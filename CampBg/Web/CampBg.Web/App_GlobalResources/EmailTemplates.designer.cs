﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CampBg.Web.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class EmailTemplates {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EmailTemplates() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CampBg.Web.App_GlobalResources.EmailTemplates", typeof(EmailTemplates).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Company address: {0}.
        /// </summary>
        public static string Company_address {
            get {
                return ResourceManager.GetString("Company_address", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Company name: {0}.
        /// </summary>
        public static string Company_name {
            get {
                return ResourceManager.GetString("Company_name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Custodian: {0}.
        /// </summary>
        public static string Custodian {
            get {
                return ResourceManager.GetString("Custodian", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EIK: {0}.
        /// </summary>
        public static string EIK {
            get {
                return ResourceManager.GetString("EIK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0},&lt;/br&gt; &lt;p&gt;Please click &lt;a href=&apos;{1}&apos;&gt;here&lt;/a&gt; to change your password&lt;/p&gt; Sincerely,&lt;/br&gt; The CAMP.BG team&lt;/br&gt; &lt;a href=&quot;http://www.camp.bg&quot;&gt;www.camp.bg&lt;/a&gt;.
        /// </summary>
        public static string Forgotten_password_email_body {
            get {
                return ResourceManager.GetString("Forgotten_password_email_body", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Forgotten password - Camp.bg.
        /// </summary>
        public static string Forgotten_password_email_title {
            get {
                return ResourceManager.GetString("Forgotten_password_email_title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html&gt;
        ///&lt;head&gt;
        ///    &lt;title&gt;Camp.bg order&lt;/title&gt;
        ///&lt;/head&gt;
        ///&lt;body&gt;
        ///Hello,
        ///&lt;br/&gt;
        ///Thank you for shopping at CAMP.BG. If you have any questions regarding your order you can email us at {0} or call us on {1}.
        ///You can find the order details below. Thank you once again!
        ///&lt;br/&gt;
        ///Order id {2} (made on {3})
        ///&lt;br/&gt;
        ///{4}
        ///&lt;br/&gt;
        ///{5}
        ///&lt;br/&gt;
        ///Payment method: {6}
        ///&lt;br/&gt;
        ///Delivery information:
        ///&lt;br/&gt;
        ///{7}
        ///&lt;br/&gt;
        ///Delivery method: {8}
        ///&lt;br/&gt;
        ///&lt;table&gt;
        ///&lt;tr&gt;&lt;td&gt;Item&lt;/td&gt;&lt;td&gt;Qty&lt;/td&gt;&lt;td&gt;Price&lt;/td&gt;&lt;/tr&gt;
        ///{9 [rest of string was truncated]&quot;;.
        /// </summary>
        public static string OrderBody {
            get {
                return ResourceManager.GetString("OrderBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Camp.bg order confirmation.
        /// </summary>
        public static string OrderTitle {
            get {
                return ResourceManager.GetString("OrderTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Payment information.
        /// </summary>
        public static string Payment_information {
            get {
                return ResourceManager.GetString("Payment_information", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Bank transfer.
        /// </summary>
        public static string Payment_method_bank {
            get {
                return ResourceManager.GetString("Payment_method_bank", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Upon delivery.
        /// </summary>
        public static string Payment_method_delivery {
            get {
                return ResourceManager.GetString("Payment_method_delivery", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid payment method.
        /// </summary>
        public static string Payment_method_unknown {
            get {
                return ResourceManager.GetString("Payment_method_unknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VAT Number: {0}.
        /// </summary>
        public static string Vat_number {
            get {
                return ResourceManager.GetString("Vat_number", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Congratulations, you have registered successfully! Welcome to Camp.bg and enjoy your stay! &lt;p&gt;Regards, &lt;/br&gt; The Camp.bg Team&lt;/p&gt;.
        /// </summary>
        public static string Welcome_email_body {
            get {
                return ResourceManager.GetString("Welcome_email_body", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to Camp.bg.
        /// </summary>
        public static string Welcome_email_title {
            get {
                return ResourceManager.GetString("Welcome_email_title", resourceCulture);
            }
        }
    }
}
