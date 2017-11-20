/**
 *  RuDiCon Soft (c) 2017
 * 
 *  User Data manipulation and store class.
 *  
 *  @created 2017-08-29 Artem Nikolaev
 *  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public class UserData
    {
        private String name;
        private string password;
        private DateTime lastUpdate;
        public bool IsSet = false;

        // Default constructor
        public UserData()
        {
            SetName("");
            SetPassword("");
            SetLastUpdate(DateTime.Now);
        }

        public void SetUserData(String newName , String newPassword)
        {
            SetName(newName);
            SetPassword(newPassword);
            SetLastUpdate(DateTime.Now);
            IsSet = true;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string value)
        {
            SetLastUpdate(DateTime.Now);
            name = value;
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetPassword(string value)
        {
            password = value;
        }

        public DateTime GetLastUpdate()
        {
            return lastUpdate;
        }

        public void SetLastUpdate(DateTime value)
        {
            lastUpdate = value;
        }

    }
}
