using System;
using System.Collections.Generic;

namespace Task.Data.Entities;

public partial class TypeStateEntity
{
    public int TypeStateId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly CreateDate { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public virtual ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}
