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

  [DbMap(DbElement.Table, "Adsdi_Org", "", "PoliticalUnit")]
  public partial class PoliticalUnit
  {
    public PoliticalUnit()
    {
      this.Addresses = new HashSet<Address>();
      this.Addresses1 = new HashSet<Address>();
      this.PoliticalUnit1 = new HashSet<PoliticalUnit>();
    }

    public int PoliticalUnitId {
      get;
      set;
    }
    public int CountryCode {
      get;
      set;
    }
    public int PoliticalUnitTypeId {
      get;
      set;
    }
    public Nullable<int> ParentPoliticalUnitId {
      get;
      set;
    }
    public string Name {
      get;
      set;
    }
    public string Abbreviation {
      get;
      set;
    }
    public string Description {
      get;
      set;
    }
    public string CapitalOrPrimaryCity {
      get;
      set;
    }
    public int StatusId {
      get;
      set;
    }
    public Nullable<bool> SuppressUsage {
      get;
      set;
    }

    public virtual ICollection<Address> Addresses {
      get;
      set;
    }
    public virtual ICollection<Address> Addresses1 {
      get;
      set;
    }
    public virtual Country Country {
      get;
      set;
    }
    public virtual ICollection<PoliticalUnit> PoliticalUnit1 {
      get;
      set;
    }
    public virtual PoliticalUnit PoliticalUnit2 {
      get;
      set;
    }
    public virtual PoliticalUnitType PoliticalUnitType {
      get;
      set;
    }
    public virtual Status Status {
      get;
      set;
    }
  }
}
