using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityMetadata
    {
        [MaxLength(256)]
        string? CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        
        [MaxLength(256)]
        string? ChangedBy { get; set; }
        DateTime ChangedAt { get; set; }
        
        // No soft deletes/updates needed yet
        // public DateTime? DeletedAt { get; set; }
    }
}