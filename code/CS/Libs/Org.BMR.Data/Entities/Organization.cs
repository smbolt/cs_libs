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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "Organization")]
  public partial class Organization
  {
    public Organization()
    {
      this.Groups = new HashSet<Group>();
      this.Organization1 = new HashSet<Organization>();
      this.OrgPersons = new HashSet<OrgPerson>();
      this.OrgPersonTypes = new HashSet<OrgPersonType>();
      this.RelatedOrgs = new HashSet<RelatedOrg>();
      this.RelatedOrgs1 = new HashSet<RelatedOrg>();
      this.Accounts = new HashSet<Account>();
      this.AppLogs = new HashSet<AppLog>();
    }

    public int OrgId {
      get;
      set;
    }
    public Nullable<int> ParentOrgId {
      get;
      set;
    }
    public int OrgStatusId {
      get;
      set;
    }
    public int OrgTypeId {
      get;
      set;
    }
    public string OrgName {
      get;
      set;
    }
    public string OrgDescription {
      get;
      set;
    }

    public virtual ICollection<Group> Groups {
      get;
      set;
    }
    public virtual ICollection<Organization> Organization1 {
      get;
      set;
    }
    public virtual Organization Organization2 {
      get;
      set;
    }
    public virtual OrgStatu OrgStatu {
      get;
      set;
    }
    public virtual OrgType OrgType {
      get;
      set;
    }
    public virtual ICollection<OrgPerson> OrgPersons {
      get;
      set;
    }
    public virtual ICollection<OrgPersonType> OrgPersonTypes {
      get;
      set;
    }
    public virtual ICollection<RelatedOrg> RelatedOrgs {
      get;
      set;
    }
    public virtual ICollection<RelatedOrg> RelatedOrgs1 {
      get;
      set;
    }
    public virtual ICollection<Account> Accounts {
      get;
      set;
    }
    public virtual ICollection<AppLog> AppLogs {
      get;
      set;
    }
  }
}
