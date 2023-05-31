using System;
using System.Collections.Generic;
using System.Text;
using Whoever.Entities;
using Whoever.Entities.Interfaces;

namespace BaseProject.Domain
{
    public class Warehouse : IHasCreationTime, ISoftDelete
    {
        public Warehouse()
        {
        }
        public Guid WarehouseId { get; set; }
        public string Name{ get; set; }
        public string Code{ get; set; }
        public string Address { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public double Longitude{ get; set; }
        public double Latitude { get; set; }

        public string FileData { get; set; }
        public string MymeType { get; set; }
        public string FileName { get; set; }

        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        

    }
}
