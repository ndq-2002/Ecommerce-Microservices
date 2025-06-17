using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Messages.Core
{
    public static class ErrorMessage
    {
        // -1
        public const string SomethingWentWrong = "Something went wrong. Please contact with administrator.";
        // -2
        public const string NotHavePermission = "You do not have permission to do this action.";
        // -3
        public const string AlreadyUpdatedByAnother = "Already updated by another.";
        // -4
        public const string Exists = "{0} already exists.";
        // -5
        public const string NotExists = "{0} does not exists.";

        // Optional: helper methods
        public static string GetErrorMessage(string template, params object[] args) =>
            string.Format(template, args);
    }
}
