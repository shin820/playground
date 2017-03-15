using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Jax.MultiTenancy;
using Jax.Users;

namespace Jax.Entities
{
    public class Task : Entity, IMustHaveTenant, IAudited
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }
        public TaskStatus Status { get; set; }
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        public int TenantId { get; set; }
        public long? AssignedUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }

        [ForeignKey(nameof(AssignedUserId))]
        public User AssignedUser { get; set; }

        public Task()
        {
            CreationTime = Clock.Now;
            Status = TaskStatus.Open;
        }

        public Task(string title, string description = null, long? assignedUserId = null) : this()
        {
            Title = title;
            Description = description;
            AssignedUserId = assignedUserId;
        }
    }
}
