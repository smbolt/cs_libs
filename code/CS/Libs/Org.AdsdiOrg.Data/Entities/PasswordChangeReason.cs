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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "PasswordChangeReason")]
  public partial class PasswordChangeReason
  {
    public PasswordChangeReason()
    {
      this.PasswordChangeHistories = new HashSet<PasswordChangeHistory>();
    }

    public int PasswordChangeReasonCode {
      get;
      set;
    }
    public string PasswordChangeReasonValue {
      get;
      set;
    }

    public virtual ICollection<PasswordChangeHistory> PasswordChangeHistories {
      get;
      set;
    }
  }
}
