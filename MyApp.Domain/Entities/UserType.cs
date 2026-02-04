using System;
using System.Collections.Generic;

namespace MyApp.Domain.Entities;

public partial class UserType
{
    public int UserTypeId { get; set; }

    public string UserTypeName { get; set; } = null!;
}
