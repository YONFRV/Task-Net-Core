using System;
using System.Collections.Generic;

namespace Task.Data.Entities;

public partial class TypeStateEntity
{
    public int TypeStateId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
