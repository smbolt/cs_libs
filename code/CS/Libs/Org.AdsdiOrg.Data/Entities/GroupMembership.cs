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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "GroupMembership")]
  public partial class GroupMembership
  {
    public int UserGroupMembershipId {
      get;
      set;
    }
    public int GroupId {
      get;
      set;
    }
    public int AccountId {
      get;
      set;
    }
    public int StatusId {
      get;
      set;
    }

    public virtual Account Account {
      get;
      set;
    }
    public virtual Group Group {
      get;
      set;
    }
    public virtual Status Status {
      get;
      set;
    }
  }
}
