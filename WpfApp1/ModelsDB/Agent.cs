using System;
using System.Collections.Generic;

namespace WpfApp1.ModelsDB;

public partial class Agent
{
    public byte Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public byte? DealShare { get; set; }
}
