using System;
using System.Collections.Generic;
using BaseProject.Application.Common;
using BaseProject.Application.Roles;
using BaseProject.Domain;
using Whoever.Common.Mapping;

namespace BaseProject.Application.Warehouse.Queries.GetAllWarehouse
{
    public class WarehouseLookupModel
    {
        public Guid WarehouseId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public double Latitude{ get; set; }
        public double Longitude{ get; set; }
        public string FileName{ get; set; }
        public bool HasImportedFile
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.FileName);
            }
        }

        public double? Distance{ get; set; }

        public string KilometerDisplay
        {
            get
            {
                return String.Format("{0:0.00} km", this.Distance);
            }
        }
    }
}
