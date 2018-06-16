using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN.Data.Utilities
{
    public class ConstantValues
    {
        public const int LENGTH_OF_IMAGE = 2097152;

        public const int PAGE_SIZE = 10;

        public const int PAGE_SIZE_USER = 8;

        public const string STRING_NOTICE_WARNING = "Warning";

        public const string STRING_UPLOAD = "/Uploads/";

        public const string STRING_SPLASH = "/";

        public static readonly List<string> LIST_IMAGE_TYPE = new List<string>() { ".jpg", ".jpeg", ".gif", ".png" };

        public const string STRING_PRODUCT_DEFAULT_IMAGE = "/Content/layoutUser/images/default-product-image.jpg";

        public const string STRING_AVATAR_DEFAULT_IMAGE = "/Content/layoutUser/images/default-avatar.png";

        public const string STRING_NO_RECORDS = "Cannot find any items!";

        public const string STRING_NO_PRODUCTS = "No product found on this category!";

        public enum ROLE
        {
            Admin = 1,
            Customer = 2,
        }

        public enum STATUS
        {
            /// <summary>
            /// The active
            /// </summary>
            Active = 1,
            /// <summary>
            /// The deleted
            /// </summary>
            Deleted = 0
        }

        public const bool ACTIVE = true;

        public const bool DELETED = false;

        public const string ROLE_ADMIN = "Admin";
        public const bool BOOL_DELETED = false;

        public const bool BOOL_ACTIVED = true;

        public const string STRING_DELETED = "Deleted";

        public const string STRING_ACTIVED = "Activated";

        public const string STRING_CREATE_PRODUCT_SUCCESS = "Your product has been successfully created.";

        public const string STRING_UPDATE_SUCCESS = "Record has been successfully updated.";

        public const string STRING_DELETE_SUCCESS = "Record has been successfully deleted.";

        public const string STRING_CREATE_USER_SUCCESS = "Create user successful";

        public const string STRING_CREATE_MENU_SUCCESS = "Create menu successful";

        public const string STRING_CREATE_SUCCESS = "Create customer service successful";

        public const string STRING_CHANGE_PASSWORD_SUCCESS = "Change password successful";

        public const string STRING_DELETE_USER_SUCCESS = "Delete successful";

        public const string STRING_NOTICE_SUCCESS = "Success";

        public const string ROLE_USER = "User";

        public const string STRING_SESSION_LOGIN = "auth";

        public const string SALT = @"「zrRu^Wq>NI7?=]e1Y`@PjX/]+Kjl\)POEgIIl(B5%J:%ow&;<87e]2;Ske3>&+7[」";

        public const string STRING_SUCCESS = "Success";

        public const string EDIT_CATEGORY_WARNING = "Your category being used";

        public const int INT_USER_DEFAULT_IMAGE = 291;

        public const string EMAIL_FROM_US = "phanductay789@gmail.com";

        public const string EMAIL_TO_US = "phanductay789@gmail.com";

        public const string PASSWORD_EMAIL_BIGSTORE = "(0123456789)";

        public const string STRING_EMAIL_SUBJECT = "[VEN-socical network]";

        public const string STRING_EMAIL_SUCCESS = "Thank for your message";

        public enum STATUS_ORDER
        {
            PENDING = 1, //Your order is in our system but we haven’t started working on it yet. You can still cancel the order. 
            PROCESSING, //We are getting your order ready to be shipped. You can no longer cancel the order.
            SHIPPED, //Order has been shipped and is being handled by the shipping provider. 
            RETURNED, //Order has been returned and refund has been processed by the store.
            CANCELED //Order has been canceled
        }

        public const string STRING_CART = "cart";

        public const string STRING_SESSION_CHECKOUT = "checkout";

        public const string STRING_DELETE_WARNING = "Can't not delete this record";
    }
}
