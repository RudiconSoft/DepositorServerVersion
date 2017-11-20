/**
 *  RuDiCon Soft (c) 2017
 * 
 *  Class for store info about selected file for encrypt
 *  
 *  @created 2017-08-23 Artem Nikolaev 
 *  @upadted 2017-09-13 Artem Nikolaev
 *                      Add IsSet() public method.
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDepositer
{
    public class FileData
    {
        private String filename;    // File name for work
        private String description; // File description for header 
        private String name;        // 
        private String password;
        //public bool IsSet;

        public FileData()
        {

        }

        public FileData(String _filename, String _description , String _name, String _password)
        {
            filename = _filename;
            description = _description;
            name = _name;
            password = _password;
        }

        public String GetFileName()
        {
            return filename;
        }
        public void SetFileName(String _filename)
        {
            filename = _filename;
        }

        public String GetDescription()
        {
            return description;
        }

        public void SetDescription(String _description)
        {
            description = _description;
        }

        public String GetName()
        {
            return name;
        }

        public void SetName(String _name)
        {
            name = _name;
        }

        public String GetPassword()
        {
            return password;
        }

        public void SetPassword(String _password)
        {
            password = _password;
        }


        // @return true if file data is set (not empty)
        public bool IsSet()
        {
            return filename.Length > 0 && name.Length > 0 && password.Length > 0;
        }
    }
}
