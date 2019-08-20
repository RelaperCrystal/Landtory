using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTA;

namespace Landtory.Engine.API
{
    public enum LicenseStatus
    {
        Vaild,
        Revoked,
        Expired
    }
    public enum Warrant
    {
        Active,
        Arrestable,
        None
    }
    public class PersonalInfo
    {
        public PersonalInfo(Ped target)
        {
            owner = target;
        }
        public void SetWarrant(Warrant status)
        {
            Wanted = status;
        }
        public Ped owner { get; private set; }
        public LicenseStatus License { get; set; }
        public Warrant Wanted { get; private set; }
        public DateTime DOB { get; set; }
    }
}
