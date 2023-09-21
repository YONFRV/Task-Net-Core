using System;
using System.Collections.Generic;

namespace Task.Data.Entities;

public partial class TaskEntity
{
    public int TaskId { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public int? State { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
