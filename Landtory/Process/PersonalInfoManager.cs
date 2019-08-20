using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Landtory.Programming;
using Landtory.Engine.API;

namespace Landtory.Process
{
    public class PersonalInfoManager
    {
        public static string GetPersonalInfoComputerString(Engine.API.PersonalInfo info)
        {
            string LicenseStatus2;
            switch (info.License)
            {
                case LicenseStatus.Vaild :
                    LicenseStatus2 = "Vaild";
                    break;
                case LicenseStatus.Expired :
                    LicenseStatus2 = "Expired";
                    break;
                case LicenseStatus.Revoked :
                    LicenseStatus2 = "Revoked";
                    if (Engine.API.Common.CommonFunc.GetRandomBool() == true)
                    {
                        LicenseStatus2 = "Suspended";
                    }
                    break;
                default :
                    LicenseStatus2 = "Vaild";
                    break;
            }
            string WarrantStatus;
            switch(info.Wanted)
            {
                case Warrant.Active :
                    WarrantStatus = "Non Arrestable";
                    break;
                case Warrant.Arrestable :
                    WarrantStatus = "Arrestable Warrant";
                    break;
                case Warrant.None :
                    WarrantStatus = "No Active Warrant";
                    break;
                default:
                    WarrantStatus = "N/A";
                    break;
            }
            string result = "Suspect Warrant: " + WarrantStatus + Environment.NewLine + "Suspect License: "+LicenseStatus2+"Suspect DOB: "+info.DOB.ToString();
            return result;
        }
    }
}
