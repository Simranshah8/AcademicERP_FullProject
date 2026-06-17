using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace StudentLogin.Models
{
    public class UserASP : StudentLogin.Models.Students.StudentUser, IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
           return true;
        }

        public UserASP(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
    }
}