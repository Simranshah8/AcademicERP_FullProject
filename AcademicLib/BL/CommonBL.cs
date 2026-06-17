using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcademicLib.BL
{
    public class CommonBL
    {
        protected ResponeValues IsValidName(string name)
        {
            ResponeValues resVal = new ResponeValues();
            if (string.IsNullOrEmpty(name))
            {
                resVal.ResponseMSG = "Empty not allowed";
            }
            //else if (Regex.IsMatch(name, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$") == false)
            else if (Regex.IsMatch(name, @"^[a-zA-Z\s.\?\,\'\;\:\!\-]+$")==false)
            {
                resVal.ResponseMSG = "Number/Special Characters are not allowed";
            }
            else
            {
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Valid";
            }

            return resVal;
        }

        protected ResponeValues IsValidEmail(string email)
        {
            ResponeValues resVal = new ResponeValues();
            if (string.IsNullOrEmpty(email))
            {
                resVal.ResponseMSG = "Empty not allowed";
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
             
            Regex regex = new Regex(emailPattern);
            bool isValid=regex.IsMatch(email);

            if(isValid)
            {
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Valid";
            }
            else
            {
                resVal.IsSuccess = false;
                resVal.ResponseMSG = "Invalid Email Id";
            }

            return resVal;

        }
        protected ResponeValues IsValidContactNo(string numbers)
        {
            ResponeValues resVal = new ResponeValues();
            
            if (string.IsNullOrEmpty(numbers))
            {
                resVal.ResponseMSG = "Empty not allowed";
            }

            if(numbers=="0")
            {
                resVal.IsSuccess = true;
                resVal.ResponseMSG = "Valid";
                return resVal;  
            }

            foreach (var number in numbers.Split(','))
            {
                if (numbers.Length < 10 || numbers.Length>13)
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Contact No.";
                    return resVal;
                }
                bool isValid = Regex.Match(number, @"^([0-9])").Success;

                if (isValid)
                {
                    resVal.IsSuccess = true;
                    resVal.ResponseMSG = "Valid";
                }
                else
                {
                    resVal.IsSuccess = false;
                    resVal.ResponseMSG = "Invalid Contact No.";
                }
            }
            

            return resVal;

        }
    }
}
