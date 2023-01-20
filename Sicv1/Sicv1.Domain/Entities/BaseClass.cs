using System;

namespace Sicv1.Domain.Entities
{
    public abstract class BaseClass
    {
        public int ID { get; set; }
        public virtual bool STATUS { get; set; }
        public int CREATED_USER { get; set; }
        public DateTime CREATED_AT { get; set; }
        public int UPDATED_USER { get; set; }
        public DateTime UPDATED_AT { get; set; }
    }

    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public virtual bool STATUS { get; set; }
        public int CREATED_USER { get; set; }
        public string CREATED_AT { get; set; }
        public int UPDATED_USER { get; set; }
        public string UPDATED_AT { get; set; }
    }
}
