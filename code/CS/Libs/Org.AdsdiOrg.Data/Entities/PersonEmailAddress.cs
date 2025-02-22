//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Org.DB;
using Org.GS;
namespace Org.AdsdiOrg.Data.Entities
{
  using System;
  using System.Collections.Generic;

  [DbMap(DbElement.Table, "Adsdi_Org", "", "PersonEmailAddress")]
  public partial class PersonEmailAddress
  {
    public int PersonEmailAddressId {
      get;
      set;
    }
    public int PersonId {
      get;
      set;
    }
    public int EmailAddressId {
      get;
      set;
    }
    public string EmailAddressTypeCode {
      get;
      set;
    }
    public int EmailAddressStatusId {
      get;
      set;
    }
    public string PrivacyStatusCode {
      get;
      set;
    }

    public virtual EmailAddress EmailAddress {
      get;
      set;
    }
    public virtual EmailAddressStatu EmailAddressStatu {
      get;
      set;
    }
    public virtual EmailAddressType EmailAddressType {
      get;
      set;
    }
    public virtual Person Person {
      get;
      set;
    }
    public virtual PrivacyStatu PrivacyStatu {
      get;
      set;
    }
  }
}
