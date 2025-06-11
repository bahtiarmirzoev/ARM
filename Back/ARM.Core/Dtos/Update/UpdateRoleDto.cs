﻿namespace ARM.Core.Dtos.Update;

public class UpdateRoleDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<string> Permissions { get; set; } = [];
}
