using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace TcmHMS.Entities
{
    public class Patient : Entity, IHasCreationTime
    {
        public DateTime CreationTime { get; set; }

        public Patient()
        {
            this.CreationTime = DateTime.Now;
        }
    }
}
