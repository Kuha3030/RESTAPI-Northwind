using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RESTAPI_Northwind.Models;
namespace RESTAPI_Northwind.Services.Interfaces
{
    public interface IAuthenticateService
    {
        LoggedInUser Authenticate(string userName, string password);
    }
}
