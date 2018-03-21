namespace ff.words.data.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Tracking Entity Class.
    /// </summary>
    public class BaseEntity : Entity
    {

        [MaxLength(100)]
        public string CreatedUser { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(100)]
        public string UpdatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual void Audit(string user)
        {
            if (Id <= 0)
            {
               CreatedUser = user;
               CreatedDate = DateTime.UtcNow;
            }

            UpdatedUser = user;
            UpdatedDate = DateTime.UtcNow;

            AuditChildren(user);
        }

        protected virtual void AuditChildren(string user)
        {
            return;
        }
    }
}