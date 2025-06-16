using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Messages.Core
{
    public class SuccessMessage
    {
        public const string CreateSuccessful = "Create new {0} successful.";
        public const string UpdateSuccessful = "Update {0} successful.";
        public const string DeleteSuccessful = "Delete {0} successful.";
        public static string GetSuccessMessage(string template, params object[] args) =>
            string.Format(template, args);
    }
}
