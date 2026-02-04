using System;
using System.Collections.Generic;

namespace MyApp.Domain.Entities;

public partial class CustomerAccount
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int CustomerId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
