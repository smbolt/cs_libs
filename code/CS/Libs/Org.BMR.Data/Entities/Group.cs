//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Org.GS;
using Org.DB;
namespace Org.BMR.Data.Entities
{
  using System;
  using System.Collections.Generic;

  [DbMap(DbElement.Table, "Adsdi_Org", "", "Group")]
  public partial class Group
  {
    public Group()
    {
      this.GroupMemberships = new HashSet<GroupMembership>();
    }

    public int GroupId {
      get;
      set;
    }
    public int OrgId {
      get;
      set;
    }
    public string GroupName {
      get;
      set;
    }
    public int StatusId {
      get;
      set;
    }

    public virtual Status Status {
      get;
      set;
    }
    public virtual ICollection<GroupMembership> GroupMemberships {
      get;
      set;
    }
    public virtual Organization Organization {
      get;
      set;
    }
  }
}
