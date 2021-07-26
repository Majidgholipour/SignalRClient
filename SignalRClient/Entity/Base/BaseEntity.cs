using System;

namespace Client.Entity.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            DateTime= System.DateTime.Now;
        }
        public DateTime DateTime  { get; set; }
    }
}
