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

namespace Org.Software.Data.Entities
{
  using System;
  using System.Collections.Generic;

  [DbMap(DbElement.Table, "Org_Software", "", "SoftwareModuleType")]
  public partial class SoftwareModuleType
  {
    public SoftwareModuleType()
    {
      this.SoftwareModules = new HashSet<SoftwareModule>();
    }

    public int SoftwareModuleTypeId {
      get;
      set;
    }
    public string SoftwareModuleTypeName {
      get;
      set;
    }

    public virtual ICollection<SoftwareModule> SoftwareModules {
      get;
      set;
    }
  }
}
