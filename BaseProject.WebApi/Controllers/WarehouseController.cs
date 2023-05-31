using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BaseProject.Application.Warehouse.Commands.CreateWarehouse;
using BaseProject.Application.Product.Queries.ExcelExport;
using BaseProject.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Application.Warehouse.Commands.UpdateWarehouse;
using BaseProject.Application.Warehouse.Commands.DeleteWarehouse;
using BaseProject.Application.Warehouse.Queries.NearestWarehouseCalculation;
using BaseProject.Application.Warehouse.Queries.GetDetailAddressLocation;
using BaseProject.Application.Warehouse.Queries.GetAllWarehouse;
using BaseProject.Application.Warehouse.Queries.GetWarehouseDetail;
using BaseProject.Application.GetAllWarehouse.Queries.GetAllProduct;

namespace BaseProject.WebApi.Controller
{
    [Authorize()]
    public class WarehouseController : BaseController
    {
        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WarehouseListViewModel>> GetAll([FromQuery] GetWarehouseListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<int>> Create([FromBody] CreateWarehouseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<WarehouseLookupModel>> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetWarehouseDetailQuery { WarehouseId = id }));
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("calculate/{latitude}/{longitude}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WarehouseLookupModel>>> Calculate(double latitude, double longitude)
        {
            return Ok(await Mediator.Send(new NearestWarehouseCalculationQuery { Latitude=latitude,Longitude=longitude}));
        }


        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDetailAddressLocation/{address}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WarehouseLookupModel>>> GetDetailAddressLocation(string address)
        {
            return Ok(await Mediator.Send(new GetDetailAddressLocationQuery { Address = address }));
        }


        /// delete user.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteWarehouseCommand { WarehouseId = id });
            return NoContent();
        }


        /// update user.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWarehouseCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }



        /// <summary>
        /// export product to excel.
        /// </summary>
        /// <returns></returns>
        [HttpPost("ProductExportExcel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<byte[]>> ProductExportExcel([FromBody] ProductExcelExportQuery command)
        {
            var file = await Mediator.Send(command);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Producto.xlsx");
        }



    }
}
